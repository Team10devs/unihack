import { Routes } from '@angular/router';
import {LoginPageComponent} from './login-page/login-page.component';
import {DoctorPageComponent} from './doctor-page/doctor-page.component';
import {CalendarPageComponent} from './calendar-page/calendar-page.component';
import {PatientPageComponent} from './patient-page/patient-page.component';
import {PatientTableComponent} from './doctor-page/patient-table/patient-table.component';
import {RegisterPageComponent} from './register-page/register-page.component';
import {ProfilePageComponent} from './profile-page/profile-page.component';

export const routes: Routes = [

  {
    path:"register",
    component : RegisterPageComponent
  },
  {
    path:"profile-page",
    component : ProfilePageComponent
  },
  {
    path:"doctor-page",
    component : DoctorPageComponent,
  },
  {
    path:"calendar-page",
    component: CalendarPageComponent,
  },

  {
    path:"patient-page",
    component : PatientTableComponent,
    children: [
      {
        path: 'patient/:id',
        component: PatientPageComponent,
      }
    ]
  },
  {
    path:"**",
    component : LoginPageComponent
  },
];
