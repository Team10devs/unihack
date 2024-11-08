import {Component, OnInit} from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {DoctorPageComponent} from './doctor-page/doctor-page.component';
import {MenuItem} from 'primeng/api';
import {TabMenuModule} from 'primeng/tabmenu';
import {CalendarPageComponent} from './calendar-page/calendar-page.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, DoctorPageComponent, TabMenuModule, CalendarPageComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit{
  items: MenuItem[] =[];

  ngOnInit() {
    this.items = [
      {label: 'Home', icon: 'pi pi-fw pi-home'},
      {label: 'Calendar', icon: 'pi pi-fw pi-calendar',routerLink : 'calendar-page'},
      {label: 'Profile', icon: 'pi pi-fw pi pi-user' }
    ];
  }
}
