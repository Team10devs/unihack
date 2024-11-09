import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatCard, MatCardHeader, MatCardContent, MatCardActions } from '@angular/material/card';
import {MatFormField, MatError, MatLabel, MatFormFieldModule} from '@angular/material/form-field';
import {MatInput, MatInputModule} from '@angular/material/input';
import { NgIf } from '@angular/common';
import { MatButton } from '@angular/material/button';
import { MatCheckbox } from '@angular/material/checkbox';
import { RegisterPageService } from './register-page.service';
import { HttpClientModule } from '@angular/common/http';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import {
  MatDatepicker,
  MatDatepickerInput,
  MatDatepickerToggle
} from '@angular/material/datepicker';

@Component({
  selector: 'app-register-page',
  standalone: true,
  imports: [
    MatInputModule,
    MatFormFieldModule,
    MatCard,
    ReactiveFormsModule,
    MatCardHeader,
    MatCardContent,
    MatFormField,
    MatInput,
    NgIf,
    MatCardActions,
    MatButton,
    MatError,
    MatLabel,
    MatCheckbox,
    HttpClientModule,
    MatDatepickerInput,
    MatDatepickerToggle,
    MatDatepicker,
    MatNativeDateModule,
    MatDatepickerModule,

  ],
  providers: [RegisterPageService],
  templateUrl: './register-page.component.html',
  styleUrls: ['./register-page.component.css']
})
export class RegisterPageComponent {
  registerForm: FormGroup;

  constructor(private formBuilder: FormBuilder, private registerPageService: RegisterPageService) {
    this.registerForm = this.formBuilder.group({
      fullName: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      birthDate: [null, [Validators.required]], // Initialize as null
      doctorId: [''],
      registerAsDoctor: [false],
      address: [''],
      specialization: [''],
      diploma: ['']
    });

    this.registerForm.get('registerAsDoctor')?.valueChanges.subscribe(isDoctor => {
      if (isDoctor) {
        this.registerForm.get('address')?.setValidators([Validators.required]);
        this.registerForm.get('specialization')?.setValidators([Validators.required]);
        this.registerForm.get('diploma')?.setValidators([Validators.required]);
      } else {
        this.registerForm.get('address')?.clearValidators();
        this.registerForm.get('specialization')?.clearValidators();
        this.registerForm.get('diploma')?.clearValidators();
      }
      this.registerForm.get('address')?.updateValueAndValidity();
      this.registerForm.get('specialization')?.updateValueAndValidity();
      this.registerForm.get('diploma')?.updateValueAndValidity();
    });
  }

  onSubmit(): void {
    if (this.registerForm.valid) {
      const formData = this.registerForm.value;

      // Ensure birthDate is properly formatted
      const birthDate = formData.birthDate instanceof Date ?
        formData.birthDate.toISOString().split('T')[0] : // Get only the date part YYYY-MM-DD
        null;

      console.log('Birth Date:', birthDate); // Debug log

      if (formData.registerAsDoctor) {
        // Doctor registration data mapping
        const doctorData = {
          email: formData.email,
          password: formData.password,
          fullname: formData.fullName,
          birthDate: birthDate,
          license: formData.diploma,
          address: formData.address,
          specialization: formData.specialization
        };

        console.log('Doctor Data:', doctorData); // Debug log

        this.registerPageService.registerDoctor(doctorData).subscribe({
          next: (response) => {
            console.log('Doctor registered successfully', response);
          },
          error: (error) => {
            console.error('Error registering doctor', error);
          }
        });

      } else {
        // Patient registration data mapping
        const patientData = {
          email: formData.email,
          password: formData.password,
          fullname: formData.fullName,
          birthDate: birthDate,
          doctorCode: formData.doctorId || null // Make sure doctorId is null if empty
        };

        console.log('Patient Data:', patientData); // Debug log

        this.registerPageService.registerPatient(patientData).subscribe({
          next: (response) => {
            console.log('Patient registered successfully', response);
          },
          error: (error) => {
            console.error('Error registering patient', error);
          }
        });
      }
    } else {
      // Log which form controls are invalid
      Object.keys(this.registerForm.controls).forEach(key => {
        const control = this.registerForm.get(key);
        if (control?.invalid) {
          console.log(`${key} is invalid:`, control.errors);
        }
      });
      console.log('Form is invalid');
    }
  }
}
