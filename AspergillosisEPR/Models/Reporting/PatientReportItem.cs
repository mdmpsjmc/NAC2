﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspergillosisEPR.Models.Reporting
{
    public class PatientReportItem
    {
        public int ID { get; set; }
        public int PatientId { get; set; }
        public int ReportId { get; set; }

        public Patient Patient { get; set; }
        public Report Report { get; set; }
    }
}
