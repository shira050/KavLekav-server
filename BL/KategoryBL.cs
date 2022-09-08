using AutoMapper;
using DAL.models;
using DTO;
using System;
using System.Collections.Generic;

namespace BL
{
    public class KategoryBL
    {
        KategoryDAL _Cdal = new KategoryDAL();
        IMapper imapper;
        public KategoryBL()
        {
            var config = new MapperConfiguration(cgf => { cgf.AddProfile<AutoProfilMapper>(); });
            imapper = config.CreateMapper();
          //  KategoryDTO currentKategory = imapper < KategoryTbl, kategoryDTO(c);
        }
      
        public KategoryDTO GetKategory(int code)
        {
            KategoryTbl c = _Cdal.GetKategoryByCode(code);
            KategoryDTO currentKategory = imapper.Map < KategoryTbl, KategoryDTO>(c);
            return currentKategory;
        }


        public List<KategoryDTO> GetAllCategories()
        {
            List<KategoryTbl> kats = _Cdal.GetAllKategory();
            List<KategoryDTO> KategoryDTO = new List<KategoryDTO>();
            foreach (var item in kats)
            {
                var c = imapper.Map<KategoryTbl, KategoryDTO>(item);
                KategoryDTO.Add(c);
            }
            return KategoryDTO;
        }

        public List<KategoryTbl> AddKategory(KategoryDTO cat)
        {
            KategoryTbl currentKategory = imapper.Map<KategoryDTO,KategoryTbl> (cat);
            return _Cdal.AddKategory(currentKategory);
        }

        public List<KategoryTbl> UppdateKategory(KategoryDTO cat, int id)
        {
            KategoryTbl currentKategory = imapper.Map<KategoryDTO, KategoryTbl>(cat);
            return _Cdal.UppdateKategory(currentKategory, id);
        }

        public List<KategoryTbl> RemoveKategory(int id)
        {
            return _Cdal.RemoveKategory(id);
        }
       
    }
}
