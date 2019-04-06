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
    public class ProductsController : Controller
    {
        private readonly ModelNorthwind _context;
        HttpClient cliente;

        public ProductsController(ModelNorthwind context)
        {
            _context = null;
            cliente = new HttpClient();
            cliente.BaseAddress = new Uri("https://localhost:44374/api/");
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var response = await cliente.GetAsync("Products");
            if ( response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var products = await response.Content.ReadAsAsync<IEnumerable<Products>>();
                if (products != null) return View(products);
            }
            return NotFound();
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                var response = await cliente.GetAsync("Products/" + id);
                if(response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var product = await response.Content.ReadAsAsync<Products>();
                    if (product != null) return View(product);
                }
            }

            return NotFound();
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CategoryID"] = new SelectList( await (await cliente.GetAsync("Categories")).Content.ReadAsAsync<IEnumerable<Categories>>(), "CategoryID", "CategoryName");
            ViewData["SupplierID"] = new SelectList( await (await cliente.GetAsync("Suppliers")).Content.ReadAsAsync<IEnumerable<Suppliers>>(), "SupplierID", "CompanyName");
            
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductID,ProductName,SupplierID,CategoryID,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued")] Products products)
        {
            if (ModelState.IsValid)
            {
                var response = await cliente.PostAsync<Products>("Products", products, new JsonMediaTypeFormatter());
                if (response.StatusCode == System.Net.HttpStatusCode.Created) return RedirectToAction("Index");
            }
            ViewData["CategoryID"] = new SelectList(await (await cliente.GetAsync("Categories")).Content.ReadAsAsync<IEnumerable<Categories>>(), "CategoryID", "CategoryName");
            ViewData["SupplierID"] = new SelectList(await (await cliente.GetAsync("Suppliers")).Content.ReadAsAsync<IEnumerable<Suppliers>>(), "SupplierID", "CompanyName");
            return View(products);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                var response = await cliente.GetAsync("Products/"+id);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var product = await response.Content.ReadAsAsync<Products>();
                    if (product != null)
                    {
                        ViewData["CategoryID"] = new SelectList(await (await cliente.GetAsync("Categories")).Content.ReadAsAsync<IEnumerable<Categories>>(), "CategoryID", "CategoryName");
                        ViewData["SupplierID"] = new SelectList(await (await cliente.GetAsync("Suppliers")).Content.ReadAsAsync<IEnumerable<Suppliers>>(), "SupplierID", "CompanyName");

                        return View(product);
                    }
                }
                return NotFound();
            }

            return NotFound();
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductID,ProductName,SupplierID,CategoryID,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued")] Products products)
        {
            if (id == products.ProductID)
            {
                if (ModelState.IsValid)
                {
                    var response = await cliente.PutAsync<Products>("Products/" + id, products, new JsonMediaTypeFormatter());
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return NotFound();
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                var response = await cliente.GetAsync("Products/" + id);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var product = await response.Content.ReadAsAsync<Products>();
                    if (product != null)
                    {
                        return View(product);
                    }
                }
            }

            return NotFound();
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await cliente.DeleteAsync("Products/" + id);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var product = await response.Content.ReadAsAsync<Products>();
                return View(product);
            }

            return NotFound();
        }

        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.ProductID == id);
        }
    }
}
