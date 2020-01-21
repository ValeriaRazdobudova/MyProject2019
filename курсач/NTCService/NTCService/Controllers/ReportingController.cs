using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NTCService.Models;

namespace NTCService.Controllers
{
    [Authorize(Roles = "Admin")]
    //Клас ReportingController - для відображення звітів
    public class ReportingController : Controller
    {  
       ntcserviceEntitiesProc db = new ntcserviceEntitiesProc();

        //Action для сторінки зі списком звітів
        public ActionResult Index()
        {       
            return View();
        }


        //Загальний прибуток
        public PartialViewResult GeneralProfit()
        {
            return PartialView(db.General_profit());
        }

        //Прибуток за місяць
        public PartialViewResult ProfitPerMonth()
        {
            return PartialView(db.Profit_per_month());
        }

        //Загальна кількість продажів
        public PartialViewResult GeneralAmountSales()
        {
            return PartialView(db.General_amount_sales());
        }

        //Загальна платня працівникам
        public PartialViewResult GeneralPayWorkerSalary()
        {
            return PartialView(db.General_pay_workers_salary());
        }

        //Загальний відсоток платні працівникам
        public PartialViewResult GeneralPayWorkerPercentage()
        {
            return PartialView(db.General_pay_workers_percentage());
        }

        //Загальна кількість клієнтів
        public PartialViewResult GeneralAmountClients()
        {
            return PartialView(db.General_amount_clients());
        }

        //Загальна кількість працівників
        public PartialViewResult GeneralAmountWorkers()
        {
            return PartialView(db.General_amount_workers());
        }

        //Загальна кількість обладнання
        public PartialViewResult GeneralAmountEquipment()
        {
            return PartialView(db.General_amount_equipment());
        }

        //Загальна кількість постачальників обладнання
        public PartialViewResult GeneralAmountEquipmentProviders()
        {
            return PartialView(db.General_amount_equipment_providers());
        }
    }
}