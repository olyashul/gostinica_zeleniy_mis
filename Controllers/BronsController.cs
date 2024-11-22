using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ptoba_svoego_vhoda_reg_2.Data;
using ptoba_svoego_vhoda_reg_2.Models;

namespace ptoba_svoego_vhoda_reg_2.Controllers
{
    public class BronsController : Controller
    {
        private readonly ptoba_svoego_vhoda_reg_2Context _context;

        public BronsController(ptoba_svoego_vhoda_reg_2Context context)
        {
            _context = context;
        }

        // GET: Brons
        public async Task<IActionResult> Index()
        {
            var bron = await _context.Bron.Include(b => b.Nomer).Include(b => b.User).ToListAsync();
            return View(bron); //Более чистый код
        }
        // GET: Brons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bron = await _context.Bron
                .Include(b => b.Nomer)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bron == null)
            {
                return NotFound();
            }

            return View(bron);
        }

        // GET: Brons/Create
        public IActionResult Create()
        {
            // Загрузка данных пользователей
            var users = _context.User.Select(u => new { Id = u.Id, FirstName = u.FirstName }).ToList();
            ViewBag.UserId = new SelectList(users, "Id", "FirstName");

            // Загрузка данных номеров
            var nomers = _context.Nomer.Select(n => new { Id = n.Id, Name = n.Name }).ToList();
            ViewBag.NomerId = new SelectList(nomers, "Id", "Name");

            return View();
        }

        // POST: Brons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Bron bron)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bron);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Перенаправление на страницу со списком броней
            }
            // Загрузка данных пользователей и номеров (как в методе GET)
            var users = _context.User.Select(u => new { Id = u.Id, FirstName = u.FirstName }).ToList();
            ViewBag.UserId = new SelectList(users, "Id", "FirstName");
            var nomers = _context.Nomer.Select(n => new { Id = n.Id, Name = n.Name }).ToList();
            ViewBag.NomerId = new SelectList(nomers, "Id", "Name");
            return View(bron); // Возврат на форму с ошибками валидации, если таковые есть
        }

        // GET: Brons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bron = await _context.Bron.FindAsync(id);
            if (bron == null)
            {
                return NotFound();
            }
            ViewData["NomerId"] = new SelectList(_context.Nomer, "Id", "Id", bron.NomerId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", bron.UserId);
            return View(bron);
        }

        // POST: Brons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Data_zaezd,Data_viezd,Stoimost,UserId,NomerId")] Bron bron)
        {
            if (id != bron.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bron);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BronExists(bron.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["NomerId"] = new SelectList(_context.Nomer, "Id", "Id", bron.NomerId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", bron.UserId);
            return View(bron);
        }

        // GET: Brons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bron = await _context.Bron
                .Include(b => b.Nomer)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bron == null)
            {
                return NotFound();
            }

            return View(bron);
        }

        // POST: Brons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bron = await _context.Bron.FindAsync(id);
            if (bron != null)
            {
                _context.Bron.Remove(bron);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BronExists(int id)
        {
            return _context.Bron.Any(e => e.Id == id);
        }
    }
}
