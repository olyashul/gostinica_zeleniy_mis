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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BronsController(ptoba_svoego_vhoda_reg_2Context context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: Brons
        public async Task<IActionResult> Index()
        {
            var ptoba_svoego_vhoda_reg_2Context = _context.Bron.Include(b => b.Nomer).Include(b => b.User);
            return View(await ptoba_svoego_vhoda_reg_2Context.ToListAsync());
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
            ViewData["NomerId"] = new SelectList(_context.Nomer, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id"); // Или другое подходящее поле для отображения
            return View();
        }

        // POST: Brons/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Data_zaezd, Data_viezd, Stoimost, UserId, NomerId")] Bron bron)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(bron);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Ошибка сохранения: {ex.Message}");
                    Console.WriteLine($"Error saving changes: {ex.Message}");
                    Console.WriteLine(ex.StackTrace);
                }
            }

            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine($"Error: {error.ErrorMessage}");
                    }
                }
            }
            ViewData["NomerId"] = new SelectList(_context.Nomer, "Id", "Id", bron.NomerId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", bron.UserId);
            return View(bron);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bron == null)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Data_zaezd, Data_viezd, Stoimost, UserId, NomerId")] Bron bron)
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
                    return RedirectToAction(nameof(Index));
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







        public IActionResult OforBroni()
        {
            ViewData["NomerId"] = new SelectList(_context.Nomer, "Id", "Name"); // Изменено здесь
            return View();
        }




        //POST: Brons/Oforbron(изменили Create на Oforbron)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OforBroni([Bind("Data_zaezd, Data_viezd, Stoimost, NomerId")] Bron bron)
        {
            int? userId = _httpContextAccessor.HttpContext.Session.GetInt32("UserId");

            if (userId == null || userId == 0)
            {
                return RedirectToAction("Login", "User");
            }

            bron.UserId = userId.Value;
            if (ModelState.IsValid)
            {
                try
                {
                    // Сохранение стоимости, полученной из формы
                    _context.Add(bron);
                    await _context.SaveChangesAsync();
                    //return RedirectToAction(nameof(Index));
                    return RedirectToAction("Profile", "Users");

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Ошибка сохранения: {ex.Message}");
                    Console.WriteLine($"Error saving changes: {ex.Message}");
                    Console.WriteLine(ex.StackTrace);
                }
            }

            ViewData["NomerId"] = new SelectList(_context.Nomer, "Id", "Id", bron.NomerId);
            return View(bron);
        }


        [HttpGet]
        public IActionResult GetPricePerDay(int NomerId)
        {
            var nomer = _context.Nomer.Find(NomerId);
            if (nomer == null)
            {
                return Json(null); // Или вернуть ошибку
            }
            return Json(nomer.PricePerDay);
        }




        [HttpGet]
        public IActionResult GetBookedDates(int nomerId, string date)
        {
            // Преобразуем строку date в DateTime
            if (!DateTime.TryParse(date, out DateTime selectedDate))
            {
                return Json(false); // Некорректная дата
            }

            var isBooked = _context.Bron
                .Any(b => b.NomerId == nomerId && b.Data_zaezd <= selectedDate && b.Data_viezd >= selectedDate);

            return Json(isBooked);
        }


        [HttpGet]
        public IActionResult AllGetBookedDates(int NomerId)
        {
            Console.WriteLine("Полученный NomerId: " + NomerId); // Добавьте эту строку

            var bookedPeriods = _context.Bron
                .Where(b => b.NomerId == NomerId)
                .Select(b => new
                {
                    StartDate = b.Data_zaezd == DateTime.MinValue ? "" : b.Data_zaezd.ToString("yyyy-MM-dd"),
                    EndDate = b.Data_viezd == DateTime.MinValue ? "" : b.Data_viezd.ToString("yyyy-MM-dd")
                })
                .ToList();

            return Json(bookedPeriods);
        }



        private bool BronExists(int id)
        {
            return (_context.Bron?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
