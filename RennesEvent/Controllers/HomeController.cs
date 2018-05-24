using BO;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RennesEvent.Models;
using System.Net;

namespace RennesEvent.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<Evenement> evenements = DalGenerique<Evenement>.GetInstance().GetAll();

            List<CreateEditEvenementVM> evenementsVM = new List<CreateEditEvenementVM>();
            DateTime now = DateTime.Now;

            foreach (var evenement in evenements.Where(a => a.DateDebut > now).OrderByDescending(a => a.DateDebut))
            {
                var defaultImage = evenement.Images.FirstOrDefault(a => a.IsDefault == true && a.Evenement.Images.Count() > 0);

                var genericImage = new Image()
                {
                    Nom = "defaultImage.png"
                };

                var vm = new CreateEditEvenementVM
                {
                    defaultImage = defaultImage ?? genericImage,
                    Evenement = evenement
                };

                evenementsVM.Add(vm);

            }

            return View(evenementsVM);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View(DalParking.GetAll());
        }

        public ActionResult Evenement(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evenement evenement = DalGenerique<Evenement>.GetInstance().GetById(id);

            if (evenement == null)
            {
                return HttpNotFound();
            }

            CreateEditEvenementVM evenementsVM = new CreateEditEvenementVM();

            var defaultImage = evenement.Images.FirstOrDefault(a => a.IsDefault == true && a.Evenement.Images.Any());

            var genericImage = new Image()
            {
                Nom = "defaultImage.png"
            };

            evenementsVM.defaultImage = defaultImage ?? genericImage;
            evenementsVM.Evenement = evenement;

            return View(evenementsVM);
        }
    }
}