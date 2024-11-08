import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {RegisterPageComponent} from './register-page/register-page.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RegisterPageComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'unihack';
}
