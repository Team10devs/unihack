import {Component, Inject, OnInit, ViewChild} from '@angular/core';
import {Table, TableModule} from 'primeng/table';
import {FormsModule} from '@angular/forms';
import {DialogModule} from 'primeng/dialog';
import {PaginatorModule} from 'primeng/paginator';
import {Ripple} from 'primeng/ripple';
import {ConfirmDialogModule} from 'primeng/confirmdialog';
import {ConfirmationService, MessageService} from 'primeng/api';
import {Button, ButtonDirective} from 'primeng/button';
import {ChipsModule} from 'primeng/chips';
import {NgClass, NgIf} from '@angular/common';
import {ActivatedRoute, Router, RouterOutlet} from '@angular/router';

interface Patient {
  id?: number;
  fullName: string;
  age: number;
  gender: string;
  email: string;
  medicalHistory: string;
}

@Component({
  selector: 'app-patient-table',
  standalone: true,
  imports: [
    TableModule,
    Button,
    FormsModule,
    DialogModule,
    PaginatorModule,
    ButtonDirective,
    Ripple,
    ConfirmDialogModule,
    ChipsModule,
    NgIf,
    NgClass,
    RouterOutlet
  ],
  providers:[MessageService,ConfirmationService] ,
  templateUrl: './patient-table.component.html',
  styleUrl: './patient-table.component.css'
})

export class PatientTableComponent implements OnInit{

  @ViewChild('dt') dt!: Table;

  patientDialog: boolean = false;
  patients: Patient[] = [];
  patient: Patient = this.initializePatient();
  selectedPatients: Patient[] = [];
  submitted: boolean = false;
  editMode: boolean = false;
  loading: boolean = false;
  searchValue: string = '';
  genderOptions: string[] = ['Male', 'Female', 'Other'];

  constructor(
    private messageService: MessageService,
    private confirmationService: ConfirmationService,
    private router : Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.loadPatients();
  }

  initializePatient(): Patient {
    return {
      fullName: '',
      age: 0,
      gender: '',
      email: '',
      medicalHistory: ''
    };
  }

  loadPatients() {
    this.loading = true;
    setTimeout(() => {
      this.patients = [
        {
          id: 1,
          fullName: 'John Doe',
          age: 45,
          gender: 'Male',
          email: 'john@example.com',
          medicalHistory: 'Hypertension, Diabetes'
        },
        {
          id: 3,
          fullName: 'J2ohn Doe',
          age: 45,
          gender: 'Male',
          email: 'john@example.com',
          medicalHistory: 'Hypertension, Diabetes'
        },
        {
          id: 2,
          fullName: 'Jo3hn Doe',
          age: 45,
          gender: 'Male',
          email: 'john@example.com',
          medicalHistory: 'Hypertension, Diabetes'
        }
      ];
      this.loading = false;
    }, 1000);
  }

  openNew() {
    this.patient = this.initializePatient();
    this.editMode = false;
    this.submitted = false;
    this.patientDialog = true;
  }

  editPatient(patient: Patient) {
    this.patient = { ...patient };
    this.editMode = true;
    this.patientDialog = true;
  }

  goToDetails(patient: { id: number }) {
    this.router.navigate(['patient', patient.id], { relativeTo: this.route });
  }

  deletePatient(patient: Patient) {
    this.confirmationService.confirm({
      message: 'Are you sure you want to delete ' + patient.fullName + '?',
      header: 'Confirm',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.patients = this.patients.filter(val => val.id !== patient.id);
        this.messageService.add({
          severity: 'success',
          summary: 'Successful',
          detail: 'Patient Deleted',
          life: 3000
        });
      }
    });
  }

  hideDialog() {
    this.patientDialog = false;
    this.submitted = false;
  }

  savePatient() {
    this.submitted = true;

    if (this.patient.fullName.trim() && this.patient.email) {
      if (this.patient.id) {
        // Update existing patient
        this.patients[this.findIndexById(this.patient.id)] = this.patient;
        this.messageService.add({
          severity: 'success',
          summary: 'Successful',
          detail: 'Patient Updated',
          life: 3000
        });
      } else {
        // Create new patient
        this.patient.id = this.generateId();
        this.patients.push(this.patient);
        this.messageService.add({
          severity: 'success',
          summary: 'Successful',
          detail: 'Patient Created',
          life: 3000
        });
      }

      this.patients = [...this.patients];
      this.patientDialog = false;
      this.patient = this.initializePatient();
    }
  }

  findIndexById(id: number): number {
    return this.patients.findIndex(patient => patient.id === id);
  }

  generateId(): number {
    return Math.floor(Math.random() * 1000);
  }

  clear(table: any) {
    table.clear();
    this.searchValue = '';
  }
  onFilter(event: Event): void {
    const target = event.target as HTMLInputElement;
    this.dt.filterGlobal(target.value, 'contains');
  }

}
