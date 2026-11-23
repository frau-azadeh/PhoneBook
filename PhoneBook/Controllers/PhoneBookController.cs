using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Data;
using PhoneBook.Models;

namespace PhoneBook.Controllers
{
    public class PhoneBookController : Controller
    {
        private readonly PhoneBookDbContext _context;
        public PhoneBookController(PhoneBookDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var contacts = await _context.Entries
                .AsNoTracking()
                .Where(x => !x.IsDeleted)
                .ToListAsync();
            return View(contacts);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var users = await _context.Users
                .AsNoTracking()
                .Where(x => x.FirstName != null && x.LastName != null)
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .ToListAsync();
            var units = await _context.Units
                .AsNoTracking()
                .Where(x => x.Title != null || x.FullName != null)
                .OrderBy(x => x.Title)
                .ToListAsync();
            ViewBag.Users = users;
            ViewBag.Units = units;
            return View(new CreatePhoneBookEntryViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePhoneBookEntryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await LoadCreateOptionsAsync();
                return View(model);
            }
            var user = await _context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == model.UserId);
            var unit = await _context.Units
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == model.UnitId.Value);

            if(user == null || string.IsNullOrWhiteSpace(user.FirstName)
                || string.IsNullOrWhiteSpace(user.LastName) ||
                unit == null )
            {
                ModelState.AddModelError("", "کاربر یا واحد انتخاب شده معتبر نیست");
                await LoadCreateOptionsAsync();
                return View(model);
            }
            bool alreadyRegistered = await _context.Entries
                .AnyAsync(X => X.Id == user.Id);
            if (alreadyRegistered)
            {
                ModelState.AddModelError("", "این کاربر قبلا ثبت نام کرده");
                await LoadCreateOptionsAsync();
                return View(model);
            }

            var unitName = string.IsNullOrWhiteSpace(unit.Title)
                ? unit.FullName
                : unit.Title;
            if(string.IsNullOrWhiteSpace(unitName)|| unitName.Length>150 || 
                string.IsNullOrWhiteSpace(model.InternalPhone))

            {
                ModelState.AddModelError("", "نام واحد یا شماره داخلی معتبر نیست");
                await LoadCreateOptionsAsync();
                return View(model);
            }
            var entry = new PhoneBookEntry
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Unit = unitName,
                InternalPhone = model.InternalPhone.Trim(),
                PhoneNumber = model.PhoneNumber?.Trim(),
                MobileNumber = model.MobileNumber?.Trim(),
                IsDeleted = false,
                Version = 1,
                CreatedAt = DateTime.UtcNow,
                CreatedByUserId = null
            };
            _context.Entries.Add(entry);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            
        }
        private async Task LoadCreateOptionsAsync()
        {
            ViewBag.Users = await _context.Users
                .AsNoTracking()
                .Where(x => x.FirstName != null && x.LastName != null)
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .ToListAsync();

            ViewBag.Units = await _context.Units
                .AsNoTracking()
                .Where(x => x.Title != null || x.FullName != null)
                .OrderBy(x => x.Title)
                .ToListAsync();
        }
    }
}