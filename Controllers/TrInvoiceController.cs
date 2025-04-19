using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InvoiceApp.Data;
using InvoiceApp.Models;

namespace InvoiceApp.Controllers
{
    public class TrInvoiceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrInvoiceController(ApplicationDbContext context)
        {
            _context = context;
        }

    [HttpPost]
public IActionResult ViewInvoice(string invoiceNo)
{
    if (string.IsNullOrWhiteSpace(invoiceNo))
    {
        return View("Index"); // Or redirect to Index
    }

    var invoice = _context.TrInvoices
        .Include(i => i.Sales)
        .Include(i => i.Courier)
        .Include(i => i.Payment)
        .Include(i => i.InvoiceDetails)
        .FirstOrDefault(i => i.InvoiceNo == invoiceNo);

    if (invoice == null)
    {
        ViewBag.Message = "Invoice not found";
        return View("Index");
    }

    return View("Index", invoice); // assuming Index.cshtml accepts the invoice model
}



        [HttpGet]
    public async Task<IActionResult> Index(string invoiceNo)
    {
        TrInvoice invoice = null;

        if (!string.IsNullOrEmpty(invoiceNo))
        {
            invoice = await _context.TrInvoices
                .Include(i => i.Sales)
                .Include(i => i.Courier)
                .Include(i => i.Payment)
                .Include(i => i.InvoiceDetails)
                    .ThenInclude(d => d.Product)
                .FirstOrDefaultAsync(i => i.InvoiceNo == invoiceNo);
        }

        return View(invoice); // Pass the invoice or null to the view
    }

    // === API ===
    [HttpGet("api/invoice/{invoiceNo}")]
    public async Task<IActionResult> GetInvoice(string invoiceNo)
    {
        var invoice = await _context.TrInvoices
            .Include(i => i.Sales)
            .Include(i => i.Courier)
            .Include(i => i.Payment)
            .Include(i => i.InvoiceDetails)
                .ThenInclude(d => d.Product)
            .FirstOrDefaultAsync(i => i.InvoiceNo == invoiceNo);

        if (invoice == null)
            return NotFound(new { message = "Invoice not found" });

        return Ok(invoice);
    }
    }
}
