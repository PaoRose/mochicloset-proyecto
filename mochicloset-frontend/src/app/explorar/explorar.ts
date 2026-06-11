import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiClient } from '../core/http/api-client';
import { RouterLink, Router } from '@angular/router';

interface Publicacion {
  id: number;
  titulo: string;
  precio: number;
  talla: string;
  condicion: string;
  imagenUrl: string;
  usuarioId: number;
  categoriaId: number;
}

@Component({
  selector: 'app-explorar',
  imports: [RouterLink, CommonModule, FormsModule],
  templateUrl: './explorar.html',
  styleUrl: './explorar.css'
})
export class Explorar {
  private api = inject(ApiClient);
  private url = 'http://localhost:5160/GestionPublicaciones';
  private router = inject(Router);

  publicaciones: Publicacion[] = [];
  filtrosAbiertos = false;
  busqueda: string = '';
  categoriaId: number | null = null;
  talla: string = '';
  condicion: string = '';
  precioMax: number = 500;
  orden: string = 'recientes';
  favoritosIds: number[] = [];

  ngOnInit() {
    this.cargarPublicaciones();
    this.cargarFavoritos();
  }

  cargarPublicaciones() {
    let params = '?';
    if (this.busqueda) params += `busqueda=${this.busqueda}&`;
    if (this.categoriaId) params += `categoriaId=${this.categoriaId}&`;
    if (this.talla) params += `talla=${this.talla}&`;
    if (this.condicion) params += `condicion=${this.condicion}&`;
    if (this.precioMax < 500) params += `precioMax=${this.precioMax}&`;

    this.api.get<Publicacion[]>(this.url + '/filtrar' + params).subscribe({
      next: data => {
        if (this.orden === 'precio-asc') {
          this.publicaciones = data.sort((a, b) => a.precio - b.precio);
        } else if (this.orden === 'precio-desc') {
          this.publicaciones = data.sort((a, b) => b.precio - a.precio);
        } else {
          this.publicaciones = data;
        }
      },
      error: error => console.error('Error al cargar publicaciones', error)
    });
  }

  private cargarFavoritos() {
    const usuario = JSON.parse(localStorage.getItem('usuario') || '{}');
    if (!usuario.id) return;
    this.api.get<any[]>('http://localhost:5160/GestionFavoritos/' + usuario.id).subscribe({
      next: data => this.favoritosIds = data.map(f => f.publicacionId),
      error: error => console.error('Error al cargar favoritos', error)
    });
  }

  agregarFavorito(publicacionId: number, event: Event) {
    event.stopPropagation();
    const usuario = JSON.parse(localStorage.getItem('usuario') || '{}');
    if (!usuario.id) return;

    const favorito = { usuarioId: usuario.id, publicacionId: publicacionId };

    this.api.post('http://localhost:5160/GestionFavoritos', favorito).subscribe({
      next: () => this.favoritosIds.push(publicacionId),
      error: (error) => {
        if (error.status === 200) {
          this.favoritosIds.push(publicacionId);
        }
      }
    });
  }

  verPrenda(id: number) {
    this.router.navigate(['/prenda', id]);
  }

  aplicarFiltros() {
    this.cargarPublicaciones();
    this.cerrarFiltros();
  }

  limpiarFiltros() {
    this.busqueda = '';
    this.categoriaId = null;
    this.talla = '';
    this.condicion = '';
    this.precioMax = 500;
    this.cargarPublicaciones();
  }

  filtrarPorCategoria(id: number | null) {
    this.categoriaId = id;
    this.cargarPublicaciones();
  }

  abrirFiltros() {
    this.filtrosAbiertos = true;
  }

  cerrarFiltros() {
    this.filtrosAbiertos = false;
  }
}
