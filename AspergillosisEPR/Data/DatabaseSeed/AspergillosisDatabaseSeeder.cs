﻿using AspergillosisEPR.Data.DatabaseSeed.SeedFiles;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspergillosisEPR.Data.DatabaseSeed
{
    public class AspergillosisDatabaseSeeder
    {
        public static void SeedDatabase(IHostingEnvironment hostingEnvironment, 
                                        AspergillosisContext context)
        {
            SampleDataInitializer.Initialize(context);
            SampleDataInitializer.AddDefaultPatientStatuses(context);
            SampleDataInitializer.CreateDbImportTypes(context);
            SampleDataInitializer.AddIgTypes(context);
            RadiologyDataInitializer.AddRadiologyModels(context);
            CaseReportFormsDataInitializer.AddCaseReportFormsModels(context);
            QoLExcelImportType.Seed(context);
            IGgEPRImportTypeSeed.Seed(context);
            CaseReportFormsDataInitializer.AddSelectFieldTypes(context);
            MedicalTiralsDataInitializer.AddMedicalTrialsModels(context);
            IntraDrugLevelExcelTypeSeed.Seed(context);
            UnitOfMeasureMgLSeed.Seed(context);
            ManArtsImportSeed.Seed(context);
            ManArtsImportSeed.AddOtherPFTs(context);
            ManArtsProcessedFileSeed.Seed(context);
            ManArtsProcessedFileSeed.SeedSmokingStatuses(context);
            ManArtsProcessedFileSeed.SeedDrugLevel(context);
            ManArtsProcessedFileSeed.SeedPFTandHaematology(context);
            EPRClinicLetterDbImportTypeSeed.Seed(context);
            FoodDatabaseSeed.SeedDefaultFoods(context);
            OtherAllergicItemDatabaseSeed.SeedDefaultItems(context);
            FungiAllergicItemDatabaseSeed.SeedDefaultItems(context);
            ReportTypeSeed.Initialize(context);
            DLNACDatesImportTypeSeed.Seed(context);
            EPRTotalIgEImportTypeSeed.Seed(context);
            UnitOfMeasureMgLSeed.SeedOtherUnits(context);
            TestTypeSeed.Seed(context);
            PatientTestsResultsImportTypeSeed.CRPSeed(context);
            PatientTestsResultsImportTypeSeed.AlbuminSeed(context);
            PatientTestsResultsImportTypeSeed.HbSeed(context);
            PatientTestsResultsImportTypeSeed.WBCSeed(context);
            PatientTestsResultsImportTypeSeed.LymphocytesSeed(context);
            QoLExcelImportType.SeedMRC(context);
            ReportTypeSeed.AddCPAMortalityAudit(context);
            ReportTypeSeed.AddIgGAndSGRQReport(context);
            AllTestTypesSeed.Seed(context);
            RadiologyDataInitializer.OtherRadiologyTypes(context, hostingEnvironment);
            PFTSpreadsheetImproterTypeSeed.Seed(context);
            ReferallDatesImportTypeSeed.Run(context);
            AddHospitalAdmissionDbImportType.Seed(context);
            DateOfDiagnosisDbImportType.Seed(context);
            AddMortalityAuditDates.Seed(context);
            AddMortalityAuditSGRQ.Seed(context);
            AddMortalityAuditMRCScore.Seed(context);
            AddMortalityAuditWeightAndHeight.Seed(context);
            //PostcodeSeed.ReadCsvIntoDatabase(hostingEnvironment, context);
        }
    }
}
