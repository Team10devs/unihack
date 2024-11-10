import {Component, OnInit, Renderer2} from '@angular/core';
import {NavigationEnd, Router, RouterOutlet} from '@angular/router';
import {DoctorPageComponent} from './doctor-page/doctor-page.component';
import {MenuItem} from 'primeng/api';
import {TabMenuModule} from 'primeng/tabmenu';
import {CalendarPageComponent} from './calendar-page/calendar-page.component';
import {style} from '@angular/animations';
import {ChatAppComponent} from './chat-app/chat-app.component';
import {RegisterPageComponent} from './register-page/register-page.component';
import {LoginPageComponent} from './login-page/login-page.component';
import {HttpClient, HttpClientModule} from '@angular/common/http';
import {AuthService} from './AuthService';
import {BehaviorSubject} from 'rxjs';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [HttpClientModule,RouterOutlet, DoctorPageComponent, TabMenuModule, CalendarPageComponent, ChatAppComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  providers:[LoginPageComponent,HttpClient,HttpClientModule]
})
export class AppComponent implements OnInit {
  constructor(private renderer: Renderer2, private router: Router, private auth:AuthService) {}

userId :string | null  = null;
userRole : string | null = null;
items: MenuItem[] = [];

  ngOnInit(): void {
    this.userId = localStorage.getItem('userUID');

    this.auth.userRole$.subscribe(temp=>{
        this.userRole =temp;
        this.setMenuItems();
    });

    this.auth.userId$.subscribe((id) => {
      this.userId = id;
    });
      this.loadScript('https://cdn.botpress.cloud/webchat/v2.2/inject.js', () => {
        this.loadScript('https://files.bpcontent.cloud/2024/11/09/08/20241109084554-UXGPSC7L.js');
      });


    this.items = [
      {label: 'Home', icon: 'pi pi-fw pi-home'},
      {label: 'Calendar', icon: 'pi pi-fw pi-calendar',routerLink : 'calendar-page'},
      {label: 'Patient List', icon: 'pi pi-fw pi-calendar', routerLink: 'patient-page',},
      {label: 'Profile', icon: 'pi pi-fw pi pi-user',routerLink:'profile-page' }
    ];
  }

  private loadScript(src: string, callback?: () => void): void {
    const script = this.renderer.createElement('script');
    script.src = src;
    script.type = 'text/javascript';
    script.async = true;
    script.onload = callback; // Call the callback when the script loads
    this.renderer.appendChild(document.body, script);
  }


  private loadChatbotScripts(): void {
    this.loadScript('https://cdn.botpress.cloud/webchat/v2.2/inject.js');
    this.loadScript('https://files.bpcontent.cloud/2024/11/09/08/20241109084554-UXGPSC7L.js');
  }
  items: MenuItem[] =[];

}
