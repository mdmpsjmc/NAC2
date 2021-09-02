﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspergillosisEPR.Data;
using AspergillosisEPR.Models;
using AspergillosisEPR.Models.Patients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspergillosisEPR.Controllers
{
    public class ExternalImportsController : Controller
    {
        private readonly AspergillosisContext _context;
        private readonly PASDbContext _pasContext;
        private readonly ExternalImportDbContext _externalImportDbContext;

        public ExternalImportsController(AspergillosisContext context,
                                         PASDbContext pasContext,
                                         ExternalImportDbContext externalImportDbContext)
        {
            _context = context;
            _pasContext = pasContext;
            _externalImportDbContext = externalImportDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Ig()
        {
            var allPatients = _context.Patients
                                      .Include(p => p.PatientImmunoglobulines);
            //var uom = _context.UnitOfMeasurements.Where(u => u.Name == "mg/L").FirstOrDefault();
            foreach(var code in PatientImmunoglobulin.Codes().Keys)
            {

                var igType = _context.ImmunoglobulinTypes
                                     .Where(it => it.Name == PatientImmunoglobulin.IgFromCode(code))
                                     .FirstOrDefault();

                foreach (var patient in allPatients)
                {
                    var igLevels = _externalImportDbContext.PathologyReports
                                                            .Where(r => r.OrderItemCode.Equals(code)
                                                                    && r.RM2Number == "RM2" + patient.RM2Number);
                    if (!igLevels.Any()) continue;

                    var existingDates = patient.PatientImmunoglobulines
                                               .Where(pi => pi.ImmunoglobulinTypeId == igType.ID)
                                               .Select(pi => pi.DateTaken.Date)
                                               .ToList();


                                        foreach (var iggLevel in igLevels)
                    {
                        if (existingDates.FindAll(d => d.Date == iggLevel.DatePerformed.Date).ToList().Count == 0)
                        {
                            if (iggLevel.Result == null) continue;
                            var patientIgG = new PatientImmunoglobulin();
                            patientIgG.PatientId = patient.ID;
                            patientIgG.DateTaken = iggLevel.DatePerformed;
                            patientIgG.ImmunoglobulinTypeId = igType.ID;
                            patientIgG.SourceSystemGUID = iggLevel.ObservationGUID;
                            try
                            {
                                patientIgG.Value = Decimal.Parse(iggLevel.Result
                                                                     .Replace("<", String.Empty)
                                                                     .Replace("*", String.Empty)
                                                                     .Replace(">", String.Empty));

                            } catch(System.FormatException e)
                            {
                                Console.WriteLine("VALUE::::::::::::" + iggLevel.Result);
                                continue;
                            }
                            patientIgG.Range = iggLevel.NormalRange;

                            await _context.PatientImmunoglobulins.AddAsync(patientIgG);
                        }
                    }
                }
            }            
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> LabTests()
        {
            var allPatients = _context.Patients
                                      .Include(p => p.PatientTestResults);
            //var uom = _context.UnitOfMeasurements.Where(u => u.Name == "mg/L").FirstOrDefault();
            foreach (var code in TestType.Codes().Keys)
            {

                var testType = _context.TestTypes
                                       .Where(it => it.Name == TestType.LabTestFromCode(code))
                                       .FirstOrDefault();

                foreach (var patient in allPatients)
                {
                    var results = _externalImportDbContext.PathologyReports
                                                            .Where(r => r.OrderItemCode.Equals(code)
                                                                    && r.RM2Number == "RM2" + patient.RM2Number);
                    if (!results.Any()) continue;
                    var existingDates = patient.PatientTestResults
                                               .Where(pi => pi.TestTypeId == testType.ID)
                                               .Select(pi => pi.DateTaken.Date)
                                               .ToList();

                    foreach (var result in results)
                    {
                        if (existingDates.FindAll(d => d.Date == result.DatePerformed.Date).ToList().Count == 0)
                        {
                            if (result.Result == null) continue;
                            var patientTestResult = new PatientTestResult();
                            patientTestResult.PatientId = patient.ID;
                            patientTestResult.DateTaken = result.DatePerformed;
                            patientTestResult.TestTypeId = testType.ID;
                            patientTestResult.SourceSystemGUID = result.ObservationGUID;
                            patientTestResult.UnitOfMeasurementId = testType.UnitOfMeasurementId;
                            try
                            {
                                patientTestResult.Value = Decimal.Parse(result.Result
                                                                     .Replace("<", String.Empty)
                                                                     .Replace("*", String.Empty)
                                                                     .Replace(">", String.Empty));

                            }
                            catch (System.FormatException e)
                            {
                                Console.WriteLine("VALUE::::::::::::" + result.Result);
                                continue;
                            }
                            patientTestResult.Range = result.NormalRange;

                            await _context.PatientTestResult.AddAsync(patientTestResult);
                        }
                    }
                }
            }
            await _context.SaveChangesAsync();
            return Ok();
        }     
    }
}
