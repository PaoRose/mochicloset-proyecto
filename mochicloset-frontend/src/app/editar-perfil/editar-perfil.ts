import { Component, inject } from '@angular/core';
import { RouterLink, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ApiClient } from '../core/http/api-client';

@Component({
  selector: 'app-editar-perfil',
  imports: [RouterLink, FormsModule, CommonModule],
  templateUrl: './editar-perfil.html',
  styleUrl: './editar-perfil.css'
})
export class EditarPerfil {
  private api = inject(ApiClient);
  private router = inject(Router);
  private url = 'http://localhost:5160/GestionUsuarios';

  usuario: any = null;

  ngOnInit() {
    this.usuario = JSON.parse(localStorage.getItem('usuario') || '{}');
  }

  guardarCambios() {
    this.api.put<any>(this.url + '/editar-perfil', this.usuario).subscribe({
      next: () => {
        localStorage.setItem('usuario', JSON.stringify(this.usuario));
        this.router.navigate(['/perfil']);
      },
      error: (error) => {
        if (error.status === 200) {
          localStorage.setItem('usuario', JSON.stringify(this.usuario));
          this.router.navigate(['/perfil']);
        } else {
          console.error('Error al guardar', error);
        }
      }
    });
  }
}
