import {IDoctor} from './IDoctor';
import {IAppointment} from './IAppointment';
import {UserRole} from './UserRole';

export interface IPatient{
  patientId: string;
  BirthDate : string;
  Doctor : IDoctor;
  MedicalHistory: string;
  Appointments : IAppointment[];
  Email : string;
  FullName : string;
  UserRole : UserRole;
  DeviceToken : string;
  Gender ?: string;

}
