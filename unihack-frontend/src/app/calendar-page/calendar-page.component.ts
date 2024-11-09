import { Component } from '@angular/core';
import {
  CalendarDayModule,
  CalendarEvent,
  CalendarMonthModule,
  CalendarView,
  CalendarWeekModule,
  DateAdapter,
  CalendarUtils,
  CalendarA11y,
  CalendarDateFormatter,
  CalendarEventTitleFormatter, CalendarModule
} from 'angular-calendar';
import { subDays, addDays, startOfDay } from 'date-fns';
import { DatePipe, NgIf } from '@angular/common';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';
import {AddEventComponent} from './add-event/add-event.component';


@Component({
  selector: 'app-calendar-page',
  standalone: true,
  imports: [
    CalendarMonthModule,
    DatePipe,
    CalendarWeekModule,
    NgIf,
    CalendarDayModule,
    CalendarModule,
    AddEventComponent
  ],
  providers: [
    CalendarUtils,
    CalendarA11y,
    CalendarDateFormatter,
    CalendarEventTitleFormatter,
    {
      provide: DateAdapter,
      useFactory: adapterFactory
    }
  ],
  templateUrl: './calendar-page.component.html',
  styleUrl: './calendar-page.component.css'
})
export class CalendarPageComponent {
  view: CalendarView = CalendarView.Month;
  CalendarView = CalendarView;
  viewDate: Date = new Date();

  events: CalendarEvent[] = [
    {
      start: subDays(startOfDay(new Date()), 1),
      end: addDays(new Date(), 1),
      title: 'An example event',
      color: { primary: '#1e90ff', secondary: '#D1E8FF' },
      allDay: true,
      resizable: { beforeStart: true, afterEnd: true },
      draggable: true,
    },
    {
      start: startOfDay(new Date()),
      title: 'Another event',
      color: { primary: '#e3bc08', secondary: '#FDF1BA' },
    },
    {
      start: startOfDay(new Date()),
      title: 'Another event',
      color: { primary: '#e3bc08', secondary: '#FDF1BA' },
    },
  ];

  setView(view: CalendarView) {
    this.view = view;
  }
  openDialog(): void {
    this.showDialog = true;
  }

  showDialog = false;

  closeDialog(): void {
    this.showDialog = false;
  }
  protected readonly addDays = addDays;
}
