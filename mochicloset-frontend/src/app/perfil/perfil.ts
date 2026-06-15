import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ApiClient } from '../core/http/api-client';
import { CommonModule } from '@angular/common';
import { RouterLink, Router } from '@angular/router';

interface Publicacion {
  id: number;
  titulo: string;
  descripcion: string;
  precio: number;
  estado: string;
  imagenUrl: string;
  fechaPublicacion: string;
  talla: string;
  condicion: string;
  usuarioId: number;
  categoriaId: number;
}
interface Favorito {
  id: number;
  usuarioId: number;
  publicacionId: number;
  publicacion: {
    id: number;
    titulo: string;
    precio: number;
    imagenUrl: string;
  };
}
interface Compra {
  id: number;
  usuarioId: number;
  publicacionId: number;
  montoTotal: number;
  fechaCompra: string;
  estado: string;
  publicacion: {
    id: number;
    titulo: string;
  };
}
interface Conversacion {
  id: number;
  compradoraId: number;
  vendedoraId: number;
  publicacionId: number;
  publicacion: {
    id: number;
    titulo: string;
    estado: string;
  };
  compradora: {
    id: number;
    nombre: string;
  };
  vendedora: {
    id: number;
    nombre: string;
  };
}


@Component({
  selector: 'app-perfil',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  templateUrl: './perfil.html',
  styleUrl: './perfil.css'
})
export class Perfil {
  private api = inject(ApiClient);
  private url = 'http://localhost:5160/GestionPublicaciones';
  private urlFavoritos = 'http://localhost:5160/GestionFavoritos';
  private urlCompras = 'http://localhost:5160/GestionCompras';
  private urlMensajes = 'http://localhost:5160/GestionMensajes';
  private router = inject(Router);

  tabActiva = 'publicaciones';
  modalAbierto = false;
  articuloEditando: any = null;
  usuario: any = null;
  publicaciones: Publicacion[] = [];
  favoritos: Favorito[] = [];
  compras: Compra[] = [];
  ventas: Compra[] = [];
  conversaciones: Conversacion[] = [];
  comprasPublicacionIds: number[] = [];
  ventasMap: {[publicacionId: number]: number} = {};


  ngOnInit() {
    this.usuario = JSON.parse(localStorage.getItem('usuario') || '{}');
    this.cargarPublicaciones();
    this.cargarFavoritos();
    this.cargarCompras();
    this.cargarVentas();
    this.cargarConversaciones();
  }

  private cargarPublicaciones() {
    this.api.get<Publicacion[]>(this.url + '/mis-publicaciones/' + this.usuario.id).subscribe({
      next: data => this.publicaciones = data,
      error: error => console.error('Error al cargar publicaciones', error)
    });
  }

  private cargarFavoritos() {
    this.api.get<Favorito[]>(this.urlFavoritos + '/' + this.usuario.id).subscribe({
      next: data => this.favoritos = data,
      error: error => console.error('Error al cargar favoritos', error)
    });
  }

  private cargarCompras() {
    this.api.get<Compra[]>(this.urlCompras + '/mis-compras/' + this.usuario.id).subscribe({
      next: data => {
        this.compras = data;
        this.comprasPublicacionIds = data.map(c => c.publicacionId);
      },
      error: error => console.error('Error al cargar compras', error)
    });
  }

  private cargarVentas() {
    this.api.get<Compra[]>(this.urlCompras + '/mis-ventas/' + this.usuario.id).subscribe({
      next: data => {
        this.ventas = data;
        data.forEach(v => this.ventasMap[v.publicacionId] = v.usuarioId);
      },
      error: error => console.error('Error al cargar ventas', error)
    });
  }

  cambiarTab(tab: string) {
    this.tabActiva = tab;
  }

  abrirModal(p: Publicacion) {
    this.articuloEditando = {...p};
    this.modalAbierto = true;
  }

  abrirChat(conversacionId: number, publicacionId: number, vendedoraId: number, compradoraId: number) {
    const esVendedora = vendedoraId === this.usuario.id;
    const esCompradora = this.comprasPublicacionIds.includes(publicacionId);
    const publicacionVendida = this.conversaciones.find(c => c.publicacionId === publicacionId)?.publicacion?.estado === 'Vendido';

    if (!publicacionVendida) {
      this.router.navigate(['/chat', conversacionId]);
      return;
    }

    if (esVendedora && this.ventasMap[publicacionId] === compradoraId) {
      this.router.navigate(['/chat', conversacionId]);
      return;
    }

    if (!esVendedora && esCompradora) {
      this.router.navigate(['/chat', conversacionId]);
      return;
    }
  }

  cerrarModal() {
    this.modalAbierto = false;
    this.articuloEditando = null;
  }

  eliminarFavorito(publicacionId: number, event: Event) {
    event.stopPropagation();
    this.api.delete(this.urlFavoritos + '/' + this.usuario.id + '/' + publicacionId).subscribe({
      next: () => this.cargarFavoritos(),
      error: () => this.cargarFavoritos()
    });
  }

  eliminarPublicacion(p: Publicacion) {
    if (p.estado === 'Vendido') {
      alert('Esta prenda ya fue vendida y no puede eliminarse.');
      return;
    }
    this.api.delete(this.url + '/eliminar-propia/' + p.id + '?usuarioId=' + this.usuario.id).subscribe({
      next: () => this.cargarPublicaciones(),
      error: () => this.cargarPublicaciones()
    });
  }

  guardarCambios() {
    this.api.put<any>(this.url + '/' + this.articuloEditando.id, this.articuloEditando).subscribe({
      next: () => {
        this.cerrarModal();
        this.cargarPublicaciones();
      },
      error: (error) => {
        if (error.status === 200) {
          this.cerrarModal();
          this.cargarPublicaciones();
        } else {
          console.error('Error al guardar cambios', error);
        }
      }
    });
  }
  get totalVentas(): number {
    return this.ventas.reduce((sum, v) => sum + v.montoTotal, 0);
  }
  private cargarConversaciones() {
    this.api.get<Conversacion[]>(this.urlMensajes + '/conversaciones/' + this.usuario.id).subscribe({
      next: data => this.conversaciones = data,
      error: error => console.error('Error al cargar conversaciones', error)
    });
  }

}
