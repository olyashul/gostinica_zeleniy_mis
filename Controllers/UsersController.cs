using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ptoba_svoego_vhoda_reg_2.Data;
using ptoba_svoego_vhoda_reg_2.Models;

namespace ptoba_svoego_vhoda_reg_2.Controllers
{
    public class UsersController : Controller
    {
        private readonly ptoba_svoego_vhoda_reg_2Context _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsersController(ptoba_svoego_vhoda_reg_2Context context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.User.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,PhoneNumber,Email, Password")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,PhoneNumber,Email, Password")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user != null)
            {
                _context.User.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }





        public IActionResult Register()
        {
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (string.IsNullOrEmpty(user.FirstName) ||
                string.IsNullOrEmpty(user.LastName) ||
                string.IsNullOrEmpty(user.PhoneNumber) ||
                string.IsNullOrEmpty(user.Email) ||
                string.IsNullOrEmpty(user.Password))
            {
                ModelState.AddModelError("", "Все поля должны быть заполнены.");
            }
            else
            {
                if (user.PhoneNumber.Length < 11)
                {
                    ModelState.AddModelError("PhoneNumber", "Номер телефона введен не до конца.");
                }
                if (user.Email.Length < 8)
                {
                    ModelState.AddModelError("Email", "Email должен содержать не менее 8 символов.");
                }
                if (user.Password.Length < 8)
                {
                    ModelState.AddModelError("Password", "Пароль должен содержать не менее 8 символов.");
                }
                if (_context.User.Any(u => u.PhoneNumber== user.PhoneNumber))
                {
                    ModelState.AddModelError("PhoneNumber", "Пользователь с таким телефоном уже существует.");
                }
            }


            if (ModelState.IsValid)
            {
                _context.User.Add(user);
                await _context.SaveChangesAsync(); //  !!! ИЗМЕНЕНИЕ !!!  Используем асинхронный метод
                return RedirectToAction("Login");
            }
            return View(user);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string PhoneNumber, string Password)
        {
            var user = _context.User.FirstOrDefault(u => u.PhoneNumber == PhoneNumber && u.Password == Password);
            if (user != null)
            {
                // Сохраняем ID пользователя в сессии
                _httpContextAccessor.HttpContext.Session.SetInt32("UserId", user.Id);
                return RedirectToAction("Profile");
            }
            ModelState.AddModelError("", "Неверный номер телефона или пароль");
            return View();
        }

        public IActionResult Logout()
        {
            _httpContextAccessor.HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }


        //Пример действия, показывающего данные пользователя, доступно только после авторизации
        public IActionResult Profile()
        {
            int? userId = _httpContextAccessor.HttpContext.Session.GetInt32("UserId");
            if (userId.HasValue)
            {
                var user = _context.User.Find(userId.Value);
                if (user != null)
                {
                    return View(user); // Предполагается наличие представления Profile.cshtml
                }
                else
                {
                    return RedirectToAction("Logout"); // Пользователь удален, выходим из системы
                }
            }
            return RedirectToAction("Login"); // Пользователь не авторизован
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
