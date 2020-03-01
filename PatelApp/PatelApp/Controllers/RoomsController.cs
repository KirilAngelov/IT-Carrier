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
    public class RoomsController : Controller
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Room room { get; set; }
        public RoomsController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult RoomsView()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            room = new Room();
           
            if (id==null)
            {
                return View(room);
            }
            room = _db.Rooms.FirstOrDefault(x=>x.Id == id);
            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {
               
                if (room.Id == 0)
                {
                    if (room.Type=="1")
                    {
                        room.Type = Room.RoomStatus.TwoBeds.ToString();
                        room.AdultPrice = 20.0;
                        room.ChildPrice = 10.0;
                    }
                    if (room.Type=="2")
                    {
                        room.Type = "Apartment";
                        room.AdultPrice = 25;
                        room.ChildPrice = 15;
                    }
                    if (room.Type == "3")
                    {
                        room.Type = "Room with a double bed";
                        room.AdultPrice = 30;
                        room.ChildPrice = 20;
                    }
                    if (room.Type == "4")
                    {
                        room.Type = "Penthouse";
                        room.AdultPrice = 35;
                        room.ChildPrice = 25;
                    }
                    if (room.Type == "5")
                    {
                        room.Type = "Massoinette";
                        room.AdultPrice = 40;
                        room.ChildPrice = 30;
                    }
                    _db.Rooms.Add(room);
                }
                else
                {
                    if (room.Type=="1")
                    {
                        room.AdultPrice = 20.0;
                        room.ChildPrice = 10.0;
                    }
                    if (room.Type == "2")
                    {
                        room.AdultPrice = 25;
                        room.ChildPrice = 15;
                    }
                    if (room.Type == "3")
                    {
                        room.AdultPrice = 30;
                        room.ChildPrice = 20;
                    }
                    if (room.Type == "4")
                    {
                        room.AdultPrice = 35;
                        room.ChildPrice = 25;
                    }
                    if (room.Type == "5")
                    {
                        room.AdultPrice = 40;
                        room.ChildPrice = 30;
                    }
                    _db.Rooms.Update(room);
                }
                _db.SaveChanges();
                return RedirectToAction("RoomsView");
            }
            return View(room);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Rooms.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var bookFromDb = await _db.Rooms.FirstOrDefaultAsync(u => u.Id == id);
            if (bookFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            _db.Rooms.Remove(bookFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful" });
        }
    }
}