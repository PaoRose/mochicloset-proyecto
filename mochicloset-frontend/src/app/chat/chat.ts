import { Component, inject } from '@angular/core';
import { RouterLink, ActivatedRoute } from '@angular/router';
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

@Component({
  selector: 'app-chat',
  imports: [RouterLink, CommonModule, FormsModule],
  templateUrl: './chat.html',
  styleUrl: './chat.css',
})
export class Chat {
  private api = inject(ApiClient);
  private route = inject(ActivatedRoute);
  private url = 'http://localhost:5160/GestionMensajes';

  mensajes: Mensaje[] = [];
  nuevoMensaje: string = '';
  usuarioId: number = 0;
  conversacionId: number = 0;

  ngOnInit() {
    const usuario = JSON.parse(localStorage.getItem('usuario') || '{}');
    this.usuarioId = usuario.id;
    this.conversacionId = Number(this.route.snapshot.paramMap.get('id'));
    this.cargarMensajes();
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
}
