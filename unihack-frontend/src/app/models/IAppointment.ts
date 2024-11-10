import {IPatient} from './IPatient';
import {IDoctor} from './IDoctor';
import {AppointmentStatus} from './AppointmentStatus';

export interface IAppointment{
  Patient :IPatient;
  Doctor : IDoctor;
  AppointmentStartTime : string;
  AppointmentEndTime : string;
  AppointmentStatus : AppointmentStatus;
  Notes : string;
}

