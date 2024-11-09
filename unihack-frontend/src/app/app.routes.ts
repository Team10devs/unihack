import { Routes } from '@angular/router';
import {LoginPageComponent} from './login-page/login-page.component';
import {DoctorPageComponent} from './doctor-page/doctor-page.component';
import {CalendarPageComponent} from './calendar-page/calendar-page.component';

export const routes: Routes = [
  {
    path:"doctor-page",
    component : DoctorPageComponent,
  },
  {
    path:"calendar-page",
    component: CalendarPageComponent,
  },
  {
    path:"**",
    component : LoginPageComponent
  }
];
