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
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public User user { get; set; }
        public UsersController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult UserView()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            user = new User();

            if (id == null)
            {
                return View(user);
            }
            user = _db.UsersApp.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {
                if (user.Id==0)
                {
                    _db.UsersApp.Add(user);
                }
                else
                {
                    _db.UsersApp.Update(user);
                }
                _db.SaveChanges();
                return RedirectToAction("UserView");
            }
            return View(user);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.UsersApp.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var userFromDb = await _db.UsersApp.FirstOrDefaultAsync(u => u.Id == id);
            if (userFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            _db.UsersApp.Remove(userFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful" });
        }
    }
}