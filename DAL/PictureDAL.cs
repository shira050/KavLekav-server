using DAL.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
public  class PictureDAL
    {
        DBContext DB = new DBContext();

        public PictureTbl GetPictureByCode(int code)
        {
            PictureTbl currentPicture = DB.PictureTbls.Where(x => x.CodePicture == code).FirstOrDefault();
            return currentPicture;
        }

        public List<PictureTbl> GetAllPicture()
        {
            List<PictureTbl> PictureTbl = DB.PictureTbls.ToList();
            return PictureTbl;
        }
        public List<PictureTbl> AddPicture(PictureTbl p)
        {
            try
            {
                if (p.CascadingPicture == null)
                    p.CascadingPicture = 0;
                if (p.CountOfDownloads == null)
                    p.CountOfDownloads = 0;
                DB.PictureTbls.Add(p);
                DB.SaveChanges();
                return DB.PictureTbls.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<PictureTbl> UppdatePicture(PictureTbl pic, int id)
        {
            try
            {
                PictureTbl p = DB.PictureTbls.FirstOrDefault(x => x.CodePicture == id);
                if (p != null)
                {
     
                   p.CodeUser = pic.CodeUser;
                    p.CodeKategory = pic.CodeKategory;
                    //p.UppDateToWeb = pic.UppDateToWeb;
                    p.CascadingPicture = pic.CascadingPicture;
                    p.CountOfDownloads = pic.CountOfDownloads;
                    p.RouteSoursePicture = pic.RouteSoursePicture;
                    p.RouteGoalPicture = pic.RouteGoalPicture;
                    DB.SaveChanges();
                }
                return DB.PictureTbls.ToList();
            }
            catch (Exception)
            {
                throw;

            }
        }

        public List<PictureTbl> RemovePicture(int id)
        {

            try
            {
                PictureTbl p = GetPictureByCode(id);
                if (p == null)
                    throw new ArgumentException("טעות! ניסית למחוק תמונה שאינה קיימת...");

                else
                {

                    this.DB.Remove(DB.PictureTbls.FirstOrDefault(x => x.CodePicture == id));
                    //changePictureTblToDefalt(id);
                    //this.DB.Products.ToList().ForEach(x => { if (x.CodePictureTbl == id) x.CodePictureTbl = 43; });
                    this.DB.SaveChanges();
                    return DB.PictureTbls.ToList();


                }


            }
            catch (Exception)
            {
                throw;

            }

        }

        public List<CountingTbl> GetAllCountings()
        {
            List<CountingTbl> CountingList= DB.CountingTbls.ToList();
            return CountingList;
        }

        public async void cascading(PictureTbl p,int id) 
        {
            int countAllDounlods = 0;
            if (p.CodeUser != id)
            {

                try
                {
                    foreach (var item in DB.PictureTbls)//סכימת ההורדות של כל התמונות
                    {
                        countAllDounlods += Convert.ToInt32(item.CountOfDownloads);
                    }
                    //if (p.CascadingPicture == null)
                    //    p.CascadingPicture = 0;
                    //if (p.CountOfDownloads ==null)
                    //    p.CountOfDownloads = 0;
                    p.CountOfDownloads += 1;//עדכון ההורדות של התמונה הנוכחית
                    int max = 0;
                    List<PictureTbl> l = new List<PictureTbl>();
                    l = GetAllPicture();
                    foreach (var item in l)
                    {
                        if (item.CountOfDownloads > max)
                            max = Convert.ToInt32(item.CountOfDownloads);
                    }
                    //חישוב כמה הורדות שווה כל כוכב - המרה מאחוזים לכמות הורדות

                    double everyStar = Convert.ToDouble((0.25)*max);
                    //חמישית ממספר ההורדות המקסימאלי
                    if(everyStar!=0)
                  
                        p.CascadingPicture =Convert.ToInt32( p.CountOfDownloads / everyStar);
                   UppdatePicture(p, p.CodePicture);
                    //foreach (var item in DB.PictureTbls)//סכימת ההורדות של כל התמונות
                    //{
                    //    item.CascadingPicture = Convert.ToInt32(item.CountOfDownloads / everyStar);
                    //    UppdatePicture(item, item.CodePicture);

                    //}
                    await Task.Delay(4000);
                    DB.SaveChanges();
                   // return DB.PictureTbls.ToList();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            //else   return DB.PictureTbls.ToList();
        }


        public List<PictureTbl> GetAllPicturesInCategory(int codeKategory)
        {
            List<PictureTbl> p = DB.PictureTbls.Where(x => x.CodeKategory == codeKategory).ToList();
            return p;
        }

        public List<PictureTbl> GetAllPicturesOfOneUser(int codeUser)
        {
            List<PictureTbl> p = DB.PictureTbls.Where(x => x.CodeUser == codeUser).ToList();
            return p;
        }
    }
}
