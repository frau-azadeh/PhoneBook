using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Data;

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
    }
}