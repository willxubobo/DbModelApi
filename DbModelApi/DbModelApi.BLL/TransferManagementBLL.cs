using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbModelApi.Model;

namespace DbModelApi.BLL
{
    public class TransferManagementBLL : BaseBLL
    {
        public TransferManagement GetById(Guid id)
        {
            TransferManagement transferManagement = new TransferManagement();
            using (DBContext db = new DBContext())
            {
                transferManagement = db.TransferManagements.Include(d => d.TransferDetails).FirstOrDefault(p => p.ApplyId == id);
                foreach (var m in transferManagement.TransferDetails)
                {
                    m.TransferManagement = null;
                }
            }
            return transferManagement;
        }

        public List<TransferManagement> GetAll(int skip, int take, out int count)
        {
            List<TransferManagement> list = new List<TransferManagement>();
            using (DBContext db = new DBContext())
            {
                List<TransferManagement> templist = db.TransferManagements.ToList();
                list = templist.Skip(skip).Take(take).ToList();
                count = templist.Count();
            }
            return list;
        } 
    }
}
