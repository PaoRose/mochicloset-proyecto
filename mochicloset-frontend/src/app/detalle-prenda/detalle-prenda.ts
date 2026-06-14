import { Component, inject } from '@angular/core';
import { RouterLink, Router, ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiClient } from '../core/http/api-client';

interface Publicacion {
  id: number;
  titulo: string;
  descripcion: string;
  precio: number;
  talla: string;
  condicion: string;
  imagenUrl: string;
  usuarioId: number;
  categoriaId: number;
  fechaPublicacion: string;
}

interface Usuario {
  id: number;
  nombre: string;
  username: string;
  fotoPerfil: string;
}

@Component({
  selector: 'app-detalle-prenda',
  standalone: true,
  imports: [RouterLink, CommonModule, FormsModule],
  templateUrl: './detalle-prenda.html',
  styleUrl: './detalle-prenda.css'
})
export class DetallePrenda {
  private api = inject(ApiClient);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private url = 'http://localhost:5160/GestionPublicaciones';
  private urlUsuarios = 'http://localhost:5160/GestionUsuarios';
  private urlFavoritos = 'http://localhost:5160/GestionFavoritos';
  private urlReportes = 'http://localhost:5160/GestionReportes';

  publicacion: Publicacion | null = null;
  vendedora: Usuario | null = null;
  modalSeguridad = false;
  modalReporte = false;
  esFavorito: boolean = false;
  usuarioLogueadoId: number = 0;
  razonReporte = '';

  categorias: {[key: number]: string} = {
    1: 'Vestidos',
    2: 'Tops y Blusas',
    3: 'Pantalones',
    4: 'Faldas',
    5: 'Abrigos',
    6: 'Accesorios'
  };

  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    const usuario = JSON.parse(localStorage.getItem('usuario') || '{}');
    this.usuarioLogueadoId = usuario.id;

    this.api.get<Publicacion>(this.url + '/' + id).subscribe({
      next: data => {
        this.publicacion = data;
        this.api.get<Usuario>(this.urlUsuarios + '/' + data.usuarioId).subscribe({
          next: user => this.vendedora = user,
          error: () => {}
        });
        if (usuario.id) {
          this.api.get<any[]>(this.urlFavoritos + '/' + usuario.id).subscribe({
            next: favs => this.esFavorito = favs.some(f => f.publicacionId === data.id),
            error: () => {}
          });
        }
      },
      error: error => console.error('Error al cargar publicacion', error)
    });
  }

  toggleFavorito() {
    const usuario = JSON.parse(localStorage.getItem('usuario') || '{}');
    if (this.esFavorito) {
      this.api.delete(this.urlFavoritos + '/' + usuario.id + '/' + this.publicacion?.id).subscribe({
        next: () => this.esFavorito = false,
        error: () => this.esFavorito = false
      });
    } else {
      const favorito = { usuarioId: usuario.id, publicacionId: this.publicacion?.id };
      this.api.post(this.urlFavoritos, favorito).subscribe({
        next: () => this.esFavorito = true,
        error: (error) => { if (error.status === 200) this.esFavorito = true; }
      });
    }
  }

  abrirModalSeguridad() { this.modalSeguridad = true; }
  cerrarModalSeguridad() { this.modalSeguridad = false; }

  abrirReporte() { this.modalReporte = true; }
  cerrarReporte() { this.modalReporte = false; this.razonReporte = ''; }

  irAlChat() {
    this.modalSeguridad = false;
    const usuario = JSON.parse(localStorage.getItem('usuario') || '{}');
    const conversacion = {
      compradoraId: usuario.id,
      vendedoraId: this.publicacion?.usuarioId,
      publicacionId: this.publicacion?.id
    };
    this.api.post<any>('http://localhost:5160/GestionMensajes/conversaciones', conversacion).subscribe({
      next: data => this.router.navigate(['/chat', data.id]),
      error: error => console.error('Error al iniciar conversacion', error)
    });
  }

  enviarReporte() {
    const usuario = JSON.parse(localStorage.getItem('usuario') || '{}');
    const reporte = {
      reportanteId: usuario.id,
      publicacionId: this.publicacion?.id,
      reportadaId: this.publicacion?.usuarioId,
      razon: this.razonReporte
    };
    this.api.post<any>(this.urlReportes, reporte).subscribe({
      next: () => { alert('Reporte enviado correctamente'); this.cerrarReporte(); },
      error: () => { alert('Reporte enviado correctamente'); this.cerrarReporte(); }
    });
  }
}
