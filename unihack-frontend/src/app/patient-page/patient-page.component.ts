import {Component, Input, ViewChild} from '@angular/core';
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
    ConfirmDialogModule
  ],
  providers:[MessageService,ConfirmationService] ,
  templateUrl: './patient-page.component.html',
  styleUrl: './patient-page.component.css'
})
export class PatientPageComponent {
@Input() patient !: IPatient;

  @ViewChild('dt') dt: Table | undefined;

  prescriptions: IPrescriptionResponse[] = [];
  selectedPrescriptions: IPrescriptionResponse[] = [];
  prescription: IPrescriptionResponse = {
    PatientId: '',
    Diagnostic: '',
    Medicine: []
  };
  prescriptionDialog: boolean = false;
  editMode: boolean = false;
  loading: boolean = false;
  searchValue: string = '';
  submitted: boolean = false;

  constructor(
    private messageService: MessageService,
    private confirmationService: ConfirmationService
  ) {}

  ngOnInit() {
    this.loadPrescriptions();
  }

  loadPrescriptions() {
    this.loading = true;
    this.prescriptions = [
      {
        PatientId: 'P12345',
        Diagnostic: 'Hypertension',
        Medicine: [
          {
            ID: 'string',
            Name: 'Amlodipine',
            Dosage: 5,
            FrequencyPerDay: 1
          },
          {
            ID: 'string',

            Name: 'Lisinopril',
            Dosage: 10,
            FrequencyPerDay: 1
          },
          {
            ID: 'string',

            Name: 'Glibenclamide',
            Dosage: 5,
            FrequencyPerDay: 1
          },
          {
            ID: 'string',

            Name: 'Glibenclamide',
            Dosage: 5,
            FrequencyPerDay: 1
          }
        ]
      },
      {
        PatientId: 'P67890',
        Diagnostic: 'Diabetes Type 2',
        Medicine: [
          {
            ID: 'string',
            Name: 'Metformin',
            Dosage: 500,
            FrequencyPerDay: 2
          },
          {
            ID: 'string',

            Name: 'Glibenclamide',
            Dosage: 5,
            FrequencyPerDay: 1
          }
        ]
      },
      ]
    // Implement your service call here
    // this.prescriptionService.getPrescriptions().subscribe(...)
    this.loading = false;
  }

  openNew() {
    this.prescription = {
      PatientId: '',
      Diagnostic: '',
      Medicine: []
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
    this.prescription.Medicine.push({
      ID: '',
      Name: '',
      Dosage: 0,
      FrequencyPerDay: 1
    });
  }

  removeMedicine(index: number) {
    this.prescription.Medicine.splice(index, 1);
  }

  hideDialog() {
    this.prescriptionDialog = false;
    this.submitted = false;
  }

  savePrescription() {
    this.submitted = true;

    if (this.prescription.PatientId?.trim() && this.prescription.Diagnostic?.trim()) {
      if (this.editMode) {
        // Implement update logic
      } else {
        // Implement create logic
      }

      this.prescriptionDialog = false;
      this.prescription = {
        PatientId: '',
        Diagnostic: '',
        Medicine: []
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

  viewDetails(prescription: IPrescriptionResponse) {
    // Implement view details logic
  }


}
