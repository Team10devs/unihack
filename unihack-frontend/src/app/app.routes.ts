import { Routes } from '@angular/router';
import {LoginPageComponent} from './login-page/login-page.component';
import {RegisterPageComponent} from './register-page/register-page.component';

export const routes: Routes = [

  {
    path:"register",
    component : RegisterPageComponent
  },
  {
    path:"**",
    component : LoginPageComponent
  },
];
