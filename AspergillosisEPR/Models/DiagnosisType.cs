﻿using AspergillosisEPR.Lib.Search;
using AspergillosisEPR.Models.Patients;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AspergillosisEPR.Models
{
    public class DiagnosisType
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public string ShortName { get; set; }
        [JsonIgnore]
        public ICollection<PatientDiagnosis> PatientDiagnoses { get; set; }

        public string KlassName => typeof(DiagnosisType).Name;

        public string DisplayName()
        {
            if (string.IsNullOrEmpty(ShortName))
            {
                return Name;
            } else
            {
                return ShortName + " (" + Name + ")";
            }
        }
    }
}