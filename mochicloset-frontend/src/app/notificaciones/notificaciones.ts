import { Component, inject, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApiClient } from '../core/http/api-client';

interface Notificacion {
  id: number;
  usuarioId: number;
  tipo: string;
  mensaje: string;
  fechaCreacion: string;
  leida: boolean;
}

@Component({
  selector: 'app-notificaciones',
  imports: [CommonModule],
  templateUrl: './notificaciones.html',
  styleUrl: './notificaciones.css',
})
export class Notificaciones implements OnDestroy{
  private api = inject(ApiClient);
  private url = 'http://localhost:5160/GestionNotificaciones';
  private intervalo: any;

  notificaciones: Notificacion[] = [];
  usuario: any = null;

  ngOnInit() {
    this.usuario = JSON.parse(localStorage.getItem('usuario') || '{}');
    this.cargarNotificaciones();
    this.intervalo = setInterval(() => this.cargarNotificaciones(), 3000);
  }

  private cargarNotificaciones() {
    this.api.get<Notificacion[]>(this.url + '/' + this.usuario.id).subscribe({
      next: data => this.notificaciones = data,
      error: error => console.error('Error al cargar notificaciones', error)
    });
  }

  marcarLeida(id: number) {
    this.api.put(this.url + '/marcar-leida/' + id, {}).subscribe({
      next: () => this.cargarNotificaciones(),
      error: () => this.cargarNotificaciones()
    });
  }
  ngOnDestroy() {
    if (this.intervalo) clearInterval(this.intervalo);
  }
}
