import { Component } from '@angular/core';
import { AsyncPipe, CommonModule, DatePipe } from '@angular/common';
import { Observable } from 'rxjs';
import { FormsModule } from '@angular/forms';
import {
  Firestore,
  collection,
  collectionData,
  addDoc,
  query,
  orderBy
} from '@angular/fire/firestore';

interface ChatMessage {
  id?: string;
  text: string;
  sender: 'doctor' | 'patient';
  timestamp: Date;
}

@Component({
  selector: 'app-chat-app',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    AsyncPipe,
    DatePipe
  ],
  templateUrl: './chat-app.component.html',
  styleUrl: './chat-app.component.css'
})
export class ChatAppComponent {
  isOpen = false;
  newMessage = '';
  messages$: Observable<ChatMessage[]>;
  userRole: 'doctor' | 'patient' = 'patient';

  constructor(private firestore: Firestore) {
    const messagesCollection = collection(this.firestore, 'messages');
    const messagesQuery = query(messagesCollection, orderBy('timestamp', 'asc'));
    this.messages$ = collectionData(messagesQuery) as Observable<ChatMessage[]>;
  }

  toggleChat() {
    this.isOpen = !this.isOpen;
  }

  async sendMessage() {
    if (this.newMessage.trim()) {
      const messagesCollection = collection(this.firestore, 'messages');
      await addDoc(messagesCollection, {
        text: this.newMessage,
        sender: this.userRole,
        timestamp: new Date()
      });
      this.newMessage = '';
    }
  }
}
