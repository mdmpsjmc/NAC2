﻿using AspergillosisEPR.Data;
using AspergillosisEPR.Models;
using AspergillosisEPR.Models.Patients;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AspergillosisEPR.Lib.Importers.ClinicLetters
{
    public class EPRDocxPatientDiagnosisResolver
    {
        private string _potentialDiagnosis;
        private Patient _patient;
        private AspergillosisContext _context;
        private SmokingStatus _currentSmoker;
        private SmokingStatus _exSmoker;


        public EPRDocxPatientDiagnosisResolver(string potentialDiagnosis, 
                                          Patient patient, 
                                          AspergillosisContext context)
        {
            _potentialDiagnosis = potentialDiagnosis;
            _patient = patient;
            _context = context;
            _currentSmoker = _context.SmokingStatuses.Where(ss => ss.Name.Equals("Current")).FirstOrDefault();
            _exSmoker = _context.SmokingStatuses.Where(ss => ss.Name.Equals("Ex-Smoker")).FirstOrDefault();
        }

        internal PatientDiagnosis ResolveDiagnosis()
        {
            if (_patient.PatientDiagnoses == null)
            {
                _patient.PatientDiagnoses = new List<PatientDiagnosis>();
            }
            PatientDiagnosis diagnosis = null;
            var matchingDiagnosis = FindMatchingDiagnosisInDatabase();
            PatientDiagnosis patientDiagnosis = CheckIfMatchingDiagnosisAlreadyExists(matchingDiagnosis);
            if (patientDiagnosis == null && matchingDiagnosis != null)
            {
                
                diagnosis = BuildDiagnosisForPatient(matchingDiagnosis);
                _patient.PatientDiagnoses.Add(diagnosis);
                _context.PatientDiagnoses.Add(diagnosis);
            } else if (patientDiagnosis != null && matchingDiagnosis != null)
            {
                patientDiagnosis.Description = ExtractDiagnosisNote(matchingDiagnosis);
                patientDiagnosis.DiagnosisCategory = ResolveDiagnosisCategory();
                if (_context.Entry(patientDiagnosis).State == EntityState.Modified)
                _context.PatientDiagnoses.Update(patientDiagnosis);                          
                diagnosis = patientDiagnosis;
            }
            AddSmokingStatusIfExists();
            return diagnosis;
        }

        private void AddSmokingStatusIfExists()
        {
            _context.Entry(_patient).Reference(p => p.PatientSmokingDrinkingStatus).Load();
            if (_patient.PatientSmokingDrinkingStatus != null) return;
            if (new Regex(@"ex-smoker", RegexOptions.IgnoreCase).IsMatch(_potentialDiagnosis))
            {
                var smokingStatus = new PatientSmokingDrinkingStatus();
                smokingStatus.SmokingStatus = _exSmoker;
                smokingStatus.PatientId = _patient.ID;
                _context.PatientSmokingDrinkingStatus.Add(smokingStatus);
                return;
            }
            if (new Regex(@"smoker", RegexOptions.IgnoreCase).IsMatch(_potentialDiagnosis))
            {
                var smokingStatus = new PatientSmokingDrinkingStatus();
                smokingStatus.SmokingStatus = _currentSmoker;
                smokingStatus.PatientId = _patient.ID;
                _context.PatientSmokingDrinkingStatus.Add(smokingStatus);
            }
        }

        private PatientDiagnosis BuildDiagnosisForPatient(string matchingDiagnosis)
        {
            PatientDiagnosis patientDiagnosis = new PatientDiagnosis();
            patientDiagnosis.Patient = _patient;
            DiagnosisType diagnosisType = FindDiagnosisTypeByName(matchingDiagnosis);
            patientDiagnosis.DiagnosisType = diagnosisType;
            DiagnosisCategory diagnosisCategory = ResolveDiagnosisCategory();
            patientDiagnosis.DiagnosisCategory = diagnosisCategory;
            patientDiagnosis.Description = ExtractDiagnosisNote(matchingDiagnosis);
            return patientDiagnosis;
        }

        private string ExtractDiagnosisNote(string matchingDiagnosis)
        {
            string diagnosisNote = "";
            int diagnosisIndex = _potentialDiagnosis.ToLower().IndexOf(matchingDiagnosis.ToLower());
            if (diagnosisIndex != -1)
            {
                diagnosisNote = _potentialDiagnosis.ToLower().Remove(diagnosisIndex, matchingDiagnosis.ToLower().Count());
            }
            return diagnosisNote.Replace("(", String.Empty).Replace(")", String.Empty).Trim();
        }

        private DiagnosisCategory ResolveDiagnosisCategory()
        {
            var primaryDiagnosisRegex = RegularExpressions.FindWordInList(PrimaryDiagnosisClassifyingWords());
            var pastDiagnosisRegex = RegularExpressions.FindWordInList(PastDiagnosisClassifyingWords());
            var underlyingDiagnosisRegex = RegularExpressions.FindWordInList(UnderlyingDiagnosisClassifyingWords());

            if (primaryDiagnosisRegex.IsMatch(_potentialDiagnosis))
            {
                return _context.DiagnosisCategories.FirstOrDefault(dc => dc.CategoryName == "Primary");
            }
            else if (pastDiagnosisRegex.IsMatch(_potentialDiagnosis))
            {
                return _context.DiagnosisCategories.FirstOrDefault(dc => dc.CategoryName == "Past Diagnosis");
            }
            else if (underlyingDiagnosisRegex.IsMatch(_potentialDiagnosis))
            {
                return _context.DiagnosisCategories.FirstOrDefault(dc => dc.CategoryName == "Underlying Diagnosis");
            }
            else 
            {
                return _context.DiagnosisCategories.FirstOrDefault(dc => dc.CategoryName == "Other");
            }
        }

        private DiagnosisType FindDiagnosisTypeByName(string matchingDiagnosis)
        {
            return _context.DiagnosisTypes.FirstOrDefault(dt => dt.Name.ToLower().Equals(matchingDiagnosis)
                                                               || dt.ShortName.ToLower() == matchingDiagnosis);
        }

        private PatientDiagnosis CheckIfMatchingDiagnosisAlreadyExists(string matchingDiagnosis)
        {
            if (matchingDiagnosis == null) return null;
            if (_patient.PatientDiagnoses == null) return null;
            foreach (var diagnosisName in _patient.PatientDiagnoses.Select(pd => pd.DiagnosisType.Name))
            {
                if (diagnosisName.ToLower().Equals(matchingDiagnosis.ToLower()))
                {
                    var patientDx = _patient.PatientDiagnoses
                                            .FirstOrDefault(pd => pd.DiagnosisType.Name.ToLower().Equals(matchingDiagnosis.ToLower()));
                    return patientDx;
                }
            }
            return null;
        }

        private string FindMatchingDiagnosisInDatabase()
        {
            var matched = new List<string>();
            foreach (var dbDiagnosis in AlphabeticalDiagnosisList())
            {               
                Regex regExpression = RegularExpressions.FindWordInList(dbDiagnosis.Split(" ").ToList());
                try
                {
                    if (regExpression.IsMatch(_potentialDiagnosis))
                    {
                        matched.Add(dbDiagnosis);
                    }
                } catch(InvalidOperationException ex)
                {
                    continue;
                }                
            }
            return matched.FirstOrDefault(el => _potentialDiagnosis.ToLower().Contains(el.ToLower()));
        }

        private List<string> AlphabeticalDiagnosisList()
        {
            var diagnoses = new List<string>();
            foreach(var dbDiagnosis in _context.DiagnosisTypes.OrderBy(dt => dt.Name))
            {
                if (string.IsNullOrEmpty(dbDiagnosis.ShortName))
                {
                    diagnoses.Add(dbDiagnosis.Name);
                }
                else
                {
                    diagnoses.Add(dbDiagnosis.ShortName);
                    diagnoses.Add(dbDiagnosis.Name);
                }
            }
            return diagnoses;
        }

        private List<string> AlphabeticalDrugsList()
        {
            return _context.Drugs.OrderBy(d => d.Name).Select(d => d.Name).ToList();
        }

        private static List<string> PastDiagnosisClassifyingWords()
        {
            return new List<string> { "Prior", "Past", "Prev", "Previous", "Previously" };
        }

        private static List<string> UnderlyingDiagnosisClassifyingWords()
        {
            return new List<string> { "ABPA",
                                     "Allergic Bronchopulmonary Aspergillgillosis",
                                     "COPD",
                                     "Chronic Obstructive Pulmonary Disease",
                                     "TB",
                                     "Lung Cancer",
                                     "Emphysema",
                                     "Pneumothorax",
                                     "Pneumothoraces",
                                     "Hydropneumothorax",
                                     "Hydropneumothoraces",
                                     "Tuberculosis" };
        }

        private static List<string> PrimaryDiagnosisClassifyingWords()
        {
            return new List<string> { "CPA",
                                      "CCPA",
                                      "Aspergillosis",
                                      "SAFS",
                                      "Chronic pulmonary aspergillosis",
                                      "Chronic cavitating pulmonary aspergillosis",
                                      "Chronic fibroising pulmonary aspergillosis",
                                      "Chronic cavitary pulmonary aspergillosis",
                                      "Severe Asthma with Fungal Sentisisation",
                                      "Aspergilloma",
                                      "Pulmonary aspergillosis",
                                      "CFPA" };
        }

    }
}
