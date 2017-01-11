using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbModelApi.Model;

namespace DbModelApi.BLL
{
    public class UserBLL:BaseBLL
    {
        public User GetUserByUNameAndPassword(string uname,string pwd)
        {
            User user = new User();
            using (DBContext db = new DBContext())
            {
                var users = db.Users.AsQueryable();
                user = db.Users.FirstOrDefault(p => p.UserName==uname&&p.Pwd==pwd);
                
            }
            return user;
        }
    }
}
