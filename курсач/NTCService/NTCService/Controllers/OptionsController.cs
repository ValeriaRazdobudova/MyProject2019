using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using OfficeOpenXml;

namespace NTCService.Controllers
{
    [Authorize(Roles = "Admin")]


    public class OptionsController : Controller
    {
        private ModelDB db = new ModelDB();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BackupDatabase()
        {
            using (var db = new ModelDB())
            {
                
                var cmd = String.Format
                                    ("BACKUP DATABASE ntcservice TO DISK='D:\\юніті срака\\MSSQL12.SQLEXPRESS\\MSSQL\\Backup\\" +
                                    "ntcservice.bak' WITH FORMAT, MEDIANAME='Full Db backup', MEDIADESCRIPTION='Full Database Backup';");
                db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, cmd);
            }
            return View();
        }

        public ActionResult RestoreDatabase()
        {
            using (var db = new ModelDB())
            {
                //var cmd = String.Format
                //    ("USE master restore DATABASE ntcservice  from DISK='D:\\юніті срака\\MSSQL12.SQLEXPRESS\\MSSQL\\Backup\\" +
                //    "ntcservice.bak' WITH REPLACE;");
                //db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, cmd);
            }
            return View();
        }

        public FileContentResult Export()
        {

            var fileDownloadName = String.Format("Журнал NTCService.xlsx");
            const string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            ExcelPackage package = GenerateExcelFile(db.NTCJournal.ToList());

            var fsr = new FileContentResult(package.GetAsByteArray(), contentType);
            fsr.FileDownloadName = fileDownloadName;

            return fsr;
        }

        private static ExcelPackage GenerateExcelFile(IEnumerable<NTCJournal> datasource)
        {

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Таблиця");

            ws.Cells[1, 1].Value = "Клієнт ";
            ws.Cells[1, 2].Value = "Інженер";
            ws.Cells[1, 3].Value = "Дата";
            ws.Cells[1, 4].Value = "Час";
            ws.Cells[1, 5].Value = "Послуга";
            ws.Cells[1, 6].Value = "К-сть обладнання";
            ws.Cells[1, 7].Value = "Повна ціна";
            ws.Cells[1, 8].Value = "Відсоток інженера";


            for (int i = 0; i < datasource.Count(); i++)
            {
                ws.Cells[i + 2, 1].Value = datasource.ElementAt(i).NTCClients.name_NTCclient;
                ws.Cells[i + 2, 2].Value = datasource.ElementAt(i).NTCWorkers.name_NTCworker;
                ws.Cells[i + 2, 3].Value = datasource.ElementAt(i).date_NTCjournal;
                ws.Cells[i + 2, 4].Value = datasource.ElementAt(i).time_NTCjournal;
                ws.Cells[i + 2, 5].Value = datasource.ElementAt(i).NTCServices.name_NTCservice;
                ws.Cells[i + 2, 6].Value = datasource.ElementAt(i).amount_NTCequipment;
                ws.Cells[i + 2, 7].Value = datasource.ElementAt(i).total_price;
                ws.Cells[i + 2, 8].Value = datasource.ElementAt(i).NTCworker_percentage;
            }

            return pck;
        }
    }
}