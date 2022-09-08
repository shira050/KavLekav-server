using DAL.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
  public class UserDAL
    {
        DBContext DB = new DBContext();

        public UserTbl GetUser(string name, string password)
        {
            UserTbl currentUserTbl = DB.UserTbls.Where(x => x.UserName.Equals(name) && x.UserPassword.Equals(password)).FirstOrDefault();
            return currentUserTbl;
        }

        public List<UserTbl> GetAllUsers()
        {
            List<UserTbl> Users = DB.UserTbls.ToList();
            return Users;
        }
        public List<UserTbl> AddUser(UserTbl User)
        {
            try
            {
                DB.UserTbls.Add(User);
                DB.SaveChanges();
                return DB.UserTbls.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<UserTbl> UppdateUser(UserTbl UserTbl, int id)
        {
            try
            {
                UserTbl u = DB.UserTbls.FirstOrDefault(x => x.CodeUser == id);
                if (u != null)
                {
      
        u.UserName = UserTbl.UserName;
                    u.UserLastName = UserTbl.UserLastName;
                    u.UserMail = UserTbl.UserMail;
                    u.UserPassword = UserTbl.UserPassword;
                    u.UserClueForPass = UserTbl.UserClueForPass;

                    DB.SaveChanges();
                }
                return DB.UserTbls.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<UserTbl> RemoveUser(int id)
        {
            try
            {
                this.DB.Remove(DB.UserTbls.FirstOrDefault(x => x.CodeUser == id));
                this.DB.SaveChanges();
                return DB.UserTbls.ToList();
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}
