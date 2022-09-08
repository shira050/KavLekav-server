using DAL.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DTO
{
  public  class AutoProfilMapper : AutoMapper.Profile
    {
        public AutoProfilMapper()
        {
            CreateMap<KategoryTbl, KategoryDTO>();
            CreateMap<KategoryDTO, KategoryTbl>();
            CreateMap<PictureTbl, PictureDTO>();
            CreateMap<PictureDTO, PictureTbl>();
            CreateMap<UserTbl, UserDTO>();
            CreateMap<UserDTO, UserTbl>();

        }
    }
}
