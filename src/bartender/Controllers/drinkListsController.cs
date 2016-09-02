using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using bartender.Data;
using bartender.Models;

namespace bartender.Controllers
{
    public class drinkListsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public drinkListsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: drinkLists
        public async Task<IActionResult> Index(string searchCat, string searchString)
        {
            //use LINQ to create a list of drink categories
            IQueryable<string> drinkCategory = from d in _context.drinkList
                                               orderby d.Category
                                               select d.Category;

            //extract list of available drinks from database
            var drink = from d in _context.drinkList
                        select d;

            //match search string if drink search field populated
            if(!string.IsNullOrEmpty(searchString))
            {
                drink = drink.Where(d => d.Drink.Contains(searchString));
            }

            //filter category if selected
            if(!string.IsNullOrEmpty(searchCat))
            {
                drink = drink.Where(d => d.Category == searchCat);
            }

            var drinkCategoryVM = new drinkCategoryViewModel();
            drinkCategoryVM.categories = new SelectList(await drinkCategory.Distinct().ToListAsync());
            drinkCategoryVM.drinks = await drink.ToListAsync();

            return View(drinkCategoryVM);
        }

        // GET: drinkLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drinkList = await _context.drinkList.SingleOrDefaultAsync(m => m.ID == id);
            if (drinkList == null)
            {
                return NotFound();
            }

            return View(drinkList);
        }

        // GET: drinkLists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: drinkLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Category,Drink")] drinkList drinkList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(drinkList);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(drinkList);
        }

        // GET: drinkLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drinkList = await _context.drinkList.SingleOrDefaultAsync(m => m.ID == id);
            if (drinkList == null)
            {
                return NotFound();
            }
            return View(drinkList);
        }

        // POST: drinkLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Category,Drink")] drinkList drinkList)
        {
            if (id != drinkList.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(drinkList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!drinkListExists(drinkList.ID))
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
            return View(drinkList);
        }

        // GET: drinkLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drinkList = await _context.drinkList.SingleOrDefaultAsync(m => m.ID == id);
            if (drinkList == null)
            {
                return NotFound();
            }

            return View(drinkList);
        }

        // POST: drinkLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var drinkList = await _context.drinkList.SingleOrDefaultAsync(m => m.ID == id);
            _context.drinkList.Remove(drinkList);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool drinkListExists(int id)
        {
            return _context.drinkList.Any(e => e.ID == id);
        }
    }
}
