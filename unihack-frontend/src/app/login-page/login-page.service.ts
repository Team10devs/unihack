import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Injectable} from '@angular/core';
import {environments} from '../environment/environment';

@Injectable({
  providedIn: 'root'
})

export class LoginPageService{
   apiUrl = `${environments.apiUrl}/api/Auth/login`

  constructor(private http: HttpClient) { }

  login(email: string, password: string): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    const body = { email, password };

    return this.http.post<any>(this.apiUrl, body, { headers });
  }

}
