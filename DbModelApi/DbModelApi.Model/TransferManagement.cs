using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbModelApi.Model
{
    [Table("TransferManagement")]
    public class TransferManagement
    {
        public TransferManagement()
        {
            this.TransferDetails = new List<TransferDetail>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public System.Guid ApplyId { get; set; }
        public Nullable<System.Guid> FactoringId { get; set; }
        [StringLength(300)]
        public string Topic { get; set; }
        [StringLength(300)]
        public string CompanyName { get; set; }
        [StringLength(300)]
        public string DeptCode { get; set; }
        [StringLength(300)]
        public string JobCode { get; set; }
        [StringLength(300)]
        public string TransferType { get; set; }
        [StringLength(300)]
        public string ProcessStatus { get; set; }
        [StringLength(300)]
        public string ProcessId { get; set; }
        public Nullable<System.DateTime> PassTime { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public Nullable<System.Guid> ModifiedBy { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        [StringLength(300)]
        public string Attribute1 { get; set; }
        [StringLength(300)]
        public string Attribute2 { get; set; }
        [StringLength(300)]
        public string Attribute3 { get; set; }
        [StringLength(300)]
        public string Attribute4 { get; set; }
        [StringLength(300)]
        public string Attribute5 { get; set; }
        public virtual ICollection<TransferDetail> TransferDetails { get; set; }
    }
}
