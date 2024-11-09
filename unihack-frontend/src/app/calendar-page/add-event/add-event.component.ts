import { Component } from '@angular/core';
import {NgIf} from '@angular/common';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {CalendarEvent} from 'angular-calendar';
import {addDays} from 'date-fns';

@Component({
  selector: 'app-add-event',
  standalone: true,
  imports: [
    NgIf,
    ReactiveFormsModule
  ],
  templateUrl: './add-event.component.html',
  styleUrl: './add-event.component.css'
})
export class AddEventComponent {

  eventForm: FormGroup;
  showDialog = false;

  constructor(private fb: FormBuilder) {
    this.eventForm = this.fb.group({
      title: ['', Validators.required],
      start: [null, Validators.required],
      end: [null, Validators.required],
      allDay: [false],
      primaryColor: ['#1e90ff'],
      secondaryColor: ['#D1E8FF']
    });
  }

  openDialog(): void {
    this.showDialog = true;
    this.eventForm.reset({
      title: '',
      start: new Date(),
      end: addDays(new Date(), 1),
      allDay: false,
      primaryColor: '#1e90ff',
      secondaryColor: '#D1E8FF'
    });
  }

  closeDialog(): void {
    this.showDialog = false;
  }

  onSubmit(): void {
    if (this.eventForm.valid) {
      const formValue = this.eventForm.value;

      const newEvent: CalendarEvent = {
        title: formValue.title,
        start: formValue.start,
        end: formValue.end,
        allDay: formValue.allDay,
        color: {
          primary: formValue.primaryColor,
          secondary: formValue.secondaryColor
        },
        resizable: {
          beforeStart: true,
          afterEnd: true
        },
        draggable: true
      };

      console.log('New Event:', newEvent);
      this.closeDialog();
    }
  }

}
