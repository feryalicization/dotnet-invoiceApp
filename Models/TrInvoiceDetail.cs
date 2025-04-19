using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceApp.Models
{
    [Table("trinvoicedetail")]
    public class TrInvoiceDetail
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Invoice")]
        [MaxLength(10)]
        public string InvoiceNo { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Product")]
        public int ProductID { get; set; }

        [Required]
        public float Weight { get; set; }

        [Required]
        public short Qty { get; set; }

        [Required]
        public decimal Price { get; set; }

        public TrInvoice Invoice { get; set; }
        public MsProduct Product { get; set; }
    }
}
