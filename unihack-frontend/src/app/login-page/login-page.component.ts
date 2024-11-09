import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {MatCardActions, MatCardContent, MatCardHeader} from '@angular/material/card';
import {MatFormField} from '@angular/material/form-field';
import {MatInput} from '@angular/material/input';
import {NgIf} from '@angular/common';
import {MatButton} from '@angular/material/button';
import {MatError} from '@angular/material/form-field';
import {MatLabel} from '@angular/material/form-field';
import {MatCard} from '@angular/material/card';
import {LoginPageService} from './login-page.service';
import {HttpClientModule} from '@angular/common/http';

@Component({
  selector: 'app-login-page',
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
    HttpClientModule,
  ],
  providers:[LoginPageService],
  templateUrl: './login-page.component.html',
  styleUrl: './login-page.component.scss'
})
export class LoginPageComponent {
  loginForm: FormGroup;
  logoUrl: string = '';

  constructor(private formBuilder: FormBuilder,private loginPageService : LoginPageService) {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  onSubmit(): void {
    if (this.loginForm.valid) {
      console.log('Login form submitted');
      this.loginPageService.login(this.loginForm.get(['email'])?.value,this.loginForm.get(['password'])?.value).subscribe(temp => console.log(temp))

    } else {
      return;
    }
  }

}
