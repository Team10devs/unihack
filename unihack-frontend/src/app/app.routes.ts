import { Routes } from '@angular/router';
import {LoginPageComponent} from './login-page/login-page.component';
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
    path:"**",
    component : LoginPageComponent
  },
];
