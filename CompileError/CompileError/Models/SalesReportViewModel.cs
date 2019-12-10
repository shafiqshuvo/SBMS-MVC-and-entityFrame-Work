using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CompileError.Model.Model;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace CompileError.Models
{
    public class SalesReportViewModel
    {
        public List<Sale> Sales { set; get; }
        public List<SalesDetail> SalesDetails { set; get; }
    }
}