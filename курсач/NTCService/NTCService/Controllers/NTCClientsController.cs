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
    //Клас NTCClientsController - для роботи з типами техніки та ПЗ
    [Authorize(Roles = "Admin")]
    public class NTCClientsController : Controller
    {
        private ModelDB db = new ModelDB();

        //Action для сторінки зі клієнтів

        public ActionResult Index(string sortOrder, string SearchByName, string SearchByNumb, string SearchByMail, string SearchBySkype, 
            string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PhoneNumbSortParm = sortOrder == "phone_numb" ? "phone_numb_desc" : "phone_numb";
            ViewBag.EmailSortParm = sortOrder == "email" ? "email_desc" : "email";
            ViewBag.SkypeSortParm = sortOrder == "skype" ? "skype_desc" : "skype";

            if (SearchByName != null)
            {
                page = 1;
            }
            else
            {
                SearchByName = currentFilter;
            }
            if (SearchByNumb != null)
            {
                page = 1;
            }
            else
            {
                SearchByNumb = currentFilter;
            }
            if (SearchByMail != null)
            {
                page = 1;
            }
            else
            {
                SearchByMail = currentFilter;
            }
            if (SearchBySkype != null)
            {
                page = 1;
            }
            else
            {
                SearchBySkype = currentFilter;
            }

            ViewBag.CurrentFilter = SearchByName;
            ViewBag.CurrentFilter = SearchByNumb;
            ViewBag.CurrentFilter = SearchByMail;
            ViewBag.CurrentFilter = SearchBySkype;

            var clients = from s in db.NTCClients select s;

            if (!String.IsNullOrEmpty(SearchByName))
            {
                clients = clients.Where(s => s.name_NTCclient.Contains(SearchByName));
            }

            if (!String.IsNullOrEmpty(SearchByNumb))
            {
                clients = clients.Where(s => s.phone_number_NTCclient.Contains(SearchByNumb));
            }

            if (!String.IsNullOrEmpty(SearchByMail))
            {
                clients = clients.Where(s => s.email_NTCclient.Contains(SearchByMail));
            }
            if (!String.IsNullOrEmpty(SearchBySkype))
            {
                clients = clients.Where(s => s.skype_NTCclient.Contains(SearchBySkype));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    clients = clients.OrderByDescending(s => s.name_NTCclient);
                    break;
                case "phone_numb":
                    clients = clients.OrderBy(s => s.phone_number_NTCclient);
                    break;
                case "phone_numb_desc":
                    clients = clients.OrderByDescending(s => s.phone_number_NTCclient);
                    break;
                case "email":
                    clients = clients.OrderBy(s => s.email_NTCclient);
                    break;
                case "email_desc":
                    clients = clients.OrderByDescending(s => s.email_NTCclient);
                    break;
                case "skype":
                    clients = clients.OrderBy(s => s.skype_NTCclient);
                    break;
                case "skype_desc":
                    clients = clients.OrderByDescending(s => s.skype_NTCclient);
                    break;
                default:
                    clients = clients.OrderBy(s => s.name_NTCclient);
                    break;
            }

            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(clients.ToPagedList(pageNumber, pageSize));
        }

        //Action-и для сторінки зі створення
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id_NTCclient,name_NTCclient,phone_number_NTCclient," +
            "email_NTCclient,skype_NTCclient")] NTCClients nTCClients)
        {
            if (ModelState.IsValid)
            {
                db.NTCClients.Add(nTCClients);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(nTCClients);
        }

        //Action-и для редагування
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NTCClients nTCClients = await db.NTCClients.FindAsync(id);
            if (nTCClients == null)
            {
                return HttpNotFound();
            }
            return View(nTCClients);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id_NTCclient,name_NTCclient,phone_number_NTCclient," +
            "email_NTCclient,skype_NTCclient")] NTCClients nTCClients)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nTCClients).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(nTCClients);
        }

        //Action-и для видалення
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NTCClients nTCClients = await db.NTCClients.FindAsync(id);
            if (nTCClients == null)
            {
                return HttpNotFound();
            }
            return View(nTCClients);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            NTCClients nTCClients = await db.NTCClients.FindAsync(id);
            db.NTCClients.Remove(nTCClients);
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
