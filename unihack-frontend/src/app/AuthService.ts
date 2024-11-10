import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private userIdSubject = new BehaviorSubject<string | null>(localStorage.getItem('userUID'));
  private userRole = new BehaviorSubject<string | null>(localStorage.getItem('userRole'));

  userId$ = this.userIdSubject.asObservable();
  userRole$ = this.userRole.asObservable();

  setUserRole$(userRole : string){
    localStorage.setItem('userRole', userRole);
    this.userRole.next(userRole);
  }

  setUserId(userId: string): void {
    localStorage.setItem('userUID', userId);
    this.userIdSubject.next(userId);
  }

  clearUserRole(): void {
    localStorage.removeItem('userRole');
    this.userIdSubject.next(null);
  }


  clearUserId(): void {
    localStorage.removeItem('userUID');
    this.userIdSubject.next(null);
  }
}
