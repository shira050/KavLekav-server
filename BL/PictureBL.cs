using AutoMapper;
using DAL;
using DAL.models;
using DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace BL
{
public    class PictureBL
    {
        PictureDAL _Pdal = new PictureDAL();
        IMapper imapper;
        public PictureBL()
        {
            var config = new MapperConfiguration(cgf => { cgf.AddProfile<AutoProfilMapper>(); });
            imapper = config.CreateMapper();
        }

        public PictureDTO GetPicture(int code)
        {
            PictureTbl p = _Pdal.GetPictureByCode(code);
            PictureDTO currentPicture = imapper.Map<PictureTbl, PictureDTO>(p);
            return currentPicture;
        }


        public List<PictureDTO> GetAllPictures()
        {
            List<PictureTbl> pics = _Pdal.GetAllPicture();
            List<PictureDTO> PictureDTO = new List<PictureDTO>();
              
            foreach (var item in pics)
            {
                var p = imapper.Map<PictureTbl, PictureDTO>(item);
                PictureDTO.Add(p);
            }
            return PictureDTO;
        }

        public List<PictureTbl> AddPicture(PictureDTO pic)
        {
            pic.UppDateToWeb=DateTime.Now;
            PictureTbl currentPicture = imapper.Map<PictureDTO, PictureTbl>(pic);

            return _Pdal.AddPicture(currentPicture);
        }

        public List<PictureTbl> UppdatePicture(PictureDTO pic, int code)
        {
            PictureTbl currentPicture = imapper.Map<PictureDTO, PictureTbl>(pic);
            return _Pdal.UppdatePicture(currentPicture, code);
        }

        public List<PictureTbl> RemovePicture(int code)
        {
            return _Pdal.RemovePicture(code);
        }
        public List<PictureTbl> GetAllPicturesInCategory(int codeCategory)
        {
            return _Pdal.GetAllPicturesInCategory(codeCategory);
        }

        public object GetAllCountings()
        {
            return _Pdal.GetAllCountings();
        }

        public List<PictureTbl> GetAllPicturesOfOneUser(int codeUser)
        {
            return _Pdal.GetAllPicturesOfOneUser(codeUser);
        }

        public void cascading(PictureDTO p,int id)
        {
             p = GetPicture(p.CodePicture);
            PictureTbl pic = imapper.Map<PictureDTO, PictureTbl>(p);
            //return _Pdal.cascading(pic,id);
            _Pdal.cascading(pic, id);
        }
    }
}
