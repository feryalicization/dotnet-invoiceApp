using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceApp.Models
{
    [Table("mspayment")]
    public class MsPayment
    {
        [Key]
        public int PaymentID { get; set; }

        [Required]
        [MaxLength(50)]
        public string PaymentName { get; set; }

        public ICollection<TrInvoice> Invoices { get; set; }
    }
}
