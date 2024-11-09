import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environments} from '../../environment/environment';
import {Observable} from 'rxjs';
import {IPatient} from '../../models/IPatient';

@Injectable({providedIn:"root"})


export class PatientTableService{
  constructor(private http : HttpClient) {
  }

  getPatientsByDoctorId(id : string) : Observable<IPatient[]>{
   const apiUrl = `${environments.apiUrl}/api/Patient/PatientsByDoctorId?doctorId=${id}`;
   return  this.http.get<IPatient[]>(apiUrl);
  }
}
