﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspergillosisEPR.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using AspergillosisEPR.Models.CaseReportForms;
using AspergillosisEPR.Models.CaseReportForms.ViewModels;

namespace AspergillosisEPR.Controllers.DataTables
{
    public class DataTableCaseReportFormSectionsController : DataTablesController
    {
        public DataTableCaseReportFormSectionsController(AspergillosisContext context)
        {
            _aspergillosisContext = context;
            _list = new List<dynamic>();
        }

        [Authorize(Roles = "Read Role, Admin Role")]
        public IActionResult Load()
        {
            Action queriesAction = () =>
            {
                QueryCaseReportSections();
                SingleSearch();
            };
            return LoadData(queriesAction);
        }


        public void QueryCaseReportSections()
        {
            var results = _aspergillosisContext.CaseReportFormSections
                                         .Include(crfs => crfs.CaseReportFormResultFields)
                                         .ToList<dynamic>();
            foreach (CaseReportFormSection result in results)
            {
                var viewModel = new CaseReportFormSectionViewModel();
                viewModel.Name = result.Name;
                viewModel.ID = result.ID;
                viewModel.FieldNames = result.CaseReportFormResultFields.Select(crfrf => crfrf.Label).ToList();
                _list.Add(viewModel);
            }
        }

        private void SingleSearch()
        {
            if (!string.IsNullOrEmpty(_searchValue))
            {
                _list = _list
                        .Where(crfog => crfog.FieldNames.Contains(_searchValue)  
                                || crfog.Name.Contains(_searchValue)).ToList();
            }
        }
    }
}