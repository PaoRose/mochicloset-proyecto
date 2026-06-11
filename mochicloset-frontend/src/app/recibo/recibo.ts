import { Component, inject } from '@angular/core';
import { RouterLink, ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ApiClient } from '../core/http/api-client';

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
    precio: number;
    imagenUrl: string;
    talla: string;
    condicion: string;
  };
}

@Component({
  selector: 'app-recibo',
  imports: [RouterLink, CommonModule],
  templateUrl: './recibo.html',
  styleUrl: './recibo.css'
})
export class Recibo {
  private api = inject(ApiClient);
  private route = inject(ActivatedRoute);
  private url = 'http://localhost:5160/GestionCompras';

  compra: Compra | null = null;

  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.api.get<Compra>(this.url + '/' + id).subscribe({
      next: data => this.compra = data,
      error: error => console.error('Error al cargar recibo', error)
    });
  }

  imprimir() {
    window.print();
  }
}
