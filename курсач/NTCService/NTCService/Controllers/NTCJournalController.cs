using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NTCService;
using PagedList;

namespace NTCService.Controllers
{
    [Authorize(Roles = "Admin")]

    //Клас NTCJournaController - для роботи з журналом
    public class NTCJournalController : Controller
    {
        private ModelDB db = new ModelDB();

        //Action для сторінки з записами
        public ActionResult Index(string sortOrder, string SearchByClient, string SearchByWorker, string SearchByService, 
            string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "date_recording_desc" : "";
            ViewBag.ClientSortParm = sortOrder == "name_client" ? "name_client_desc" : "name_client";
            ViewBag.WorkerSortParm = sortOrder == "name_worker" ? "name_worker_desc" : "name_worker";
            ViewBag.TimeSortParm = sortOrder == "time_recording" ? "time_recording_desc" : "time_recording";
            ViewBag.ServicestSortParm = sortOrder == "name_service" ? "name_service_desc" : "name_service";
            ViewBag.AmountSortParm = sortOrder == "amount" ? "amount_desc" : "amount";
            ViewBag.PriceSortParm = sortOrder == "total_price" ? "total_price_desc" : "total_price";
            ViewBag.WorkerPersentSortParm = sortOrder == "worker_percentage" ? "worker_percentage_desc" : "worker_percentage";

            if (SearchByClient != null)
            {
                page = 1;
            }
            else
            {
                SearchByClient = currentFilter;
            }

            if (SearchByWorker != null)
            {
                page = 1;
            }
            else
            {
                SearchByWorker = currentFilter;
            }

            if (SearchByService != null)
            {
                page = 1;
            }
            else
            {
                SearchByService = currentFilter;
            }

            ViewBag.CurrentFilter = SearchByClient;
            ViewBag.CurrentFilter = SearchByWorker;
            ViewBag.CurrentFilter = SearchByService;

            var journal = db.NTCJournal.Include(n => n.NTCClients).Include(n => n.NTCServices).Include(n => n.NTCWorkers);

            if (!String.IsNullOrEmpty(SearchByClient))
            {
                journal = journal.Where(s => s.NTCClients.name_NTCclient.Contains(SearchByClient));
            }
            if (!String.IsNullOrEmpty(SearchByWorker))
            {
                journal = journal.Where(s => s.NTCWorkers.name_NTCworker.Contains(SearchByWorker));
            }
            if (!String.IsNullOrEmpty(SearchByService))
            {
                journal = journal.Where(s => s.NTCServices.name_NTCservice.Contains(SearchByService));
            }

            switch (sortOrder)
            {
                case "date_recording_desc":
                    journal = journal.OrderByDescending(s => s.date_NTCjournal);
                    break;
                case "name_client":
                    journal = journal.OrderBy(s => s.NTCClients.name_NTCclient);
                    break;
                case "name_client_desc":
                    journal = journal.OrderByDescending(s => s.NTCClients.name_NTCclient);
                    break;
                case "name_worker":
                    journal = journal.OrderBy(s => s.NTCWorkers.name_NTCworker);
                    break;
                case "name_worker_desc":
                    journal = journal.OrderByDescending(s => s.NTCWorkers.name_NTCworker);
                    break;
                case "time_recording":
                    journal = journal.OrderBy(s => s.time_NTCjournal);
                    break;
                case "time_recording_desc":
                    journal = journal.OrderByDescending(s => s.time_NTCjournal);
                    break;
                case "name_service":
                    journal = journal.OrderBy(s => s.NTCServices.name_NTCservice);
                    break;
                case "name_service_desc":
                    journal = journal.OrderByDescending(s => s.NTCServices.name_NTCservice);
                    break;
                case "amount":
                    journal = journal.OrderBy(s => s.amount_NTCequipment);
                    break;
                case "amount_desc":
                    journal = journal.OrderByDescending(s => s.amount_NTCequipment);
                    break;
                case "total_price":
                    journal = journal.OrderBy(s => s.total_price);
                    break;
                case "total_price_desc":
                    journal = journal.OrderByDescending(s => s.total_price);
                    break;
                case "worker_percentage":
                    journal = journal.OrderBy(s => s.NTCworker_percentage);
                    break;
                case "worker_percentage_desc":
                    journal = journal.OrderByDescending(s => s.NTCworker_percentage);
                    break;
                default:
                    journal = journal.OrderBy(s => s.date_NTCjournal);
                    break;
            }

            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(journal.ToPagedList(pageNumber, pageSize));
        }

