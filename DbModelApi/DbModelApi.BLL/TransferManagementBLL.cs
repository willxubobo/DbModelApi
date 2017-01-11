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
                var templist = db.TransferManagements.AsQueryable();
                list = templist.OrderByDescending(d=>d.Created).ThenBy(d=>d.ApplyId).Skip(skip).Take(take).ToList();
                count = templist.Count();
            }
            return list;
        }

        public void UpdateMain(TransferManagement bc)
        {
            using (DBContext db = new DBContext())
            {
                db.Entry(bc).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void Update(TransferManagement tm)
        {
            using (DBContext db = new DBContext())
            {
                foreach (var detail in db.TransferDetails.Where(p => p.ApplyId == tm.ApplyId))
                {
                    db.Entry(detail).State = EntityState.Deleted;
                }
                foreach (var detail in tm.TransferDetails)
                {
                    db.Entry(detail).State = EntityState.Added;
                }
                db.Entry(tm).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public List<TransferManagement> GetTransfers(string gfID, string TransferType, string UserName, string ProcessStatus, string sdate, string edate, int skip, int take, out int count)
        {
            DateTime dtStart = Convert.ToDateTime(sdate);
            DateTime dtEnd = Convert.ToDateTime(edate).AddDays(1);
            List<TransferManagement> coms = null;
            using (DBContext db = new DBContext())
            {
                string sql = " select a.*,b.UserName,SubTotal=(select sum(Amount) from TransferDetails where applyid=a.ApplyId),Currency=(select top 1 Currency from TransferDetails where applyid=a.ApplyId) from  TransferManagement a left join Sys_User b on a.CreatedBy=b.UserId where 1=1";
                if (!string.IsNullOrEmpty(TransferType))
                {
                    sql += " and a.TransferType='" + TransferType + "'";
                }
                if (!string.IsNullOrEmpty(gfID))
                {
                    sql += " and a.FactoringId='" + gfID + "'";
                }
                if (!string.IsNullOrEmpty(UserName))
                {
                    sql += " and b.UserName like N'%" + UserName + "%'";
                }
                if (!string.IsNullOrEmpty(ProcessStatus))
                {
                    sql += " and a.ProcessStatus='" + ProcessStatus + "'";
                }
                if (!string.IsNullOrEmpty(sdate))
                {
                    sql += " and a.Created >='" + sdate + "'";
                }
                if (!string.IsNullOrEmpty(edate))
                {
                    sql += " and a.Created <'" + edate + "'";
                }
                sql += " order by a.Attribute1 desc";
                IEnumerable<TransferManagement> tempcoms = db.Database.SqlQuery<TransferManagement>(sql).ToList();
                coms = tempcoms.Skip(skip).Take(take).ToList();
                count = tempcoms.Count();
            }
            return coms;
        }

        public void Create(TransferManagement transfer)
        {
            using (DBContext db = new DBContext())
            {
                db.TransferManagements.Add(transfer);
                db.SaveChanges();
            }
        }
    }
}
