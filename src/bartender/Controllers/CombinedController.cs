using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using bartender.Models;
using System;

namespace bartender.Controllers
{
    public class CombinedController : Controller
    {
        // GET: Combined
        public ActionResult Index()
        {
            return View();
        }


        // GET: Combined/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Combined/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Combined/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Combined/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Combined/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Combined/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Combined/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}