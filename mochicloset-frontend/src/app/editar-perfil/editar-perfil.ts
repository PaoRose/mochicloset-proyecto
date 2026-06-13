import { Component, inject, OnInit } from '@angular/core';
import { RouterLink, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-editar-perfil',
  standalone: true,
  imports: [RouterLink, FormsModule, CommonModule],
  templateUrl: './editar-perfil.html',
  styleUrl: './editar-perfil.css'
})
export class EditarPerfil implements OnInit {
  private http = inject(HttpClient);
  private router = inject(Router);
  private url = 'http://localhost:5160/GestionUsuarios';

  usuario: any = null;

  ngOnInit() {
    this.usuario = JSON.parse(localStorage.getItem('usuario') || '{}');
  }

  guardarCambios() {
    this.http.put(this.url + '/editar-perfil', this.usuario, { responseType: 'text' }).subscribe({
      next: () => {
        localStorage.setItem('usuario', JSON.stringify(this.usuario));
        this.router.navigate(['/perfil']);
      },
      error: (err) => alert(err.error || 'Error al guardar cambios.')
    });
  }
}
