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


export const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'login', component: Login },
  { path: 'home', component: Home },
  { path: 'registro', component: Registro },
  { path: 'perfil', component: Perfil },
  { path: 'notificaciones', component: Notificaciones },
  { path: 'publicar', component: Publicar },
  { path: 'editar-perfil', component: EditarPerfil },
  { path: 'explorar', component: Explorar },
  { path: 'prenda/:id', component: DetallePrenda },
  { path: 'chat', component: Chat }
];
