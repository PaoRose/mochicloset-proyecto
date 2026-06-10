import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-explorar',
  imports: [RouterLink, CommonModule],
  templateUrl: './explorar.html',
  styleUrl: './explorar.css'
})
export class Explorar {
  filtrosAbiertos = false;

  abrirFiltros() {
    this.filtrosAbiertos = true;
  }

  cerrarFiltros() {
    this.filtrosAbiertos = false;
  }
}
