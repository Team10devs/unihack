import {Component, OnInit, Renderer2} from '@angular/core';
import {NavigationEnd, Router, RouterOutlet} from '@angular/router';
import {RegisterPageComponent} from './register-page/register-page.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RegisterPageComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  constructor(private renderer: Renderer2, private router: Router) {}

  ngOnInit(): void {
    this.loadScript('https://cdn.botpress.cloud/webchat/v2.2/inject.js', () => {
      this.loadScript('https://files.bpcontent.cloud/2024/11/09/08/20241109084554-UXGPSC7L.js');
    });
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
}
