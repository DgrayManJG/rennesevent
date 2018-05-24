using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BO;
using DAL;
using RennesEvent.Models;

namespace RennesEvent.Controllers
{
    [Authorize]
    public class ImagesController : Controller
    {

        // GET: Image
        public ActionResult Index()
        {
            return View(DalGenerique<Image>.GetInstance().GetAll());
        }

        // GET: Image/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Image image = DalGenerique<Image>.GetInstance().GetById(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        // GET: Image/Create
        public ActionResult Create()
        {
            var vm = new CreateEditImageVM
            {
                Evenements = DalGenerique<Evenement>.GetInstance().GetAll()
            };

            return View(vm);
        }

        // POST: Image/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Identifiant,Nom,IsDefault")] CreateEditImageVM imageVm)
        {
            if (imageVm.IdSelectedEvent.HasValue)
                imageVm.Image.Evenement = DalGenerique<Evenement>.GetInstance().GetById(imageVm.IdSelectedEvent.Value);
            DalGenerique<Image>.GetInstance().Create(imageVm.Image);

            return RedirectToAction("Index");
        }

        // GET: Image/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Image image = DalGenerique<Image>.GetInstance().GetById(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        // POST: Image/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Identifiant,Nom,IsDefault")] Image image)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View(image);
        }

        // GET: Image/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Image image = DalGenerique<Image>.GetInstance().GetById(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        // POST: Image/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }
    }
}
