using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BO;
using DAL;
using RennesEvent.Models;
using System.IO;
using System.Web;
using RennesEvent.Services;

namespace RennesEvent.Controllers
{
    [Authorize]
    public class EvenementsController : Controller
    {
        // GET: Evenements
        public ActionResult Index()
        {
            return View(DalGenerique<Evenement>.GetInstance().GetAll());
        }

        // GET: Evenements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var evenement = DalGenerique<Evenement>.GetInstance().GetById(id);
            if (evenement == null)
            {
                return HttpNotFound();
            }
            return View(evenement);
        }

        // GET: Evenements/Create
        public ActionResult Create()
        {
            var vm = new CreateEditEvenementVM
            {
                Themes = DalGenerique<Theme>.GetInstance().GetAll()
            };

            return View(vm);
        }

        // POST: Evenements/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(CreateEditEvenementVM eventVm)
        {
            var isCreated = EvenementsService.CreateEvenement(Request, Server, eventVm);

            return RedirectToAction(!isCreated ? "Create" : "Index");
        }

        // GET: Evenements/Edit/5
        public ActionResult Edit(int id)
        {
            var vm = new CreateEditEvenementVM
            {
                Evenement = DalGenerique<Evenement>.GetInstance().GetById(id),
                Themes = DalGenerique<Theme>.GetInstance().GetAll(),
                Images = ImageRepository.GetInstance().GetImagesByIdEvent(id)
            };

            return View(vm);
        }

        // POST: Evenements/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CreateEditEvenementVM eventVm)
        {
            EvenementsService.UpdateEvenement(eventVm);

            return RedirectToAction("Index");
        }

        // GET: Evenements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var evenement = DalGenerique<Evenement>.GetInstance().GetById(id);
            if (evenement == null)
            {
                return HttpNotFound();
            }
            return View(evenement);
        }

        // POST: Evenements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EvenementRepository.GetInstance().Delete(id);

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {

            base.Dispose(disposing);
        }
    }
}
