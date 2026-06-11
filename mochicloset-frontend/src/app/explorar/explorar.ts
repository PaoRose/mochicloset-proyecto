import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ApiClient } from '../core/http/api-client';

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
  imports: [RouterLink, CommonModule],
  templateUrl: './explorar.html',
  styleUrl: './explorar.css'
})
export class Explorar {
  private api = inject(ApiClient);
  private url = 'http://localhost:5160/GestionPublicaciones';

  publicaciones: Publicacion[] = [];
  filtrosAbiertos = false;

  ngOnInit() {
    this.cargarPublicaciones();
  }

  private cargarPublicaciones() {
    this.api.get<Publicacion[]>(this.url + '/filtrar').subscribe({
      next: data => this.publicaciones = data,
      error: error => console.error('Error al cargar publicaciones', error)
    });
  }

  abrirFiltros() {
    this.filtrosAbiertos = true;
  }

  cerrarFiltros() {
    this.filtrosAbiertos = false;
  }
}
