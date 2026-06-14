import { Component, inject, OnDestroy } from '@angular/core';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ApiClient } from '../core/http/api-client';

@Component({
  selector: 'app-nav-menu',
  standalone: true,
  imports: [RouterLink, CommonModule],
  templateUrl: './nav-menu.html',
  styleUrl: './nav-menu.css'
})
export class NavMenu implements OnDestroy {
  private api = inject(ApiClient);
  private url = 'http://localhost:5160/GestionNotificaciones';
  private intervalo: any;

  notificacionesNoLeidas: number = 0;

  ngOnInit() {
    this.cargarNotificaciones();
    this.intervalo = setInterval(() => this.cargarNotificaciones(), 500);
  }

  ngOnDestroy() {
    if (this.intervalo) clearInterval(this.intervalo);
  }

  private cargarNotificaciones() {
    const usuario = JSON.parse(localStorage.getItem('usuario') || '{}');
    if (!usuario.id) return;
    this.api.get<any[]>(this.url + '/' + usuario.id).subscribe({
      next: data => this.notificacionesNoLeidas = data.filter(n => !n.leida).length,
      error: () => {}
    });
  }
}
