import { Component, signal } from '@angular/core';
import { RouterOutlet, Router } from '@angular/router';
import { NavMenu } from './nav-menu/nav-menu';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, NavMenu, CommonModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('mochicloset-frontend');

  constructor(public router: Router) {}
}
