﻿using Microsoft.EntityFrameworkCore;
using RabbitConsumers.PatientAdministrationSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitConsumers.PatientAdministrationSystem.Data
{
    class PASDbContext : DbContext
    {
        public DbSet<LpiPatientData> LpiPatientData { get; set; }

        public PASDbContext(DbContextOptions<PASDbContext> options) : base(options)
        {           
        }
    }
}
