﻿using AspergillosisEPR.Data;
using AspergillosisEPR.Models;
using AspergillosisEPR.Models.Patients;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AspergillosisEPR.Lib.Importers.Implementations
{
    public class IGgLevelEPRImporter : Importer
    {
        private List<string> _lines;
        private string _id;

        public IGgLevelEPRImporter(FileStream stream, IFormFile file,
                               string fileExtension, AspergillosisContext context)
        {
            _stream = stream;
            _file = file;
            _fileExtension = fileExtension;
            Imported = new List<dynamic>();
            _context = context;
            ReadCSVFile();
            _lines = new List<string>();
        }

        private void ReadCSVFile()
        {
            _file.CopyTo(_stream);
            _stream.Position = 0;
            _lines = ReadPDFText().Split("\n").ToList();
            GetIdentifier();
            var  matched = ReadIgValues();
            ParseAndSaveLines(matched);           
        }

        private void ParseAndSaveLines(List<string> matched)
        {
            var patient = _context.Patients.Where(p => p.RM2Number.Contains(_id))
                                           .Include(p => p.PatientImmunoglobulines) 
                                           .ThenInclude(p => p.ImmunoglobulinType)
                                           .FirstOrDefault();

            var igType = _context.ImmunoglobulinTypes
                                 .Where(it => it.Name == "Aspergillus F IgG")
                                 .FirstOrDefault();

            var existingDates = patient.PatientImmunoglobulines
                                       .Where(pi => pi.ImmunoglobulinTypeId == igType.ID)
                                       .Select(pi => pi.DateTaken.Date)                                       
                                       .ToList();

           foreach (string line in matched)
            {
                var dataArray = line.Split(" ");
                string dateString = dataArray.Take(3).ToList().Join(" ");
                string igGValue = dataArray[5].Replace("*", String.Empty)
                                              .Replace("<",String.Empty)
                                              .Replace(">", String.Empty);
                DateTime parsedDate;
                DateTime.TryParseExact(dateString,"dd MMM yyyy", null, System.Globalization.DateTimeStyles.None, out parsedDate);
                if (existingDates.FindAll(d => d.Date == parsedDate.Date).ToList().Count == 0)
                {
                    var patientIGg = new PatientImmunoglobulin();
                    patientIGg.ImmunoglobulinType = igType;
                    patientIGg.PatientId = patient.ID;
                    patientIGg.Value = decimal.Parse(igGValue);
                    patientIGg.DateTaken = parsedDate;
                    _context.PatientImmunoglobulins.Add(patientIGg);
                    Imported.Add(patientIGg);
                }
            }
        }

        private string GetIdentifier()
        {
            _id = _lines.Where(l => l.Contains("Hospital number"))
                                  .FirstOrDefault()
                                  .Split(":")
                                  .Last()
                                  .Trim();
            return _id;
        }

        private List<string> ReadIgValues()
        {
            var firstCharacterIsNumber = new Regex("^[0-9]");
            var matched = new List<string>();
            foreach (string line in _lines)
            {
                if (firstCharacterIsNumber.IsMatch(line))
                {
                    matched.Add(line);
                }
            }
            return matched;
        }

        private string ReadPDFText()
        {
            using (PdfReader reader = new PdfReader(_stream))
            {
                StringBuilder text = new StringBuilder();

                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
                }

                return text.ToString();
            }
        }

    }
}
