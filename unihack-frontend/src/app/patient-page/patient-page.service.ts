import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environments} from '../environment/environment';
import {IPrescriptionResponse} from '../models/IPrescriptionResponse';
import {Observable} from 'rxjs';
import {IPatientResponse} from '../models/IPatientResponse';

@Injectable()

export class PatientPageService{
  constructor(private http : HttpClient) {}

  getPrescriptionByPatientId(id : string) : Observable<IPrescriptionResponse[]>{
    const apiUrl = `${environments.apiUrl}/api/Prescription/GetByPatientId?patientId=${id}`;
    return this.http.get<IPrescriptionResponse[]>(apiUrl);
  }

  getPdf(presctiptionId : string){
    const apiUrl = `${environments.apiUrl}/api/Prescription/GetPdfByPrescriptionId?prescriptionId=${presctiptionId}`;
    return this.http.get(apiUrl, { responseType: 'blob' });

  }


  postPrescription(response: IPrescriptionResponse):Observable<Blob> {
    const apiUrl = `${environments.apiUrl}/api/Prescription/GeneratePrescription`;
    return this.http.post(apiUrl, response, { responseType: 'blob' });
  }


}
