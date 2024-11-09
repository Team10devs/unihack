import {IMedicineResponse} from './IMedicineResponse';

export interface IPrescriptionResponse{
  PatientId : string;
  Diagnostic : string;
  Medicine : IMedicineResponse[];
}
