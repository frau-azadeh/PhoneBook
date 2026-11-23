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
            ViewBag.Users = users;
            return View(new CreatePhoneBookEntryViewModel());
        }
      
    }
}