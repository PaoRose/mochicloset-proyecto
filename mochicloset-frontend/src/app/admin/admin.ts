import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink, Router } from '@angular/router';
import { ApiClient } from '../core/http/api-client';
import { HttpClient } from '@angular/common/http';

interface Publicacion {
  id: number;
  titulo: string;
  precio: number;
  imagenUrl: string;
  usuarioId: number;
  vendedoraNombre?: string;
}

interface Usuaria {
  id: number;
  nombre: string;
  email: string;
  estado: string;
  rol: string;
  fotoPerfil: string;
  fechaRegistro: string;
}

interface Reporte {
  id: number;
  reportanteId: number;
  publicacionId: number | null;
  reportadaId: number | null;
  razon: string;
  estado: string;
  fechaReporte: string;
  reportante: { nombre: string };
  publicacion: { titulo: string } | null;
  reportada: { nombre: string } | null;
}

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './admin.html',
  styleUrl: './admin.css'
})
export class Admin implements OnInit {
  private api = inject(ApiClient);
  private http = inject(HttpClient);
  private router = inject(Router);
  private urlPublicaciones = 'http://localhost:5160/GestionPublicaciones';
  private urlUsuarios = 'http://localhost:5160/GestionUsuarios';
  private urlReportes = 'http://localhost:5160/GestionReportes';

  tabActiva = 'prendas';
  publicaciones: Publicacion[] = [];
  usuarias: Usuaria[] = [];
  reportes: Reporte[] = [];
  modalEliminar = false;
  prendaSeleccionada: Publicacion | null = null;
  razonEliminacion = '';
  adminId = 0;

  ngOnInit() {
    const usuario = JSON.parse(localStorage.getItem('usuario') || '{}');
    this.adminId = usuario.id;

    if (usuario.rol !== 'Admin') {
      this.router.navigate(['/home']);
      return;
    }

    this.cargarPublicaciones();
    this.cargarUsuarias();
    this.cargarReportes();
  }

  cambiarTab(tab: string) {
    this.tabActiva = tab;
  }

  cargarPublicaciones() {
    this.api.get<Publicacion[]>(this.urlPublicaciones + '/lista-publicaciones').subscribe({
      next: data => {
        this.publicaciones = data;
        for (const p of this.publicaciones) {
          this.api.get<any>(this.urlUsuarios + '/' + p.usuarioId).subscribe({
            next: u => p.vendedoraNombre = u.nombre,
            error: () => {}
          });
        }
      },
      error: err => console.error('Error al cargar publicaciones', err)
    });
  }

  cargarUsuarias() {
    this.api.get<Usuaria[]>(this.urlUsuarios + '/lista-usuarios').subscribe({
      next: data => this.usuarias = data.filter(u => u.rol !== 'Admin'),
      error: err => console.error('Error al cargar usuarias', err)
    });
  }

  cargarReportes() {
    this.api.get<Reporte[]>(this.urlReportes).subscribe({
      next: data => this.reportes = data,
      error: err => console.error('Error al cargar reportes', err)
    });
  }

  abrirModalEliminar(p: Publicacion) {
    this.prendaSeleccionada = p;
    this.razonEliminacion = '';
    this.modalEliminar = true;
  }

  cerrarModalEliminar() {
    this.modalEliminar = false;
    this.prendaSeleccionada = null;
  }

  eliminarPrenda() {
    if (!this.razonEliminacion.trim()) {
      alert('La razón de eliminación es obligatoria.');
      return;
    }

    this.http.delete(
      `${this.urlPublicaciones}/${this.prendaSeleccionada?.id}?adminId=${this.adminId}&razon=${this.razonEliminacion}`,
      { responseType: 'text' }
    ).subscribe({
      next: () => {
        this.cerrarModalEliminar();
        this.cargarPublicaciones();
      },
      error: (err) => {
        if (err.status === 204) {
          this.cerrarModalEliminar();
          this.cargarPublicaciones();
        } else {
          alert(err.error || 'Error al eliminar.');
        }
      }
    });
  }

  suspenderUsuaria(id: number) {
    this.http.put(
      `${this.urlUsuarios}/suspender-usuario/${id}?adminId=${this.adminId}`,
      {},
      { responseType: 'text' }
    ).subscribe({
      next: () => this.cargarUsuarias(),
      error: () => this.cargarUsuarias()
    });
  }

  activarUsuaria(id: number) {
    this.http.put(
      `${this.urlUsuarios}/activar-usuario/${id}?adminId=${this.adminId}`,
      {},
      { responseType: 'text' }
    ).subscribe({
      next: () => this.cargarUsuarias(),
      error: () => this.cargarUsuarias()
    });
  }

  resolverReporte(id: number) {
    this.http.put(`${this.urlReportes}/resolver/${id}`, {}, { responseType: 'text' }).subscribe({
      next: () => this.cargarReportes(),
      error: () => this.cargarReportes()
    });
  }
}
