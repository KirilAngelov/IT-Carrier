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
    public class ClientsController : Controller
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Client client { get; set; }
        public ClientsController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult ClientView()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Clients.ToListAsync() });
        }
        public IActionResult Upsert(int? id)
        {
            client = new Client();

            if (id == null)
            {
                return View(client);
            }
            client = _db.Clients.FirstOrDefault(x => x.Id == id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {
                if (client.Id == 0)
                {
                    _db.Clients.Add(client);
                }
                else
                {
                    _db.Clients.Update(client);
                }
                _db.SaveChanges();
                return RedirectToAction("ClientView");
            }
            return View(client);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var clientFromDb = await _db.Clients.FirstOrDefaultAsync(u => u.Id == id);
            if (clientFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            _db.Clients.Remove(clientFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful" });
        }
    }
}