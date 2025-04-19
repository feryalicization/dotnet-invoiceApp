using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceApp.Models
{
    [Table("mscourier")]
    public class MsCourier
    {
        [Key]
        public int CourierID { get; set; }

        [Required]
        [MaxLength(50)]
        public string CourierName { get; set; }

        public ICollection<TrInvoice> Invoices { get; set; }
        public ICollection<LtCourierFee> CourierFees { get; set; }
    }
}
