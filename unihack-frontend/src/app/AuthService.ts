import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private userIdSubject = new BehaviorSubject<string | null>(localStorage.getItem('userUID'));
  userId$ = this.userIdSubject.asObservable();

  setUserId(userId: string): void {
    localStorage.setItem('userUID', userId);
    this.userIdSubject.next(userId);
  }

  clearUserId(): void {
    localStorage.removeItem('userUID');
    this.userIdSubject.next(null);
  }
}
