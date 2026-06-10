import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-perfil',
  imports: [CommonModule, RouterLink],
  templateUrl: './perfil.html',
  styleUrl: './perfil.css'
})
export class Perfil {
  tabActiva = 'publicaciones';
  modalAbierto = false;
  articuloEditando: any = null;

  cambiarTab(tab: string) {
    this.tabActiva = tab;
  }

  abrirModal(articulo: any) {
    this.articuloEditando = articulo;
    this.modalAbierto = true;
  }

  cerrarModal() {
    this.modalAbierto = false;
    this.articuloEditando = null;
  }
}
