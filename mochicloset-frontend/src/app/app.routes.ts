import { Routes } from '@angular/router';
import { Login } from './login/login';
import { Home } from './home/home';
import { Registro } from './registro/registro';
import { Perfil } from './perfil/perfil';
import { Notificaciones } from './notificaciones/notificaciones';
import { Publicar } from './publicar/publicar';
import { EditarPerfil } from './editar-perfil/editar-perfil';
import { Explorar } from './explorar/explorar';
import { DetallePrenda } from './detalle-prenda/detalle-prenda';
import { Chat } from './chat/chat';
import {Recibo} from './recibo/recibo';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'login', component: Login },
  { path: 'registro', component: Registro },
  { path: 'home', component: Home },
  { path: 'explorar', component: Explorar, canActivate: [authGuard] },
  { path: 'perfil', component: Perfil, canActivate: [authGuard] },
  { path: 'editar-perfil', component: EditarPerfil, canActivate: [authGuard] },
  { path: 'publicar', component: Publicar, canActivate: [authGuard] },
  { path: 'notificaciones', component: Notificaciones, canActivate: [authGuard] },
  { path: 'prenda/:id', component: DetallePrenda, canActivate: [authGuard] },
  { path: 'chat', component: Chat, canActivate: [authGuard] }
];
