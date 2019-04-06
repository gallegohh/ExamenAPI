using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Examen.DB;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace Examen.Cliente.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ModelNorthwind _context;
        public HttpClient cliente;

        public CustomersController(ModelNorthwind context)
        {
            _context = null;
            cliente = new HttpClient();
            cliente.BaseAddress = new Uri("https://localhost:44374/api/");
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            var response = await cliente.GetAsync("Customers");
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var customers = await response.Content.ReadAsAsync<IEnumerable<Customers>>();
                return View(customers);
            }
            else
            {
                return View();
            }
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await cliente.GetAsync("Customers/" + id);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var customer = await response.Content.ReadAsAsync<Customers>();
                if (customer != null)
                {
                    return View(customer);
                }
                else
                {
                    return NotFound();
                }
            }
            else return NotFound();
        }

        // GET: Customers/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CategoryId"] = new SelectList(await(await cliente.GetAsync("Categories")).Content.ReadAsAsync<IEnumerable<Categories>>());
            ViewData["SupplierId"] = new SelectList(await(await cliente.GetAsync("Suppliers")).Content.ReadAsAsync<IEnumerable<Suppliers>>());

            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerID,CompanyName,ContactName,ContactTitle,Address,City,Region,PostalCode,Country,Phone,Fax")] Customers customers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customers);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await cliente.GetAsync("Customers/" + id);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var customer = await response.Content.ReadAsAsync<Customers>();
                if (customer != null) return View(customer);
                else return NotFound();
            }
            else return NotFound();
            
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CustomerID,CompanyName,ContactName,ContactTitle,Address,City,Region,PostalCode,Country,Phone,Fax")] Customers customers)
        {
            if (id != customers.CustomerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var response = await cliente.PutAsync("Customers/" + id, customers, new JsonMediaTypeFormatter());
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent) return RedirectToAction("index");
                {
                    return View("Customers");
                }
            }
 
            ViewData["CategoryId"] = new SelectList(await (await cliente.GetAsync("Categories")).Content.ReadAsAsync<IEnumerable<Categories>>());
            ViewData["SupplierId"] = new SelectList(await (await cliente.GetAsync("Suppliers")).Content.ReadAsAsync<IEnumerable<Suppliers>>());

            return View(customers);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customers = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerID == id);
            if (customers == null)
            {
                return NotFound();
            }

            return View(customers);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var customers = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomersExists(string id)
        {
            return _context.Customers.Any(e => e.CustomerID == id);
        }
    }
}
