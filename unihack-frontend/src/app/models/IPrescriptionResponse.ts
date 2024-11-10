import {IMedicineResponse} from './IMedicineResponse';

export interface IPrescriptionResponse{
  patientId : string;
  diagnostic : string;
  medicine : IMedicineResponse[];
}
