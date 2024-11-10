import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environments} from '../../environment/environment';
import {Observable} from 'rxjs';
import {IPatientResponse} from '../../models/IPatientResponse';

@Injectable({providedIn:"root"})


export class PatientTableService{
  constructor(private http : HttpClient) {
  }

  getPatientsByDoctorId(id : string) : Observable<IPatientResponse[]>{
   const apiUrl = `${environments.apiUrl}/api/Patient/PatientsByDoctorId?doctorId=${id}`;
   return  this.http.get<IPatientResponse[]>(apiUrl);
  }
}
