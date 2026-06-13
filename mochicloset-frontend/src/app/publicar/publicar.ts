import { Component, inject, OnInit } from '@angular/core';
import { RouterLink, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ApiClient } from '../core/http/api-client';
import { HttpClient } from '@angular/common/http';

interface Categoria {
  id: number;
  nombre: string;
}

interface Publicacion {
  titulo: string;
  descripcion: string;
  precio: number;
  talla: string;
  condicion: string;
  usuarioId: number;
  categoriaId: number;
  imagenUrl: string;
}

@Component({
  selector: 'app-publicar',
  imports: [RouterLink, FormsModule, CommonModule],
  templateUrl: './publicar.html',
  styleUrl: './publicar.css'
})
export class Publicar implements OnInit {
  private api = inject(ApiClient);
  private http = inject(HttpClient);
  private router = inject(Router);
  private urlPublicaciones = 'http://localhost:5160/GestionPublicaciones';
  private urlCategorias = 'http://localhost:5160/GestionCategorias';


  categorias: Categoria[] = [];

  publicacion: Publicacion = {
    titulo: '',
    descripcion: '',
    precio: 0,
    talla: '',
    condicion: '',
    usuarioId: 0,
    categoriaId: 0,
    imagenUrl: ''
  };

  ngOnInit() {
    const usuario = JSON.parse(localStorage.getItem('usuario') || '{}');
    this.publicacion.usuarioId = usuario.id;

    this.api.get<Categoria[]>(this.urlCategorias + '/lista-categorias').subscribe({
      next: data => this.categorias = data,
      error: err => console.error('Error al cargar categorías', err)
    });
  }

  publicar() {
    if (!this.publicacion.titulo || !this.publicacion.precio || !this.publicacion.talla || !this.publicacion.condicion || !this.publicacion.categoriaId) {
      alert('Por favor completa todos los campos obligatorios.');
      return;
    }

    this.http.post(this.urlPublicaciones, this.publicacion, { responseType: 'text' }).subscribe({
      next: () => this.router.navigate(['/perfil']),
      error: (err) => alert(err.error || 'Error al publicar. Intenta de nuevo.')
    });
  }
}
