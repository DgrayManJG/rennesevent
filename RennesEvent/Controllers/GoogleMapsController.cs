using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BO;
using DAL;
using System.Net;
using RennesEvent.Models;

namespace RennesEvent.Controllers
{
    public class GoogleMapsController : Controller
    {
        Context db = GetDbContext.GetContext();
        // GET: GoogleMaps/id_evenement
        public ActionResult Index(int id)
        {
            Evenement evenement = db.Evenements.Find(id);
            if (evenement == null)
            {
                return HttpNotFound();
            }

            List<dynamic> listeDistances = new List<dynamic>();

            List<Parking> parkingsAll = DalParking.GetAll();

            string adresseEvent = evenement.Adresse.Rue + " " +
                                  evenement.Adresse.Cp + " " + evenement.Adresse.Ville;

            foreach (var parking in parkingsAll)
            {
                
                long distance = DalGoogleMaps.GetItineraire(adresseEvent, parking.Adresse).DistanceMetres;
                listeDistances.Add(new
                {
                    IdParking = parking.Identifiant,
                    Distance = distance
                });
            }

            listeDistances = listeDistances.OrderBy(d => d.Distance).ToList();

            List<Parking> parkings = new List<Parking>();
            parkings.Add(parkingsAll.FirstOrDefault(p => p.Identifiant == listeDistances.ElementAt(0).IdParking));
            parkings.Add(parkingsAll.FirstOrDefault(p => p.Identifiant == listeDistances.ElementAt(1).IdParking));
            parkings.Add(parkingsAll.FirstOrDefault(p => p.Identifiant == listeDistances.ElementAt(2).IdParking));


            ParkingEvenementVM parkingEvenementVM = new ParkingEvenementVM
            {
                parkings = parkings,
                evenement = evenement,
                coordonneesEvenement = DalGoogleMaps.GetLatLngFromAddress(adresseEvent)
            };

            return View(parkingEvenementVM);
        }
    }
}
