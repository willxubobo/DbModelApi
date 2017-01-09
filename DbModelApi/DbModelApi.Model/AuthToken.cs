using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbModelApi.Model
{
    public class AuthToken
    {
        public string Access_Token { get; set; }
        public string User_Name { get; set; }
        public string User_Pwd { get; set; }
        public string User_NickName { get; set; }
        public Guid User_ID { get; set; }
    }
}
