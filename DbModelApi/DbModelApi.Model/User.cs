using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbModelApi.Model
{
    public class User
    {
       // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public System.Guid UserId { get; set; }

        [StringLength(100)]
        public string UserName { get; set; }

        [StringLength(200)]
        public string Pwd { get; set; }
        [StringLength(200)]
        public string NickName { get; set; }

        public User ConvertUser(AuthToken authToken)
        {
            if (authToken == null)
            {
                return null;
            }

            User user = new User();
            user.UserName = authToken.User_Name;
            user.NickName = authToken.User_NickName;
            user.Pwd = authToken.User_Pwd;
            user.UserId = authToken.User_ID;
            return user;
        }
    }
}
