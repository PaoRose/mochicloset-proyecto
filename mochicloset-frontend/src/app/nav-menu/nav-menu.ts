import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  imports: [RouterLink],
  templateUrl: './nav-menu.html',
  styleUrl: './nav-menu.css'   // ← esta línea
})
export class NavMenu {}
