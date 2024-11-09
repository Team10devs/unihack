import {Component, OnInit} from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {DoctorPageComponent} from './doctor-page/doctor-page.component';
import {MenuItem} from 'primeng/api';
import {TabMenuModule} from 'primeng/tabmenu';
import {CalendarPageComponent} from './calendar-page/calendar-page.component';
import {style} from '@angular/animations';
import {ChatAppComponent} from './chat-app/chat-app.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, DoctorPageComponent, TabMenuModule, CalendarPageComponent, ChatAppComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit{
  items: MenuItem[] =[];

  ngOnInit() {
    this.items = [
      {label: 'Home', icon: 'pi pi-fw pi-home'},
      {label: 'Calendar', icon: 'pi pi-fw pi-calendar',routerLink : 'calendar-page'},
      {label: 'Patient List', icon: 'pi pi-fw pi-calendar', routerLink: 'patient-page',
      },
      {label: 'Profile', icon: 'pi pi-fw pi pi-user' }
    ];
  }
}
