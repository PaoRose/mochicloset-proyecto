import { Component, inject, OnDestroy } from '@angular/core';
import { RouterLink, ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiClient } from '../core/http/api-client';

interface Mensaje {
  id: number;
  conversacionId: number;
  remitenteId: number;
  texto: string;
  fechaEnvio: string;
}

interface Conversacion {
  id: number;
  compradoraId: number;
  vendedoraId: number;
  publicacionId: number;
  publicacion: {
    id: number;
    titulo: string;
    precio: number;
    imagenUrl: string;
  };
}

interface Usuario {
  id: number;
  nombre: string;
  username: string;
  fotoPerfil: string;
}

@Component({
  selector: 'app-chat',
  imports: [RouterLink, CommonModule, FormsModule],
  templateUrl: './chat.html',
  styleUrl: './chat.css',
})
export class Chat implements OnDestroy {
  private api = inject(ApiClient);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private url = 'http://localhost:5160/GestionMensajes';
  private urlUsuarios = 'http://localhost:5160/GestionUsuarios';
  private urlCompras = 'http://localhost:5160/GestionCompras';
  private intervalo: any;

  mensajes: Mensaje[] = [];
  conversacion: Conversacion | null = null;
  otraPersona: Usuario | null = null;
  nuevoMensaje: string = '';
  usuarioId: number = 0;
  conversacionId: number = 0;
  esCompradora: boolean = false;

  ngOnInit() {
    const usuario = JSON.parse(localStorage.getItem('usuario') || '{}');
    this.usuarioId = usuario.id;
    this.conversacionId = Number(this.route.snapshot.paramMap.get('id'));

    this.api.get<Conversacion[]>(this.url + '/conversaciones/' + this.usuarioId).subscribe({
      next: data => {
        this.conversacion = data.find(c => c.id === this.conversacionId) || null;
        if (this.conversacion) {
          this.esCompradora = this.conversacion.compradoraId === this.usuarioId;
          const otraId = this.esCompradora ? this.conversacion.vendedoraId : this.conversacion.compradoraId;
          this.api.get<Usuario>(this.urlUsuarios + '/' + otraId).subscribe({
            next: user => this.otraPersona = user,
            error: () => {}
          });
        }
      },
      error: error => console.error('Error al cargar conversacion', error)
    });

    this.cargarMensajes();
    this.intervalo = setInterval(() => this.cargarMensajes(), 3000);
  }

  ngOnDestroy() {
    if (this.intervalo) clearInterval(this.intervalo);
  }

  private cargarMensajes() {
    this.api.get<Mensaje[]>(this.url + '/mensajes/' + this.conversacionId).subscribe({
      next: data => this.mensajes = data,
      error: error => console.error('Error al cargar mensajes', error)
    });
  }

  enviarMensaje() {
    if (!this.nuevoMensaje.trim()) return;

    const mensaje = {
      conversacionId: this.conversacionId,
      remitenteId: this.usuarioId,
      texto: this.nuevoMensaje
    };

    this.api.post<{mensaje: string}>(this.url + '/mensajes', mensaje).subscribe({
      next: () => {
        this.nuevoMensaje = '';
        this.cargarMensajes();
      },
      error: (error) => {
        if (error.status === 200) {
          this.nuevoMensaje = '';
          this.cargarMensajes();
        } else {
          console.error('Error al enviar mensaje', error);
        }
      }
    });
  }

  irAPrenda() {
    this.router.navigate(['/prenda', this.conversacion?.publicacionId]);
  }

  confirmarVenta() {
    const compra = {
      usuarioId: this.conversacion?.compradoraId,
      publicacionId: this.conversacion?.publicacionId
    };
    this.api.post<any>(this.urlCompras, compra).subscribe({
      next: () => alert('¡Venta registrada exitosamente!'),
      error: (error) => {
        if (error.status === 200) alert('¡Venta registrada exitosamente!');
        else console.error('Error al registrar venta', error);
      }
    });
  }
}
