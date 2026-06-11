import { Component, inject } from '@angular/core';
import { RouterLink, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

interface UsuarioRegistro {
  nombre: string;
  username: string;
  telefono: string;
  email: string;
  password: string;
}

@Component({
  selector: 'app-registro',
  imports: [RouterLink, FormsModule],
  templateUrl: './registro.html',
  styleUrl: './registro.css',
})
export class Registro {
  private http = inject(HttpClient);
  private router = inject(Router);
  private url = 'http://localhost:5160/GestionUsuarios';

  usuario: UsuarioRegistro = {
    nombre: '',
    username: '',
    telefono: '',
    email: '',
    password: ''
  };

  confirmarPassword = '';

  registrar() {
    if (!this.usuario.nombre || !this.usuario.username || !this.usuario.email || !this.usuario.password || !this.usuario.telefono) {
      alert('Por favor completa todos los campos.');
      return;
    }

    if (this.usuario.password.length < 8) {
      alert('La contraseña debe tener al menos 8 caracteres.');
      return;
    }

    if (this.usuario.password !== this.confirmarPassword) {
      alert('Las contraseñas no coinciden.');
      return;
    }

    this.http.post(this.url + '/registrar-usuario', this.usuario, { responseType: 'text' }).subscribe({
      next: () => this.router.navigate(['/login']),
      error: (err) => alert(err.error || 'Error al registrar. Intenta de nuevo.')
    });
  }
}
