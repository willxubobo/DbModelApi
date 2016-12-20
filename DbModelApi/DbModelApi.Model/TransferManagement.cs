using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbModelApi.Model
{
    public class TransferManagement
    {
        public TransferManagement()
        {
            this.TransferDetails = new List<TransferDetail>();
        }

        public System.Guid ApplyId { get; set; }
        public Nullable<System.Guid> FactoringId { get; set; }
        public string Topic { get; set; }
        public string CompanyName { get; set; }
        public string DeptCode { get; set; }
        public string JobCode { get; set; }
        public string TransferType { get; set; }
        public string ProcessStatus { get; set; }
        public string ProcessId { get; set; }
        public Nullable<System.DateTime> PassTime { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public Nullable<System.Guid> ModifiedBy { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public string Attribute1 { get; set; }
        public string Attribute2 { get; set; }
        public string Attribute3 { get; set; }
        public string Attribute4 { get; set; }
        public string Attribute5 { get; set; }
        public virtual ICollection<TransferDetail> TransferDetails { get; set; }
    }
}
