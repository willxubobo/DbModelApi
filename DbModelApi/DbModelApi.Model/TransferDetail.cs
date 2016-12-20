using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbModelApi.Model
{
    public class TransferDetail
    {
        public System.Guid Id { get; set; }
        public Nullable<System.Guid> ApplyId { get; set; }
        public string InvoiceNo { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string Currency { get; set; }
        public string Summary { get; set; }
        public string ReceiveAccountName { get; set; }
        public string ReceiveAccount { get; set; }
        public string ReceiveOpeningBank { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public string Attribute1 { get; set; }
        public string Attribute2 { get; set; }
        public string Attribute3 { get; set; }
        public virtual TransferManagement TransferManagement { get; set; }
    }
}
