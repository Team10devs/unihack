import { Component } from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import { MatCard, MatCardHeader, MatCardContent, MatCardActions } from '@angular/material/card';
import { MatFormField, MatError, MatLabel } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { NgIf } from '@angular/common';
import { MatButton } from '@angular/material/button';
import { MatCheckbox } from '@angular/material/checkbox';

@Component({
  selector: 'app-register-page',
  standalone: true,
  imports: [
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
    MatCheckbox
  ],
  templateUrl: './register-page.component.html',
  styleUrls: ['./register-page.component.css']
})
export class RegisterPageComponent {
  registerForm: FormGroup;

  constructor(private formBuilder: FormBuilder) {
    this.registerForm = this.formBuilder.group({
      fullName: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      registerAsDoctor: [false],
      address: [''], // Initialize doctor-specific fields
      specialization: [''],
      diploma: ['']
    });

    // Add validators conditionally based on checkbox state
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
      console.log('Register form submitted:', formData);

      if (formData.registerAsDoctor) {
        console.log('User chose to register as a doctor:', {
          address: formData.address,
          specialization: formData.specialization,
          diploma: formData.diploma
        });
      } else {
        console.log('User chose to register as a patient.');
      }
    } else {
      console.log('Form is invalid');
    }
  }
}
