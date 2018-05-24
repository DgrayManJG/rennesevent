using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BO;
using BO.ApiGoogleMaps;
using Newtonsoft.Json;

namespace DAL
{
    public class DalGoogleMaps
    {
        public static Itineraire GetItineraire(string origine, string destination)
        {
            var url = "https://maps.googleapis.com/maps/api/distancematrix/json?origins="+ origine + "&destinations="+ destination + "&key=AIzaSyALhF417BMAZPVB1jUugfoV13sTgMZwmMI";
            var client = new WebClient();
            client.Encoding = Encoding.UTF8;
            var json = client.DownloadString(url);
            RootObject rootObject = JsonConvert.DeserializeObject<RootObject>(json);

            Itineraire itineraire = new Itineraire
            {
                Origine = origine,
                Destination = destination,
                DistanceKm = rootObject.rows.ElementAt(0).elements.ElementAt(0).distance.text,
                DistanceMetres = rootObject.rows.ElementAt(0).elements.ElementAt(0).distance.value,
                DureeHeuresMinutes = rootObject.rows.ElementAt(0).elements.ElementAt(0).duration.text,
                DureeSecondes = rootObject.rows.ElementAt(0).elements.ElementAt(0).duration.value
            };
            return itineraire;
        }


        public static string GetAddressFromLatLng(string latLng)
        {
            //string[] tabLatLng = latLng.Split(',');
            //latLng = tabLatLng[0] + "." + tabLatLng[1] + "," + tabLatLng[2] + "." + tabLatLng[3];
            var url = "https://maps.googleapis.com/maps/api/geocode/json?latlng="+ latLng + "&key=AIzaSyALhF417BMAZPVB1jUugfoV13sTgMZwmMI";
            var client = new WebClient();
            client.Encoding = Encoding.UTF8;
            var json = client.DownloadString(url);
            RootObject2 rootObject2 = JsonConvert.DeserializeObject<RootObject2>(json);
            
            return rootObject2.results.ElementAt(0).formatted_address;
        }

        public static string GetLatLngFromAddress(string adresse)
        {
            var url = "https://maps.googleapis.com/maps/api/geocode/json?address="+adresse+"&key=AIzaSyALhF417BMAZPVB1jUugfoV13sTgMZwmMI";
            var client = new WebClient();
            client.Encoding = Encoding.UTF8;
            var json = client.DownloadString(url);
            RootObject2 rootObject2 = JsonConvert.DeserializeObject<RootObject2>(json);

            string latLng = rootObject2.results.ElementAt(0).geometry.location.lat.ToString() + "," +
                            rootObject2.results.ElementAt(0).geometry.location.lng.ToString();
            string[] tabLatLng = latLng.Split(',');
            latLng = tabLatLng[0] + "." + tabLatLng[1] + "," + tabLatLng[2] + "." + tabLatLng[3];

            return latLng;
        }


        

        

        

        

        

        



        

        

        

        
        


        

        

        

        


    }


}
