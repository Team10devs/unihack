import {Component, Input} from '@angular/core';
import {IPatient} from '../models/IPatient';

@Component({
  selector: 'app-patient-page',
  standalone: true,
  imports: [],
  templateUrl: './patient-page.component.html',
  styleUrl: './patient-page.component.css'
})
export class PatientPageComponent {
@Input() patient !: IPatient;


}
