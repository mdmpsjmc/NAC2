﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AspergillosisEPR.Controllers.Patients
{
    public class SmokingStatusesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}