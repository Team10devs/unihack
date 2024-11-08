import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {MatIcon} from '@angular/material/icon';
import {MatCard, MatCardContent, MatCardTitle} from '@angular/material/card';
import {MatFormField} from '@angular/material/form-field';
import {MatOption, MatSelect} from '@angular/material/select';
import {MatDatepicker, MatDatepickerInput, MatDatepickerToggle} from '@angular/material/datepicker';
import {MatInput} from '@angular/material/input';
import {MatButton} from '@angular/material/button';
import {MatLabel} from '@angular/material/form-field';

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
    MatLabel
  ],
  styleUrls: ['./profile-page.component.scss']
})
export class ProfilePageComponent implements OnInit {
  profileForm: FormGroup;

  constructor(private formBuilder: FormBuilder) {
    this.profileForm = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      department: ['', Validators.required],
      position: ['', Validators.required],
      hiredDate: ['', Validators.required],
      birthDate: ['', Validators.required]
    });
  }

  ngOnInit(): void {}

  onSave(): void {
    if (this.profileForm.valid) {
      console.log('Form data:', this.profileForm.value);
      // Add logic to save the form data
    }
  }

  onCancel(): void {
    // Add logic to handle the cancel action
  }
}
