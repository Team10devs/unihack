import {Component, Input, OnInit, ViewChild} from '@angular/core';
import {IPatient} from '../models/IPatient';
import {Table, TableModule} from 'primeng/table';
import {Button, ButtonDirective} from 'primeng/button';
import {ChipsModule} from 'primeng/chips';
import {FormsModule} from '@angular/forms';
import {DialogModule} from 'primeng/dialog';
import {NgForOf} from '@angular/common';
import {PaginatorModule} from 'primeng/paginator';
import {ConfirmDialogModule} from 'primeng/confirmdialog';
import {IPrescriptionResponse} from '../models/IPrescriptionResponse';
import {ConfirmationService, MessageService} from 'primeng/api';
import {PatientPageService} from './patient-page.service';
import {ActivatedRoute} from '@angular/router';
import {PatientTableService} from '../doctor-page/patient-table/patient-table.service';
import {HttpClientModule} from '@angular/common/http';
import {InputTextModule} from 'primeng/inputtext';
import {InputNumberModule} from 'primeng/inputnumber';

@Component({
  selector: 'app-patient-page',
  standalone: true,
  imports: [
    TableModule,
    Button,
    ChipsModule,
    FormsModule,
    DialogModule,
    NgForOf,
    PaginatorModule,
    ButtonDirective,
    ConfirmDialogModule,
    HttpClientModule,
    InputTextModule,
    InputNumberModule
  ],
  providers: [
    MessageService,
    ConfirmationService,
    PatientTableService,
    PatientPageService  // Add the service here
  ],
  templateUrl: './patient-page.component.html',
  styleUrl: './patient-page.component.css'
})
export class PatientPageComponent implements OnInit{
  @Input() patient!: IPatient;
  @ViewChild('dt') dt: Table | undefined;

  prescriptions: IPrescriptionResponse[] = [];
  selectedPrescriptions: IPrescriptionResponse[] = [];
  prescription: IPrescriptionResponse = {
    patientId: '',
    diagnostic: '',
    medicine: []
  };
  prescriptionDialog: boolean = false;
  editMode: boolean = false;
  loading: boolean = false;
  searchValue: string = '';
  submitted: boolean = false;
  patientId: string | null = null;

  constructor(
    private messageService: MessageService,
    private confirmationService: ConfirmationService,
    private patientPageService: PatientPageService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    // Get the patient ID first
    this.patientId = this.route.snapshot.paramMap.get('id');
    console.log('Patient ID:', this.patientId);

    // Then load prescriptions
    if (this.patientId) {
      this.loadPrescriptions();
    }
  }

  loadPrescriptions() {
    this.loading = true;

    if (this.patientId) {
      this.patientPageService.getPrescriptionByPatientId(this.patientId)
        .subscribe({
          next: (data) => {
            this.prescriptions = data;
            this.loading = false;
          },
          error: (error) => {
            console.error('Error loading prescriptions:', error);
            this.messageService.add({
              severity: 'error',
              summary: 'Error',
              detail: 'Failed to load prescriptions',
              life: 3000
            });
            this.loading = false;
          }
        });
    }
  }


  openNew() {
    this.prescription = {
      patientId: '',
      diagnostic: '',
      medicine: []
    };
    this.editMode = false;
    this.prescriptionDialog = true;
    this.submitted = false;
  }

  editPrescription(prescription: IPrescriptionResponse) {
    this.prescription = { ...prescription };
    this.editMode = true;
    this.prescriptionDialog = true;
  }

  deletePrescription(prescription: IPrescriptionResponse) {
    this.confirmationService.confirm({
      message: 'Are you sure you want to delete this prescription?',
      header: 'Confirm',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        // Implement delete logic
        this.messageService.add({
          severity: 'success',
          summary: 'Successful',
          detail: 'Prescription Deleted',
          life: 3000
        });
      }
    });
  }

  addMedicine() {
    this.prescription.medicine.push({
      name: '',
      dosage: 0,
      frequencyPerDay: 1
    });
  }

  removeMedicine(index: number) {
    this.prescription.medicine.splice(index, 1);
  }

  hideDialog() {
    this.prescriptionDialog = false;
    this.submitted = false;
  }

  savePrescription() {
    this.submitted = true;

    this.prescription.medicine.forEach(med => {
      if (med.startDate) {
        med.startDate = new Date(med.startDate).toISOString();
      }
      if (med.endDate) {
        med.endDate = new Date(med.endDate).toISOString();
      }
    });

    console.log(this.prescription);
    if(this.patientId)
    this.prescription.patientId = this.patientId;
    this.patientPageService.postPrescription(this.prescription).subscribe(temp =>{
      const blobUrl = window.URL.createObjectURL(temp);
      const a = document.createElement('a');
      a.href = blobUrl;
      a.download = 'prescription.pdf'; // Set the download filename
      document.body.appendChild(a);
      a.click();
      document.body.removeChild(a);
      window.URL.revokeObjectURL(blobUrl);
    });

    if (this.prescription.patientId?.trim() && this.prescription.diagnostic?.trim()) {
      if (this.editMode) {
        this.patientPageService.postPrescription(this.prescription);
      } else {
        this.patientPageService.postPrescription(this.prescription);
      }

      this.prescriptionDialog = false;
      this.prescription = {
        patientId: '',
        diagnostic: '',
        medicine: []
      };
    }
  }

  onFilter(event: any) {
    this.dt?.filterGlobal(event.target.value, 'contains');
  }

  clear(table: Table) {
    table.clear();
    this.searchValue = '';
  }

  openPdf(presctiptionId :string){
    this.patientPageService.getPdf(presctiptionId).subscribe({
      next: (response: Blob) => {
        const blobUrl = window.URL.createObjectURL(response);
        window.open(blobUrl, '_blank');
      },
      error: (error) => {
        console.error('Error fetching PDF:', error);
      }
    });
  }

  viewDetails(prescription: IPrescriptionResponse) {
  }
}
