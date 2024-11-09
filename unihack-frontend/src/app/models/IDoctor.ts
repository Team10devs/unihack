import {IPatient} from './IPatient';
import {IAppointment} from './IAppointment';

export interface IDoctor{
  License : string;
  Address : string;
  Specialization : string;
  Patients : IPatient[];
  DoctorAppointments : IAppointment[];

}
