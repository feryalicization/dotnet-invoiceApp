using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceApp.Models
{
    [Table("ltcourierfee")]
    public class LtCourierFee
    {
        [Key, Column(Order = 0)]
        public int WeightID { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Courier")]
        public int CourierID { get; set; }

        [Required]
        public int StartKg { get; set; }

        public int? EndKg { get; set; }

        public decimal? Price { get; set; }

        public MsCourier Courier { get; set; }
    }
}
