import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {environments} from '../environment/environment';

@Injectable({providedIn:"root"})

export class RegisterPageService{
  constructor(private http: HttpClient) {}

   baseUrl = `${environments.apiUrl}/api/Auth`

  registerDoctor(doctorData: any): Observable<any> {
    const url = `${this.baseUrl}/registerDoctor`;
    return this.http.post(url, doctorData);
  }

  registerPatient(patientData: any): Observable<any> {
    const url = `${this.baseUrl}/registerPatient`;
    return this.http.post(url, patientData);
  }
}
