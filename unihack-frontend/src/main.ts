import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { initializeApp } from '@angular/fire/app';
import { environments } from './app/environment/environment';
import {appConfig} from './app/app.config';


bootstrapApplication(AppComponent, appConfig)
  .catch((err) => console.error(err));
const firebaseConfig = environments.firebaseConfig;
//const app = initializeApp(firebaseConfig);
