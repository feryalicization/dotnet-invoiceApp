using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceApp.Models
{
    [Table("msproduct")]
    public class MsProduct
    {
        [Key]
        public int ProductID { get; set; }

        [Required]
        [MaxLength(50)]
        public string ProductName { get; set; }

        [Required]
        public float Weight { get; set; }

        [Required]
        public decimal Price { get; set; }

        public ICollection<TrInvoiceDetail> InvoiceDetails { get; set; }
    }
}