        //Action для сторінки деталей
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NTCJournal nTCJournal = await db.NTCJournal.FindAsync(id);
            if (nTCJournal == null)
            {
                return HttpNotFound();
            }
            return View(nTCJournal);
        }

        //Action-и для сторінки зі створення
        public ActionResult Create()
        {
            ViewBag.id_NTCclient = new SelectList(db.NTCClients, "id_NTCclient", "name_NTCclient");
            ViewBag.id_NTCservice = new SelectList(db.NTCServices, "id_NTCservice", "name_NTCservice");
            ViewBag.id_NTCworker = new SelectList(db.NTCWorkers, "id_NTCworker", "name_NTCworker");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id_NTCjournal,id_NTCclient,id_NTCworker,date_NTCjournal," +
            "time_NTCjournal,id_NTCservice,amount_NTCequipment,total_price,NTCworker_percentage")] NTCJournal nTCJournal)
        {
            if (ModelState.IsValid)
            {
                db.NTCJournal.Add(nTCJournal);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.id_NTCclient = new SelectList(db.NTCClients, "id_NTCclient", "name_NTCclient", nTCJournal.id_NTCclient);
            ViewBag.id_NTCservice = new SelectList(db.NTCServices, "id_NTCservice", "name_NTCservice", nTCJournal.id_NTCservice);
            ViewBag.id_NTCworker = new SelectList(db.NTCWorkers, "id_NTCworker", "name_NTCworker", nTCJournal.id_NTCworker);
            return View(nTCJournal);
        }

        //Action-и для редагування
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NTCJournal nTCJournal = await db.NTCJournal.FindAsync(id);
            if (nTCJournal == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_NTCclient = new SelectList(db.NTCClients, "id_NTCclient", "name_NTCclient", nTCJournal.id_NTCclient);
            ViewBag.id_NTCservice = new SelectList(db.NTCServices, "id_NTCservice", "name_NTCservice", nTCJournal.id_NTCservice);
            ViewBag.id_NTCworker = new SelectList(db.NTCWorkers, "id_NTCworker", "name_NTCworker", nTCJournal.id_NTCworker);
            return View(nTCJournal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id_NTCjournal,id_NTCclient,id_NTCworker,date_NTCjournal," +
            "time_NTCjournal,id_NTCservice,amount_NTCequipment,total_price,NTCworker_percentage")] NTCJournal nTCJournal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nTCJournal).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.id_NTCclient = new SelectList(db.NTCClients, "id_NTCclient", "name_NTCclient", nTCJournal.id_NTCclient);
            ViewBag.id_NTCservice = new SelectList(db.NTCServices, "id_NTCservice", "name_NTCservice", nTCJournal.id_NTCservice);
            ViewBag.id_NTCworker = new SelectList(db.NTCWorkers, "id_NTCworker", "name_NTCworker", nTCJournal.id_NTCworker);
            return View(nTCJournal);
        }

        //Action-и для видалення
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NTCJournal nTCJournal = await db.NTCJournal.FindAsync(id);
            if (nTCJournal == null)
            {
                return HttpNotFound();
            }
            return View(nTCJournal);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            NTCJournal nTCJournal = await db.NTCJournal.FindAsync(id);
            db.NTCJournal.Remove(nTCJournal);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //звільнення некерованих ресурсів
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
