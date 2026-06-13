import { Component, inject } from '@angular/core';
import { RouterLink, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ApiClient } from '../core/http/api-client';

interface UsuarioLogin {
  email: string;
  password: string;
}

interface UsuarioRespuesta {
  id: number;
  nombre: string;
  email: string;
  rol: string;
}

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [RouterLink, FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {
  private api = inject(ApiClient);
  private router = inject(Router);
  private url = 'http://localhost:5160/GestionUsuarios';

  usuario: UsuarioLogin = {
    email: '',
    password: ''
  };

  iniciarSesion() {
    console.log('intentando iniciar sesión con:', this.usuario);
    this.api.post<UsuarioRespuesta>(this.url + '/iniciar-sesion', this.usuario).subscribe({
      next: data => {
        localStorage.setItem('usuario', JSON.stringify(data));
        this.router.navigate(['/home']);
      },
      error: () => alert('Correo o contraseña incorrectos.')
    });
  }
}
