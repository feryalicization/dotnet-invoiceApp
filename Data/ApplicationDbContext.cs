using Microsoft.EntityFrameworkCore;
using InvoiceApp.Models;

namespace InvoiceApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<MsCourier> MsCouriers { get; set; }
        public DbSet<MsPayment> MsPayments { get; set; }
        public DbSet<MsProduct> MsProducts { get; set; }
        public DbSet<MsSales> MsSales { get; set; }
        public DbSet<TrInvoice> TrInvoices { get; set; }
        public DbSet<TrInvoiceDetail> TrInvoiceDetails { get; set; }
        public DbSet<LtCourierFee> LtCourierFees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TrInvoiceDetail>()
                .HasKey(d => new { d.InvoiceNo, d.ProductID });

            modelBuilder.Entity<TrInvoiceDetail>()
                .HasOne(d => d.Invoice)
                .WithMany(i => i.InvoiceDetails)
                .HasForeignKey(d => d.InvoiceNo);

            modelBuilder.Entity<TrInvoiceDetail>()
                .HasOne(d => d.Product)
                .WithMany(p => p.InvoiceDetails)
                .HasForeignKey(d => d.ProductID);

            modelBuilder.Entity<TrInvoice>()
                .HasOne(i => i.Sales)
                .WithMany(s => s.Invoices)
                .HasForeignKey(i => i.SalesID);

            modelBuilder.Entity<TrInvoice>()
                .HasOne(i => i.Courier)
                .WithMany(c => c.Invoices)
                .HasForeignKey(i => i.CourierID);

            modelBuilder.Entity<TrInvoice>()
                .HasOne(i => i.Payment)
                .WithMany(p => p.Invoices)
                .HasForeignKey(i => i.PaymentType);

            modelBuilder.Entity<LtCourierFee>()
                .HasKey(f => new { f.WeightID, f.CourierID });

            modelBuilder.Entity<LtCourierFee>()
                .HasOne(f => f.Courier)
                .WithMany(c => c.CourierFees)
                .HasForeignKey(f => f.CourierID);
        }
    }
}
