import {ApplicationConfig, provideZoneChangeDetection} from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideAnimations } from '@angular/platform-browser/animations';
import {provideFirebaseApp} from '@angular/fire/app';
import {getFirestore, provideFirestore} from '@angular/fire/firestore';
import {getMessaging, provideMessaging} from '@angular/fire/messaging';
import {initializeApp} from 'firebase/app';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideAnimationsAsync(),
    provideAnimations(),
    provideFirebaseApp(() => initializeApp(firebaseConfig)),
    provideFirestore(() => getFirestore()),
    provideMessaging(() => getMessaging())
  ]
};

const firebaseConfig = {
  apiKey: "AIzaSyDBdlDG9J_SChYN3Ns_obKwS8ebcnxjCSc",
  authDomain: "unihack-a1d2f.firebaseapp.com",
  projectId: "unihack-a1d2f",
  storageBucket: "unihack-a1d2f.firebasestorage.app",
  messagingSenderId: "734646451071",
  appId: "1:734646451071:web:a533d192d9577af04a784a",
  measurementId: "G-2L8G6RBRDN"
};



