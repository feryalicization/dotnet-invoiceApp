using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceApp.Models
{
    [Table("mssales")]
    public class MsSales
    {
        [Key]
        public int SalesID { get; set; }

        [Required]
        [MaxLength(50)]
        public string SalesName { get; set; }

        public ICollection<TrInvoice> Invoices { get; set; }
    }
}
