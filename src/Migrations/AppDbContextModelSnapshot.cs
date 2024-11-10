﻿// <auto-generated />
using System;
using MedicalAPI.Repository.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MedicalAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MedicalAPI.Domain.Entities.Entity.Documents.AppointmentModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime>("AppointmentEndTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("AppointmentStartTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("AppointmentStatus")
                        .HasColumnType("integer");

                    b.Property<string>("DoctorId")
                        .HasColumnType("text");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PatientId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("MedicalAPI.Domain.Entities.Entity.Documents.PrescriptionModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Diagnostic")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DoctorId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PatientId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PdfId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.HasIndex("PdfId");

                    b.ToTable("Prescriptions");
                });

            modelBuilder.Entity("MedicalAPI.Domain.Entities.Medicine.MedicineModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("Dosage")
                        .HasColumnType("integer");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("FrequencyPerDay")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PrescriptionModelId")
                        .HasColumnType("text");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("PrescriptionModelId");

                    b.ToTable("Medicines");
                });

            modelBuilder.Entity("MedicalAPI.Domain.Entities.PrescriptionPdf", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<byte[]>("Data")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PrescriptionId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("PrescriptionPdfs");
                });

            modelBuilder.Entity("MedicalAPI.Domain.Entities.User.DoctorModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DeviceToken")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("License")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<string>("Specialization")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("MedicalAPI.Domain.Entities.User.PatientModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DeviceToken")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DoctorId")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Gender")
                        .HasColumnType("text");

                    b.Property<string>("MedicalHistory")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("MedicalAPI.Domain.Entities.Entity.Documents.AppointmentModel", b =>
                {
                    b.HasOne("MedicalAPI.Domain.Entities.User.DoctorModel", "Doctor")
                        .WithMany("DoctorAppointments")
                        .HasForeignKey("DoctorId");

                    b.HasOne("MedicalAPI.Domain.Entities.User.PatientModel", "Patient")
                        .WithMany("PatientAppointments")
                        .HasForeignKey("PatientId");

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("MedicalAPI.Domain.Entities.Entity.Documents.PrescriptionModel", b =>
                {
                    b.HasOne("MedicalAPI.Domain.Entities.User.PatientModel", "Patient")
                        .WithMany("PatientPrescriptions")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MedicalAPI.Domain.Entities.PrescriptionPdf", "Pdf")
                        .WithMany()
                        .HasForeignKey("PdfId");

                    b.Navigation("Patient");

                    b.Navigation("Pdf");
                });

            modelBuilder.Entity("MedicalAPI.Domain.Entities.Medicine.MedicineModel", b =>
                {
                    b.HasOne("MedicalAPI.Domain.Entities.Entity.Documents.PrescriptionModel", null)
                        .WithMany("Medicine")
                        .HasForeignKey("PrescriptionModelId");
                });

            modelBuilder.Entity("MedicalAPI.Domain.Entities.User.PatientModel", b =>
                {
                    b.HasOne("MedicalAPI.Domain.Entities.User.DoctorModel", "Doctor")
                        .WithMany("Patients")
                        .HasForeignKey("DoctorId");

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("MedicalAPI.Domain.Entities.Entity.Documents.PrescriptionModel", b =>
                {
                    b.Navigation("Medicine");
                });

            modelBuilder.Entity("MedicalAPI.Domain.Entities.User.DoctorModel", b =>
                {
                    b.Navigation("DoctorAppointments");

                    b.Navigation("Patients");
                });

            modelBuilder.Entity("MedicalAPI.Domain.Entities.User.PatientModel", b =>
                {
                    b.Navigation("PatientAppointments");

                    b.Navigation("PatientPrescriptions");
                });
#pragma warning restore 612, 618
        }
    }
}
