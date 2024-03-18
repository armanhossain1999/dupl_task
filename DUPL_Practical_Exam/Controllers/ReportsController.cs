using DUPL_Practical_Exam.Models;
using DUPL_Practical_Exam.ReportFiles;
using DUPL_Practical_Exam.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DUPL_Practical_Exam.Controllers
{
    public class ReportsController : Controller
    {
        private readonly BookDbContext db = new BookDbContext();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetReport(ReportRange reportRange)
        {
            var data = db.Books.AsEnumerable().Where(x=> x.Date.Date >= reportRange.FromDate.Value.Date && x.Date.Date<= reportRange.ToDate.Value.Date).ToList();
            reportRange.Books = data;
            return View("Index", reportRange);
        }

        //To Get Pdf
        public ActionResult GetPdf(DateTime from, DateTime to)
        {
            var data = db.Books.AsEnumerable().Where(x => x.Date.Date >= from.Date && x.Date.Date <= to.Date).ToList();
            RptBooks rpt = new RptBooks();
            rpt.Load();
            rpt.SetDataSource(data);
            Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }
        //To Get Excel
        public ActionResult GetExcel(DateTime from, DateTime to)
        {
            var data = db.Books.AsEnumerable().Where(x => x.Date.Date >= from.Date && x.Date.Date <= to.Date).ToList();
            RptBooks rpt = new RptBooks();
            rpt.Load();
            rpt.SetDataSource(data);
            Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
            return File(s, "application/vnd.ms-excel");
        }
    }
}