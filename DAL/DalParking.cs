using System;
using BO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using BO.ApiParkings;

namespace DAL
{
    public class DalParking
    {
        public static List<Parking> GetAll()
        {
            List<Parking> listeParkings = new List<Parking>();
            var client = new WebClient();
            client.Encoding = Encoding.UTF8;
            var adresse = @"http://data.citedia.com/r1/parks/?crs=EPSG:4326";
            var json = client.DownloadString(adresse);
            RootObject rootObject = JsonConvert.DeserializeObject<RootObject>(json);

            //Donnees des parking
            foreach(var park in rootObject.parks)
            {
                if (park != null && park.parkInformation != null && park.parkInformation.free >= 10)
                {
                    listeParkings.Add(new Parking
                    {
                        Identifiant = park.id,
                        Nom = park.parkInformation.name,
                        Statut = park.parkInformation.status,
                        PlacesMax = park.parkInformation.max,
                        PlacesLibres = park.parkInformation.free
                    });
                }
            }

            //Coordonnees des parkings
            foreach (var feature in rootObject.features.features)
            {
                var parkingSelected = listeParkings.FirstOrDefault(f => f.Identifiant == feature.id);
                if (parkingSelected != null)
                {
                    parkingSelected.Coordonnees = FormatCoordonnees(feature.geometry.coordinates);
                }
            }

            //TexteTarifs et adresses des parkings
            List<ParkingPrice> listeTarifs = SplitCSV(@"http://data.citedia.com/r1/parks/timetable-and-prices");
            foreach (var tarif in listeTarifs)
            {
                Parking p = listeParkings.FirstOrDefault(f => f.Identifiant == tarif.Id);
                if (p != null)
                {
                    p.TexteHoraires = tarif.Horaires;
                    p.Adresse = tarif.Adresse;
                    p.TexteTarifs = tarif.Tarifs;
                }
            }

            foreach (var parking in listeParkings)
            {
                parking.Adresse = DalGoogleMaps.GetAddressFromLatLng(parking.Coordonnees);
                parking.LastUpdate = DateTime.Now;
            }

            //TODO Creer objets horaires et tarifs

            //Horaires
            Horaire h = new Horaire();
            


            //Tarifs
            Tarif t = new Tarif();


            SaveParkings(listeParkings);

            return listeParkings;
        }


        private static void SaveParkings(List<Parking> listeParkings)
        {
            DalGenerique<Parking> dalParking = DalGenerique<Parking>.GetInstance();

            foreach (Parking parking in dalParking.GetAll())
            {
                if (listeParkings.FirstOrDefault(p => p.Identifiant == parking.Identifiant) == null)
                {
                    dalParking.Create(parking);
                }
                else
                {
                    parking.LastUpdate = DateTime.Now;
                    ParkingRepository.GetInstance().Update(parking);
                }
                
            }
            
        }


        private static string GetCSV(string url)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            StreamReader sr = new StreamReader(resp.GetResponseStream());
            string results = sr.ReadToEnd();
            sr.Close();

            return results;
        }

        private static List<ParkingPrice> SplitCSV(string url)
        {
            List<ParkingPrice> splitted = new List<ParkingPrice>();
            string fileList = GetCSV(url);

            var tempStr = fileList.Split('\n');

            for (int i=1;i < tempStr.Length;i++)
            {
                if (!string.IsNullOrWhiteSpace(tempStr[i]))
                {
                    tempStr[i].Replace("\r", string.Empty);

                    Regex CSVParser = new Regex(";(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
                    String[] ligne = CSVParser.Split(tempStr[i]);
                    
                    splitted.Add(new ParkingPrice
                    {
                        Id = ligne[0].Replace("\"", string.Empty),
                        Horaires = ligne[1].Replace("\"", string.Empty),
                        Tarifs = ligne[2].Replace("\"", string.Empty),
                        Adresse = ligne[3].Replace("\"", string.Empty)
                        //Capacite = int.Parse(ligne[4]),
                        //SeuilComplet = int.Parse(ligne[5])
                    });


                    //Tentative de formattage des infos...

                    //horaires
                    /*string[] horaires = ligne[1].Replace("\"", string.Empty).Split(';');
                    
                    foreach (var chaine in horaires)
                    {
                        string[] temp = chaine.Split(' ');
                        if (temp.Length > 1)
                        {
                            string chaineJours = temp[0];
                            string[] jours = chaineJours.Split('-');

                            string chaineHeures = temp[1];
                            string[] heures = chaineHeures.Split('-');
                            TimeSpan[] tabHeureDebutFin = new TimeSpan[2];
                            tabHeureDebutFin[0] = TimeSpan.Parse(heures[0]);
                            tabHeureDebutFin[1] = TimeSpan.Parse(heures[1]);
                            
                        }
                    }

                    //tarifs
                    string[] tarifs = ligne[2].Replace("\"", string.Empty).Split(';');*/

                }
            }

            return splitted;
        }

        private static string FormatCoordonnees(List<double> coordonnees)
        {
            return coordonnees.ElementAt(1).ToString().Replace(",",".")+","+ coordonnees.ElementAt(0).ToString().Replace(",", ".");
        }


        public static Parking GetById(string idParking)
        {
            var client = new WebClient();
            client.Encoding = Encoding.UTF8;
            var adresse = @"http://data.citedia.com/r1/parks/"+ idParking;
            var json = client.DownloadString(adresse);
            ParkInformation parkInformation = JsonConvert.DeserializeObject<ParkInformation>(json);

            var parking = new Parking
            {
                Identifiant = idParking,
                Nom = parkInformation.name,
                Statut = parkInformation.status,
                PlacesMax = parkInformation.max,
                PlacesLibres = parkInformation.free
            };

            return parking;
        }
    }
}



















