using DAL.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BL
{
 public class KategoryDAL
    {
        DBContext DB = new DBContext();

        public KategoryTbl GetKategoryByCode(int code)
        {
            KategoryTbl currentKategory = DB.KategoryTbls.Where(x => x.CodeKategory == code).FirstOrDefault();
            return currentKategory;
        }

        public List<KategoryTbl> GetAllKategory()
        {
            List<KategoryTbl> KategoryTbl = DB.KategoryTbls.ToList();
            return KategoryTbl;
        }
        public List<KategoryTbl> AddKategory(KategoryTbl c)
        {
            try
            {
                DB.KategoryTbls.Add(c);
                DB.SaveChanges();
                return DB.KategoryTbls.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<KategoryTbl> UppdateKategory(KategoryTbl cat, int id)
        {
            try
            {
                KategoryTbl c = DB.KategoryTbls.FirstOrDefault(x => x.CodeKategory == id);
                if (c != null)
                {

                    c.NameKategory = cat.NameKategory;
                    DB.SaveChanges();
                }
                return DB.KategoryTbls.ToList();
            }
            catch (Exception)
            {
                throw;

            }
        }

        public List<KategoryTbl> RemoveKategory(int id)
        {

            try
            {
                KategoryTbl c = GetKategoryByCode(id);
                if (c == null)
                    throw new ArgumentException("טעות! ניסית למחוק קטגורייה שאינה נמצאת...");

                else
                {

                    this.DB.Remove(DB.KategoryTbls.FirstOrDefault(x => x.CodeKategory == id));
                    //changeKategoryTblToDefalt(id);
                    //this.DB.Products.ToList().ForEach(x => { if (x.CodeKategoryTbl == id) x.CodeKategoryTbl = 43; });
                    this.DB.SaveChanges();
                    return DB.KategoryTbls.ToList();


                }


            }
            catch (Exception)
            {
                throw;

            }

        }

        //public void changeKategoryTblToDefalt(int id)
        //{
        //    try
        //    {
        //        DB.Products.ToList().ForEach(x => { if (x.CodeKategoryTbl == id) x.CodeKategoryTbl = 43; });

        //    }
        //    catch
        //    {

        //    }

        //}
    }
}
