using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bartender.Data;
using bartender.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using bartender.Data.Migrations;

namespace bartender.Views.CocktailMenu
{
    public class orderDrinksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public orderDrinksController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: orderDrinks
        public async Task<IActionResult> Index()
        {
            
            return View(await _context.orderDrink.ToListAsync());
        }

        // GET: orderDrinks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDrink = await _context.orderDrink.SingleOrDefaultAsync(m => m.ID == id);
            if (orderDrink == null)
            {
                return NotFound();
            }

            return View(orderDrink);
        }

        // GET: orderDrinks/Create
        public IActionResult Create()
        {
            //TO DO: change to create a new order
            var availableDrinks = _context.drinkList.ToList();
            ViewBag.AvailableDrinks = availableDrinks;
            return View();
        }

        // POST: orderDrinks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("CustName,Drink,Qty")] orderDrinks orderDrinks)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderDrinks);
                _context.SaveChanges();
                
                return RedirectToAction("Create");
            }

            //ViewBag.Category = new SelectList(_context)
            return View(orderDrinks);
        }

        // GET: orderDrinks/FillOrder/5
        public ActionResult FillOrder(int? id)
        {
            if (id == null)
            {
                return NotFound();
                //return Content("drink id not found");
            }

            var orderDrink = _context.orderDrink.SingleOrDefault(m => m.ID == id);
            orderDrink.Filled = true;


            if (orderDrink == null)
            {
                return NotFound();
            }
            return View(orderDrink);
            
        }

        // POST: orderDrinks/FillOrder/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FillOrder(int id)
        {
            var orderDrink = _context.orderDrink.SingleOrDefault(m => m.ID == id);

            if (ModelState.IsValid)
            {
                try
                {
                    orderDrink.Filled = true;
                    _context.orderDrink.Update(orderDrink);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!orderDrinkExists(orderDrink.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }


                return RedirectToAction("Index");
            }
            return View(orderDrink);
        }


        // GET: orderDrinks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDrinks = await _context.orderDrink.SingleOrDefaultAsync(m => m.ID == id);
            if (orderDrinks == null)
            {
                return NotFound();
            }
            return View(orderDrinks);
        }

        // POST: orderDrinks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CustName,Drink,Qty,Filled")] orderDrinks orderDrinks)
        {
            if (id != orderDrinks.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderDrinks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!orderDrinkExists(orderDrinks.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(orderDrinks);
        }

        // GET: orderDrinks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDrink = await _context.orderDrink.SingleOrDefaultAsync(m => m.ID == id);
            if (orderDrink == null)
            {
                return NotFound();
            }

            return View(orderDrink);
        }

        // POST: orderDrinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderDrink = await _context.orderDrink.SingleOrDefaultAsync(m => m.ID == id);
            _context.orderDrink.Remove(orderDrink);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool orderDrinkExists(int id)
        {
            return _context.orderDrink.Any(e => e.ID == id);
        }

        
    }
}
