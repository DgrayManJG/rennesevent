using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace DAL
{
    public class ImageRepository
    {
        private static ImageRepository Instance;
        private readonly Context db;

        public ImageRepository()
        {
            db = GetDbContext.GetContext();
        }

        public static ImageRepository GetInstance()
        {
            return Instance ?? (Instance = new ImageRepository());
        }

        public List<Image> GetImagesByIdEvent(int idEvent)
        {
            try
            {
                return db.Set<Image>().Where(i => i.Evenement.Id == idEvent).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
