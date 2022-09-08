using AutoMapper;
using DAL;
using DAL.models;
using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL
{
  public  class UserBL
    {
        UserDAL _Pdal = new UserDAL();
        IMapper imapper;
        public UserBL()
        {
            var config = new MapperConfiguration(cgf => { cgf.AddProfile<AutoProfilMapper>(); });
            imapper = config.CreateMapper();
        }

        public List<UserDTO> GetAllUsers()
        {
            List<UserTbl> users = _Pdal.GetAllUsers();
            List<UserDTO> UsersDTO = new List<UserDTO>();
            foreach (var item in users)
            {
                var u = imapper.Map<UserTbl, UserDTO>(item);
                UsersDTO.Add(u);
            }
            return UsersDTO;
        }

        public UserDTO GetUser(string name, string password)
        {
            UserTbl user = _Pdal.GetUser(name, password);
            if (user == null)
                return null;
            UserDTO currentUser = imapper.Map<UserTbl, UserDTO>(user);
            return currentUser;
        }


       
        public List<UserTbl> AddUser(UserDTO u)
        {
            UserTbl currentUser = imapper.Map<UserDTO, UserTbl>(u);
            return _Pdal.AddUser(currentUser);
        }

        public List<UserTbl> UppdateUser(UserDTO u, int code)
        {
            UserTbl currentUser = imapper.Map<UserDTO, UserTbl>(u);
            return _Pdal.UppdateUser(currentUser, code);
        }

        public List<UserTbl> RemoveUser(int code)
        {
            return _Pdal.RemoveUser(code);
        }

    }
}
