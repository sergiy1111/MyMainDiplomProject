using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyMainDiplomProject.Data;
using MyMainDiplomProject.Models;

namespace MyMainDiplomProject.Controllers
{
    public class FollowListsController : Controller
    {
        private readonly MyMainDiplomProjectDbContext _context;

        public FollowListsController(MyMainDiplomProjectDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public ActionResult AddFollow(string Id)
        {

            string UserId = _context.Users.Where(i => i.Email == User.Identity.Name).FirstOrDefault().Id;

            if (_context.FollowLists.Where(i => i.UserId == UserId && i.FolloverUserId == Id).IsNullOrEmpty())
            {
                FollowList followList = new FollowList
                {
                    FolloverUserId = Id,
                    UserId = UserId,
                };
                _context.FollowLists.Add(followList);

            }
            else
            {
                FollowList existingFollow = _context.FollowLists.FirstOrDefault(i => i.UserId == UserId && i.FolloverUserId == Id);
                _context.FollowLists.Remove(existingFollow);
            }
            _context.SaveChangesAsync();
            return Ok();

        }

        // GET: FollowLists
        public async Task<IActionResult> Index()
        {
              return _context.FollowLists != null ? 
                          View(await _context.FollowLists.ToListAsync()) :
                          Problem("Entity set 'MyMainDiplomProjectDbContext.FollowLists'  is null.");
        }

        // GET: FollowLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FollowLists == null)
            {
                return NotFound();
            }

            var followList = await _context.FollowLists
                .FirstOrDefaultAsync(m => m.Id == id);
            if (followList == null)
            {
                return NotFound();
            }

            return View(followList);
        }

        // GET: FollowLists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FollowLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,FolloverUserId")] FollowList followList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(followList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(followList);
        }

        // GET: FollowLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FollowLists == null)
            {
                return NotFound();
            }

            var followList = await _context.FollowLists.FindAsync(id);
            if (followList == null)
            {
                return NotFound();
            }
            return View(followList);
        }

        // POST: FollowLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,FolloverUserId")] FollowList followList)
        {
            if (id != followList.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(followList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FollowListExists(followList.Id))
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
            return View(followList);
        }

        // GET: FollowLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FollowLists == null)
            {
                return NotFound();
            }

            var followList = await _context.FollowLists
                .FirstOrDefaultAsync(m => m.Id == id);
            if (followList == null)
            {
                return NotFound();
            }

            return View(followList);
        }

        // POST: FollowLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FollowLists == null)
            {
                return Problem("Entity set 'MyMainDiplomProjectDbContext.FollowLists'  is null.");
            }
            var followList = await _context.FollowLists.FindAsync(id);
            if (followList != null)
            {
                _context.FollowLists.Remove(followList);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FollowListExists(int id)
        {
          return (_context.FollowLists?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
