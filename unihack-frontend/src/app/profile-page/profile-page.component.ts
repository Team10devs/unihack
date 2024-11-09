import { Component, OnInit, Renderer2 } from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {MatIcon} from '@angular/material/icon';
import {MatCard, MatCardContent, MatCardTitle} from '@angular/material/card';
import {MatFormField} from '@angular/material/form-field';
import {MatOption, MatSelect} from '@angular/material/select';
import {MatDatepicker, MatDatepickerInput, MatDatepickerToggle} from '@angular/material/datepicker';
import {MatInput} from '@angular/material/input';
import {MatButton} from '@angular/material/button';
import {MatLabel} from '@angular/material/form-field';
import {MAT_DATE_FORMATS, MAT_DATE_LOCALE} from '@angular/material/core';
import {AppComponent} from '../app.component';
import {MatNativeDateModule, NativeDateAdapter, DateAdapter} from '@angular/material/core';
import {NgIf, NgOptimizedImage} from '@angular/common';
import {RouterOutlet} from '@angular/router';

export const CUSTOM_DATE_FORMATS = {
  parse: {
    dateInput: 'LL',
  },
  display: {
    dateInput: 'YYYY-MM-DD',
    monthYearLabel: 'MMM YYYY',
    dateA11yLabel: 'LL',
    monthYearA11yLabel: 'MMMM YYYY',
  },
};

@Component({
  standalone: true,
  selector: 'app-profile-page',
  templateUrl: './profile-page.component.html',
  imports: [
    MatIcon,
    MatCardContent,
    MatCard,
    MatCardTitle,
    MatFormField,
    MatSelect,
    MatOption,
    MatDatepicker,
    MatDatepickerToggle,
    MatDatepickerInput,
    ReactiveFormsModule,
    MatInput,
    MatButton,
    MatLabel,
    AppComponent,
    MatNativeDateModule,
    NgOptimizedImage,
    NgIf,
    RouterOutlet
  ],
  styleUrls: ['./profile-page.component.scss'],
  providers: [
    { provide: MAT_DATE_LOCALE, useValue: 'en-GB' }, // Change 'en-GB' to your desired locale (e.g., 'en-US')
    { provide: MAT_DATE_FORMATS, useValue: CUSTOM_DATE_FORMATS }
  ]
})
export class ProfilePageComponent {
  profileForm: FormGroup;
  imagePreview: string | ArrayBuffer | null = null;

  constructor(private formBuilder: FormBuilder, private renderer: Renderer2) {
    this.profileForm = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      department: ['', Validators.required],
      position: ['', Validators.required],
      hiredDate: ['', Validators.required],
      birthDate: ['', Validators.required]
    });
  }


  onSave(): void {
    if (this.profileForm.valid) {
      console.log('Form data:', this.profileForm.value);
      // Add logic to save the form data
    }
  }

  onCancel(): void {
    // Add logic to handle the cancel action
  }


onFileSelected(event: Event): void {
  const fileInput = event.target as HTMLInputElement;
  if (fileInput.files && fileInput?.files[0]) {
  const file = fileInput?.files[0];
  const reader = new FileReader();

  reader.onload = (e) => {
    this.imagePreview = reader.result;
  };

  reader.readAsDataURL(file); // Read the file as a data URL
}
}
}
