using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceApp.Models
{
    [Table("trinvoice")]
    public class TrInvoice
    {
        [Key]
        [MaxLength(10)]
        public string InvoiceNo { get; set; }

        [Required]
        public DateTime InvoiceDate { get; set; }

        [Required]
        public string InvoiceTo { get; set; }

        [Required]
        public string ShipTo { get; set; }

        [ForeignKey("Sales")]
        public int SalesID { get; set; }

        [ForeignKey("Courier")]
        public int CourierID { get; set; }

        [ForeignKey("Payment")]
        public int PaymentType { get; set; }

        [Required]
        public decimal CourierFee { get; set; }

        public MsSales Sales { get; set; }
        public MsCourier Courier { get; set; }
        public MsPayment Payment { get; set; }

        public ICollection<TrInvoiceDetail> InvoiceDetails { get; set; }
    }
}
