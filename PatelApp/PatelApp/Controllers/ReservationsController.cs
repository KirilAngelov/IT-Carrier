using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatelApp.Data;
using PatelApp.Models;

namespace PatelApp.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Reservation res { get; set; }
        public ReservationsController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult ReservationView()
        {
            return View();
        }


        public IActionResult Upsert(int? id)
        {
            res = new Reservation();

            if (id == null)
            {
                return View(res);
            }
            res = _db.Reservations.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return NotFound();
            }
            return View(res);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {
                if (res.Id == 0)
                {
                    _db.Reservations.Add(res);
                }
                else
                {
                    _db.Reservations.Update(res);
                }
                _db.SaveChanges();
                return RedirectToAction("ReservationView");
            }
            return View(res);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Reservations.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var bookFromDb = await _db.Reservations.FirstOrDefaultAsync(u => u.Id == id);
            if (bookFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            _db.Reservations.Remove(bookFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful" });
        }
    }
}