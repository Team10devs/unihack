import {Component, OnInit} from '@angular/core';
import {SidebarModule} from 'primeng/sidebar';
import {MenuModule} from 'primeng/menu';
import {ToolbarModule} from 'primeng/toolbar';
import {Button, ButtonDirective} from 'primeng/button';
import {CardModule} from 'primeng/card';
import {TabMenuModule} from 'primeng/tabmenu';
import {MenuItem} from 'primeng/api';



@Component({
  selector: 'app-doctor-page',
  standalone: true,
  imports: [
    SidebarModule,
    MenuModule,
    ToolbarModule,
    ButtonDirective,
    CardModule,
    Button,
    TabMenuModule
  ],
  templateUrl: './doctor-page.component.html',
  styleUrl: './doctor-page.component.css'
})
export class DoctorPageComponent {

}
