using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using BO;
using DAL;
using RennesEvent.Models;

namespace RennesEvent.Services
{
    public class EvenementsService
    {

        public static void UpdateEvenement(CreateEditEvenementVM eventVm)
        {
            if (eventVm.IdSelectedTheme.HasValue)
            {
                eventVm.Evenement.Theme = DalGenerique<Theme>.GetInstance().GetById(eventVm.IdSelectedTheme.Value);
            }

            var images = DalGenerique<Image>.GetInstance().GetAll();
            eventVm.Evenement.Images = ImageRepository.GetInstance().GetImagesByIdEvent(eventVm.Evenement.Id);

            EvenementRepository.GetInstance().Update(eventVm.Evenement);
        }

        public static bool CreateEvenement(HttpRequestBase request, HttpServerUtilityBase server, CreateEditEvenementVM eventVm)
        {
            try
            {
                if (eventVm.IdSelectedTheme.HasValue)
                    eventVm.Evenement.Theme = DalGenerique<Theme>.GetInstance().GetById(eventVm.IdSelectedTheme.Value);
                DalGenerique<Evenement>.GetInstance().Create(eventVm.Evenement);

                var evenements = DalGenerique<Evenement>.GetInstance().GetAll();


                var nbFile = request.Files.Count;
                var fileExist = request.Files[0]?.ContentLength ?? 0; // On récupère ContentLength si différent de null
                if (nbFile <= 0 || fileExist <= 0) return false;
                for (var i = 0; i < nbFile; i++)
                {
                    var isCreated = EvenementsService.CreateImage(request, server, eventVm, evenements, i);
                    if (isCreated != false) continue;
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool CreateImage(HttpRequestBase request, HttpServerUtilityBase server,CreateEditEvenementVM eventVm, 
                                List<Evenement> evenements, int index)
        {
            var file = (null != request.Files[index]) ? request.Files[index] : null;

            var filename = Path.GetFileName(file?.FileName); // Check non nullité avec '?'

            if (filename == null) return false;
                
            // TODO - Create Constante pour l'url de stockage des fichiers images des events
            var path = Path.Combine(server.MapPath("~/Content/images/evenements/"), filename);
            file.SaveAs(path);
            

            var img = new Image
            {
                Nom = filename,
                IsDefault = (index == 0) ? true : false, // Première image up désignée par défault
                Evenement = evenements.First(a => a.Id == eventVm.Evenement.Id)
            };

            var isCreate = DalGenerique<Image>.GetInstance().Create(img);

            return (null != isCreate) ? true : false;
        }

    }
}