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
                return View("Index"); 
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

            return View("Index", invoice); 
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

            return View(invoice); 
        }

        [HttpPost]
        public IActionResult UpdateInvoiceDetails(TrInvoice model)
        {
            if (model.InvoiceDetails != null && model.InvoiceDetails.Any())
            {
                foreach (var detail in model.InvoiceDetails)
                {
                    var existing = _context.TrInvoiceDetails.FirstOrDefault(x => x.InvoiceNo == detail.InvoiceNo);
                    if (existing != null)
                    {
                        existing.Qty = detail.Qty;
                        existing.Weight = detail.Weight;
                        existing.Price = detail.Price;
                    }
                }

                _context.SaveChanges();
            }

            return Redirect("/?invoiceNo=" + model.InvoiceNo);
        }


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

        [HttpGet]
        public IActionResult Create()
        {
            var model = new TrInvoice
            {
                InvoiceDate = DateTime.Today,
                InvoiceDetails = new List<TrInvoiceDetail> { new TrInvoiceDetail() }
            };

            return View(model); // This renders Views/TrInvoice/Create.cshtml
        }



        [HttpPost]
        public IActionResult Create(TrInvoice model)
        {
            if (ModelState.IsValid)
            {
                _context.TrInvoices.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Details", new { id = model.InvoiceNo }); // Redirect to the details view after creating
            }

            // If there are validation errors, return to the Create view with the model so the user can correct them
            return View(model);
        }


    
    }
}
