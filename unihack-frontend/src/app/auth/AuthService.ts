import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { getAuth, onAuthStateChanged, User } from 'firebase/auth';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private auth = getAuth();
  private userSubject = new BehaviorSubject<User | null>(null);

  // Observable that components can subscribe to
  user$ = this.userSubject.asObservable();

  constructor() {
    // Listen for authentication state changes
    onAuthStateChanged(this.auth, (user) => {
      this.userSubject.next(user); // Set the current user in the BehaviorSubject
    });
  }

  // Method to get the current user (if needed immediately)
  getCurrentUser(): User | null {
    return this.auth.currentUser;
  }

  // Optional: method for signing out
  signOut() {
    return this.auth.signOut();
  }
}
