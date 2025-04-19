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

        // GET: TrInvoice
        public async Task<IActionResult> Index()
        {
            var invoices = await _context.TrInvoices
                .Include(i => i.Sales)
                .Include(i => i.Courier)
                .Include(i => i.Payment)
                .Include(i => i.InvoiceDetails)
                .ThenInclude(d => d.Product)
                .ToListAsync();

            return View(invoices);
        }

        // GET: TrInvoice/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
                return NotFound();

            var invoice = await _context.TrInvoices
                .Include(i => i.Sales)
                .Include(i => i.Courier)
                .Include(i => i.Payment)
                .Include(i => i.InvoiceDetails)
                .ThenInclude(d => d.Product)
                .FirstOrDefaultAsync(m => m.InvoiceNo == id);

            if (invoice == null)
                return NotFound();

            return View(invoice);
        }

        // GET: TrInvoice/Create
        public IActionResult Create()
        {
            PopulateDropdowns();
            return View();
        }

        // POST: TrInvoice/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TrInvoice invoice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            PopulateDropdowns();
            return View(invoice);
        }

        // GET: TrInvoice/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
                return NotFound();

            var invoice = await _context.TrInvoices
                .Include(i => i.InvoiceDetails)
                .FirstOrDefaultAsync(i => i.InvoiceNo == id);

            if (invoice == null)
                return NotFound();

            PopulateDropdowns();
            return View(invoice);
        }

        // POST: TrInvoice/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, TrInvoice invoice)
        {
            if (id != invoice.InvoiceNo)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    // Remove existing details first
                    var existingDetails = _context.TrInvoiceDetails.Where(d => d.InvoiceNo == id);
                    _context.TrInvoiceDetails.RemoveRange(existingDetails);

                    // Update main invoice
                    _context.Update(invoice);

                    // Re-add new details
                    if (invoice.InvoiceDetails != null)
                    {
                        foreach (var detail in invoice.InvoiceDetails)
                        {
                            _context.TrInvoiceDetails.Add(detail);
                        }
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrInvoiceExists(invoice.InvoiceNo))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            PopulateDropdowns();
            return View(invoice);
        }

        // GET: TrInvoice/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
                return NotFound();

            var invoice = await _context.TrInvoices
                .Include(i => i.Sales)
                .Include(i => i.Courier)
                .Include(i => i.Payment)
                .FirstOrDefaultAsync(m => m.InvoiceNo == id);

            if (invoice == null)
                return NotFound();

            return View(invoice);
        }

        // POST: TrInvoice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var invoice = await _context.TrInvoices
                .Include(i => i.InvoiceDetails)
                .FirstOrDefaultAsync(i => i.InvoiceNo == id);

            if (invoice != null)
            {
                _context.TrInvoiceDetails.RemoveRange(invoice.InvoiceDetails);
                _context.TrInvoices.Remove(invoice);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool TrInvoiceExists(string id)
        {
            return _context.TrInvoices.Any(e => e.InvoiceNo == id);
        }

        private void PopulateDropdowns()
        {
            ViewData["CourierID"] = new SelectList(_context.MsCouriers, "CourierID", "CourierName");
            ViewData["SalesID"] = new SelectList(_context.MsSales, "SalesID", "SalesName");
            ViewData["PaymentType"] = new SelectList(_context.MsPayments, "PaymentID", "PaymentName");
            ViewData["Products"] = new SelectList(_context.MsProducts, "ProductID", "ProductName");
        }
    }
}
