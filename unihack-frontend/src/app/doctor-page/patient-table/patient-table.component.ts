import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { Table, TableModule } from 'primeng/table';
import { FormsModule } from '@angular/forms';
import { DialogModule } from 'primeng/dialog';
import { PaginatorModule } from 'primeng/paginator';
import { Ripple } from 'primeng/ripple';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Button, ButtonDirective } from 'primeng/button';
import { ChipsModule } from 'primeng/chips';
import { NgClass, NgIf } from '@angular/common';
import { ActivatedRoute, Router, RouterOutlet } from '@angular/router';
import { PatientTableService } from './patient-table.service';
import { AuthService } from '../../AuthService';
import { IPatientResponse } from '../../models/IPatientResponse';
import { HttpClientModule } from '@angular/common/http'; // Add this import

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
    RouterOutlet,
    HttpClientModule  // Add HttpClientModule to imports
  ],
  providers: [
    MessageService,
    ConfirmationService,
    PatientTableService  // Add the service to providers
  ],
  templateUrl: './patient-table.component.html',
  styleUrl: './patient-table.component.css'
})

export class PatientTableComponent implements OnInit{

  @ViewChild('dt') dt!: Table;

  patientDialog: boolean = false;
  patients: IPatientResponse[] = [];
  patient !: IPatientResponse;
  selectedPatients: IPatientResponse[] = [];
  submitted: boolean = false;
  editMode: boolean = false;
  loading: boolean = false;
  searchValue: string = '';
  genderOptions: string[] = ['Male', 'Female', 'Other'];

  constructor(
    private messageService: MessageService,
    private confirmationService: ConfirmationService,
    private router : Router,
    private route: ActivatedRoute,
    private patientTableService :PatientTableService,
    private authService :AuthService
  ) {}

  ngOnInit() {
    this.loadPatients();
  }


  loadPatients() {
    this.loading = true;

    this.authService.userId$.subscribe(temp =>{
      if(temp)
      this.patientTableService.getPatientsByDoctorId(temp).subscribe(data=>{
        console.log()
        this.patients = data;
      })

    })
  }

  openNew() {
    this.editMode = false;
    this.submitted = false;
    this.patientDialog = true;
  }

  editPatient(patient: IPatientResponse) {
    this.patient = { ...patient };
    this.editMode = true;
    this.patientDialog = true;
  }

  goToDetails(patient: { id: number }) {
    this.router.navigate(['/patient-page/patient', patient.id]);
  }

  deletePatient(patient: IPatientResponse) {
    this.confirmationService.confirm({
      message: 'Are you sure you want to delete ' + patient.pacientNamem + '?',
      header: 'Confirm',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
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

    if (this.patient.pacientNamem.trim() && this.patient.pacientEmail) {
      if (this.patient) {
        this.messageService.add({
          severity: 'success',
          summary: 'Successful',
          detail: 'Patient Updated',
          life: 3000
        });
      } else {
        this.messageService.add({
          severity: 'success',
          summary: 'Successful',
          detail: 'Patient Created',
          life: 3000
        });
      }

      this.patients = [...this.patients];
      this.patientDialog = false;
    }
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
