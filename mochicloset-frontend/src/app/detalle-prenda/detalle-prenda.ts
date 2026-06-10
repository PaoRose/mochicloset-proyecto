import { Component } from '@angular/core';
import { RouterLink, Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-detalle-prenda',
  imports: [RouterLink, CommonModule],
  templateUrl: './detalle-prenda.html',
  styleUrl: './detalle-prenda.css'
})
export class DetallePrenda {
  modalSeguridad = false;

  constructor(private router: Router) {}

  abrirModalSeguridad() {
    this.modalSeguridad = true;
  }

  cerrarModalSeguridad() {
    this.modalSeguridad = false;
  }

  irAlChat() {
    this.modalSeguridad = false;
    this.router.navigate(['/chat']);
  }
}
