import { Component, inject } from '@angular/core';
import { RouterLink, Router, ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
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
}

@Component({
  selector: 'app-detalle-prenda',
  imports: [RouterLink, CommonModule],
  templateUrl: './detalle-prenda.html',
  styleUrl: './detalle-prenda.css'
})
export class DetallePrenda {
  private api = inject(ApiClient);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private url = 'http://localhost:5160/GestionPublicaciones';

  publicacion: Publicacion | null = null;
  modalSeguridad = false;

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
    this.api.get<Publicacion>(this.url + '/' + id).subscribe({
      next: data => this.publicacion = data,
      error: error => console.error('Error al cargar publicacion', error)
    });
  }

  abrirModalSeguridad() {
    this.modalSeguridad = true;
  }

  cerrarModalSeguridad() {
    this.modalSeguridad = false;
  }

  irAlChat() {
    this.modalSeguridad = false;
    const usuario = JSON.parse(localStorage.getItem('usuario') || '{}');

    const conversacion = {
      compradoraId: usuario.id,
      vendedoraId: this.publicacion?.usuarioId,
      publicacionId: this.publicacion?.id
    };

    this.api.post<any>('http://localhost:5160/GestionMensajes/conversaciones', conversacion).subscribe({
      next: data => {
        this.router.navigate(['/chat', data.id]);
      },
      error: error => console.error('Error al iniciar conversacion', error)
    });
  }
}
