import {IPatient} from './IPatient';
import {IDoctor} from './IDoctor';
import {IMedicine} from './IMedicine';

export interface IPrescription{
  Patiend : IPatient;
  Doctor : IDoctor;
  Diacnostic : string;
  StartDate : string;
  EndDate : string;
  Medicine : IMedicine;

}
