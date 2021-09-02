﻿// <auto-generated />
using AspergillosisEPR.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;

namespace AspergillosisEPR.Migrations.Aspergillosis
{
    [DbContext(typeof(AspergillosisContext))]
    [Migration("20180309102017_AddCaseReportFormFieldOption")]
    partial class AddCaseReportFormFieldOption
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AspergillosisEPR.Models.CaseReportForms.CaseReportFormCategory", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("CaseReportFormCategories");
                });

            modelBuilder.Entity("AspergillosisEPR.Models.CaseReportForms.CaseReportFormField", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CaseReportFormFieldTypeId");

                    b.Property<int?>("CaseReportFormSectionID");

                    b.Property<string>("Label");

                    b.HasKey("ID");

                    b.HasIndex("CaseReportFormFieldTypeId");

                    b.HasIndex("CaseReportFormSectionID");

                    b.ToTable("CaseReportFormFields");
                });

            modelBuilder.Entity("AspergillosisEPR.Models.CaseReportForms.CaseReportFormFieldOption", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CaseReportFormFieldId");

                    b.Property<int>("CaseReportFormOptionChoiceId");

                    b.HasKey("ID");

                    b.HasIndex("CaseReportFormFieldId");

                    b.HasIndex("CaseReportFormOptionChoiceId");

                    b.ToTable("CaseReportFormFieldOptions");
                });

            modelBuilder.Entity("AspergillosisEPR.Models.CaseReportForms.CaseReportFormFieldType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("CaseReportFormFieldTypes");
                });

            modelBuilder.Entity("AspergillosisEPR.Models.CaseReportForms.CaseReportFormOptionChoice", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CaseReportFormOptionGroupId");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.HasIndex("CaseReportFormOptionGroupId");

                    b.ToTable("CaseReportFormOptionChoices");
                });

            modelBuilder.Entity("AspergillosisEPR.Models.CaseReportForms.CaseReportFormOptionGroup", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("CaseReportFormOptionGroups");
                });

            modelBuilder.Entity("AspergillosisEPR.Models.CaseReportForms.CaseReportFormResult", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CaseReportFormCategoryID");

                    b.Property<int>("CaseReportFormResultCategoryId");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.HasIndex("CaseReportFormCategoryID");

                    b.ToTable("CaseReportFormResults");
                });

            modelBuilder.Entity("AspergillosisEPR.Models.CaseReportForms.CaseReportFormSection", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CaseReportFormResultID");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.HasIndex("CaseReportFormResultID");

                    b.ToTable("CaseReportFormSections");
                });

            modelBuilder.Entity("AspergillosisEPR.Models.ChestDistribution", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("ChestDistributions");
                });

            modelBuilder.Entity("AspergillosisEPR.Models.ChestLocation", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("ChestLocations");
                });

            modelBuilder.Entity("AspergillosisEPR.Models.DbImport", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DbImportTypeId");

                    b.Property<DateTime>("ImportedDate");

                    b.Property<string>("ImportedFileName");

                    b.Property<int>("PatientsCount");

                    b.HasKey("ID");

                    b.ToTable("DbImports");
                });

            modelBuilder.Entity("AspergillosisEPR.Models.DbImportType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ImporterClass")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("DbImportTypes");
                });

            modelBuilder.Entity("AspergillosisEPR.Models.DiagnosisCategory", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CategoryName")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("DiagnosisCategories");
                });

            modelBuilder.Entity("AspergillosisEPR.Models.DiagnosisType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("ShortName");

                    b.HasKey("ID");

                    b.ToTable("DiagnosisTypes");
                });

            modelBuilder.Entity("AspergillosisEPR.Models.Drug", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("Drugs");
                });

            modelBuilder.Entity("AspergillosisEPR.Models.Finding", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Findings");
                });

            modelBuilder.Entity("AspergillosisEPR.Models.Grade", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Grades");
                });

            modelBuilder.Entity("AspergillosisEPR.Models.ImmunoglobulinType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("ImmunoglobulinTypes");
                });

            modelBuilder.Entity("AspergillosisEPR.Models.Patient", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DOB");

                    b.Property<DateTime?>("DateOfDeath");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("Gender")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<int?>("PatientStatusId");

                    b.Property<string>("RM2Number")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.HasIndex("PatientStatusId");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("AspergillosisEPR.Models.PatientDiagnosis", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasColumnType("ntext");

                    b.Property<int>("DiagnosisCategoryId");

                    b.Property<int>("DiagnosisTypeId");

                    b.Property<int>("PatientId");

                    b.HasKey("ID");

                    b.HasIndex("DiagnosisCategoryId");

                    b.HasIndex("DiagnosisTypeId");

                    b.HasIndex("PatientId");

                    b.ToTable("PatientDiagnosis");
                });

            modelBuilder.Entity("AspergillosisEPR.Models.PatientDrug", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DrugId");

                    b.Property<DateTime?>("EndDate");

                    b.Property<int>("PatientId");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("ID");

                    b.HasIndex("DrugId");

                    b.HasIndex("PatientId");

                    b.ToTable("PatientDrugs");
                });

            modelBuilder.Entity("AspergillosisEPR.Models.PatientDrugSideEffect", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("PatientDrugId");

                    b.Property<int>("SideEffectId");

                    b.HasKey("ID");

                    b.HasIndex("PatientDrugId");

                    b.HasIndex("SideEffectId");

                    b.ToTable("PatientDrugSideEffects");
                });

            modelBuilder.Entity("AspergillosisEPR.Models.PatientExamination", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<int>("PatientImmunoglobulinId");

                    b.Property<int>("PatientMeasurementId");

                    b.Property<int>("PatientRadiologyFinidingId");

                    b.Property<int>("PatientSTGQuestionnaireId");

                    b.Property<int>("PatientVisitId");

                    b.HasKey("ID");

                    b.HasIndex("PatientImmunoglobulinId");

                    b.HasIndex("PatientMeasurementId");

                    b.HasIndex("PatientRadiologyFinidingId");

                    b.HasIndex("PatientSTGQuestionnaireId");

                    b.HasIndex("PatientVisitId");

                    b.ToTable("PatientExaminations");

                    b.HasDiscriminator<string>("Discriminator").HasValue("PatientExamination");
                });

            modelBuilder.Entity("AspergillosisEPR.Models.PatientImmunoglobulin", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateTaken");

                    b.Property<int>("ImmunoglobulinTypeId");

                    b.Property<int>("PatientId");

                    b.Property<decimal>("Value");

                    b.HasKey("ID");

                    b.HasIndex("ImmunoglobulinTypeId");

                    b.HasIndex("PatientId");

                    b.ToTable("PatientImmunoglobulins");
                });

            modelBuilder.Entity("AspergillosisEPR.Models.PatientMeasurement", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateTaken");

                    b.Property<decimal?>("Height");

                    b.Property<int>("PatientId");

                    b.Property<decimal?>("Weight");

                    b.HasKey("ID");

                    b.HasIndex("PatientId");

                    b.ToTable("PatientMeasurements");
                });

            modelBuilder.Entity("AspergillosisEPR.Models.PatientRadiologyFinding", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ChestDistributionId");

                    b.Property<int>("ChestLocationId");

                    b.Property<DateTime>("DateTaken");

                    b.Property<int>("FindingId");

                    b.Property<int>("GradeId");

                    b.Property<string>("Note")
                        .HasColumnType("ntext");

                    b.Property<int>("PatientId");

                    b.Property<int>("RadiologyTypeId");

                    b.Property<int>("TreatmentResponseId");

                    b.HasKey("ID");

                    b.HasIndex("ChestDistributionId");

                    b.HasIndex("ChestLocationId");

                    b.HasIndex("FindingId");

                    b.HasIndex("GradeId");

                    b.HasIndex("PatientId");

                    b.HasIndex("RadiologyTypeId");

                    b.HasIndex("TreatmentResponseId");

                    b.ToTable("PatientRadiologyFindings");
                });

            modelBuilder.Entity("AspergillosisEPR.Models.PatientStatus", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("PatientStatuses");
                });

            modelBuilder.Entity("AspergillosisEPR.Models.PatientSTGQuestionnaire", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("ActivityScore")
                        .HasColumnType("decimal(10,2)");

                    b.Property<DateTime>("DateTaken");

                    b.Property<decimal>("ImpactScore")
                        .HasColumnType("decimal(10,2)");

                    b.Property<int>("PatientId");

                    b.Property<decimal>("SymptomScore")
                        .HasColumnType("decimal(10,2)");

                    b.Property<decimal>("TotalScore")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("ID");

                    b.HasIndex("PatientId");

                    b.ToTable("PatientSTGQuestionnaires");
                });

            modelBuilder.Entity("AspergillosisEPR.Models.PatientVisit", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("PatientId");

                    b.Property<DateTime>("VisitDate");

                    b.HasKey("ID");

                    b.HasIndex("PatientId");

                    b.ToTable("PatientVisits");
                });

            modelBuilder.Entity("AspergillosisEPR.Models.RadiologyType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("RadiologyTypes");
                });

            modelBuilder.Entity("AspergillosisEPR.Models.SideEffect", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("SideEffects");
                });

            modelBuilder.Entity("AspergillosisEPR.Models.TreatmentResponse", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("TreatmentResponses");
                });

            modelBuilder.Entity("AspergillosisEPR.Models.ImmunologyExamination", b =>
                {
                    b.HasBaseType("AspergillosisEPR.Models.PatientExamination");


                    b.ToTable("ImmunologyExamination");

                    b.HasDiscriminator().HasValue("ImmunologyExamination");
                });

            modelBuilder.Entity("AspergillosisEPR.Models.MeasurementExamination", b =>
                {
                    b.HasBaseType("AspergillosisEPR.Models.PatientExamination");


                    b.ToTable("MeasurementExamination");

                    b.HasDiscriminator().HasValue("MeasurementExamination");
                });

            modelBuilder.Entity("AspergillosisEPR.Models.RadiologyExamination", b =>
                {
                    b.HasBaseType("AspergillosisEPR.Models.PatientExamination");


                    b.ToTable("RadiologyExamination");

                    b.HasDiscriminator().HasValue("RadiologyExamination");
                });

            modelBuilder.Entity("AspergillosisEPR.Models.SGRQExamination", b =>
                {
                    b.HasBaseType("AspergillosisEPR.Models.PatientExamination");


                    b.ToTable("SGRQExamination");

                    b.HasDiscriminator().HasValue("SGRQExamination");
                });

            modelBuilder.Entity("AspergillosisEPR.Models.CaseReportForms.CaseReportFormField", b =>
                {
                    b.HasOne("AspergillosisEPR.Models.CaseReportForms.CaseReportFormFieldType", "CaseReportFormFieldType")
                        .WithMany()
                        .HasForeignKey("CaseReportFormFieldTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AspergillosisEPR.Models.CaseReportForms.CaseReportFormSection")
                        .WithMany("CaseReportFormResultFields")
                        .HasForeignKey("CaseReportFormSectionID");
                });

            modelBuilder.Entity("AspergillosisEPR.Models.CaseReportForms.CaseReportFormFieldOption", b =>
                {
                    b.HasOne("AspergillosisEPR.Models.CaseReportForms.CaseReportFormField", "Field")
                        .WithMany("Options")
                        .HasForeignKey("CaseReportFormFieldId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AspergillosisEPR.Models.CaseReportForms.CaseReportFormOptionChoice", "Option")
                        .WithMany()
                        .HasForeignKey("CaseReportFormOptionChoiceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AspergillosisEPR.Models.CaseReportForms.CaseReportFormOptionChoice", b =>
                {
                    b.HasOne("AspergillosisEPR.Models.CaseReportForms.CaseReportFormOptionGroup", "CaseReportFormOptionGroup")
                        .WithMany("Choices")
                        .HasForeignKey("CaseReportFormOptionGroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AspergillosisEPR.Models.CaseReportForms.CaseReportFormResult", b =>
                {
                    b.HasOne("AspergillosisEPR.Models.CaseReportForms.CaseReportFormCategory", "CaseReportFormCategory")
                        .WithMany()
                        .HasForeignKey("CaseReportFormCategoryID");
                });

            modelBuilder.Entity("AspergillosisEPR.Models.CaseReportForms.CaseReportFormSection", b =>
                {
                    b.HasOne("AspergillosisEPR.Models.CaseReportForms.CaseReportFormResult")
                        .WithMany("CaseReportFormResultSections")
                        .HasForeignKey("CaseReportFormResultID");
                });

            modelBuilder.Entity("AspergillosisEPR.Models.Patient", b =>
                {
                    b.HasOne("AspergillosisEPR.Models.PatientStatus", "PatientStatus")
                        .WithMany()
                        .HasForeignKey("PatientStatusId");
                });

            modelBuilder.Entity("AspergillosisEPR.Models.PatientDiagnosis", b =>
                {
                    b.HasOne("AspergillosisEPR.Models.DiagnosisCategory", "DiagnosisCategory")
                        .WithMany("PatientDiagnoses")
                        .HasForeignKey("DiagnosisCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AspergillosisEPR.Models.DiagnosisType", "DiagnosisType")
                        .WithMany("PatientDiagnoses")
                        .HasForeignKey("DiagnosisTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AspergillosisEPR.Models.Patient", "Patient")
                        .WithMany("PatientDiagnoses")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AspergillosisEPR.Models.PatientDrug", b =>
                {
                    b.HasOne("AspergillosisEPR.Models.Drug", "Drug")
                        .WithMany()
                        .HasForeignKey("DrugId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AspergillosisEPR.Models.Patient", "Patient")
                        .WithMany("PatientDrugs")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AspergillosisEPR.Models.PatientDrugSideEffect", b =>
                {
                    b.HasOne("AspergillosisEPR.Models.PatientDrug", "PatientDrug")
                        .WithMany("SideEffects")
                        .HasForeignKey("PatientDrugId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AspergillosisEPR.Models.SideEffect", "SideEffect")
                        .WithMany()
                        .HasForeignKey("SideEffectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AspergillosisEPR.Models.PatientExamination", b =>
                {
                    b.HasOne("AspergillosisEPR.Models.PatientImmunoglobulin", "PatientImmunoglobulin")
                        .WithMany()
                        .HasForeignKey("PatientImmunoglobulinId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AspergillosisEPR.Models.PatientMeasurement", "PatientMeasurement")
                        .WithMany()
                        .HasForeignKey("PatientMeasurementId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AspergillosisEPR.Models.PatientRadiologyFinding", "PatientRadiologyFiniding")
                        .WithMany()
                        .HasForeignKey("PatientRadiologyFinidingId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AspergillosisEPR.Models.PatientSTGQuestionnaire", "PatientSTGQuestionnaire")
                        .WithMany()
                        .HasForeignKey("PatientSTGQuestionnaireId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AspergillosisEPR.Models.PatientVisit", "PatientVisit")
                        .WithMany()
                        .HasForeignKey("PatientVisitId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AspergillosisEPR.Models.PatientImmunoglobulin", b =>
                {
                    b.HasOne("AspergillosisEPR.Models.ImmunoglobulinType", "ImmunoglobulinType")
                        .WithMany()
                        .HasForeignKey("ImmunoglobulinTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AspergillosisEPR.Models.Patient")
                        .WithMany("PatientImmunoglobulines")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AspergillosisEPR.Models.PatientMeasurement", b =>
                {
                    b.HasOne("AspergillosisEPR.Models.Patient")
                        .WithMany("PatientMeasurements")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AspergillosisEPR.Models.PatientRadiologyFinding", b =>
                {
                    b.HasOne("AspergillosisEPR.Models.ChestDistribution", "ChestDistribution")
                        .WithMany()
                        .HasForeignKey("ChestDistributionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AspergillosisEPR.Models.ChestLocation", "ChestLocation")
                        .WithMany()
                        .HasForeignKey("ChestLocationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AspergillosisEPR.Models.Finding", "Finding")
                        .WithMany()
                        .HasForeignKey("FindingId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AspergillosisEPR.Models.Grade", "Grade")
                        .WithMany()
                        .HasForeignKey("GradeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AspergillosisEPR.Models.Patient")
                        .WithMany("PatientRadiologyFindings")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AspergillosisEPR.Models.RadiologyType", "RadiologyType")
                        .WithMany()
                        .HasForeignKey("RadiologyTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AspergillosisEPR.Models.TreatmentResponse", "TreatmentResponse")
                        .WithMany()
                        .HasForeignKey("TreatmentResponseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AspergillosisEPR.Models.PatientSTGQuestionnaire", b =>
                {
                    b.HasOne("AspergillosisEPR.Models.Patient")
                        .WithMany("STGQuestionnaires")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AspergillosisEPR.Models.PatientVisit", b =>
                {
                    b.HasOne("AspergillosisEPR.Models.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
