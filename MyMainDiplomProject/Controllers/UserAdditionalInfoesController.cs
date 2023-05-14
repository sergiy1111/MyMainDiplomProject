using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyMainDiplomProject.Data;
using MyMainDiplomProject.Models;
using MyMainDiplomProject.Models.ViewModel;

namespace MyMainDiplomProject.Controllers
{
    public class UserAdditionalInfoesController : Controller
    {
        private readonly MyMainDiplomProjectDbContext _context;

        public UserAdditionalInfoesController(MyMainDiplomProjectDbContext context)
        {
            _context = context;
        }

        // GET: UserAdditionalInfoes
        public ActionResult Index(string UserName)
        {
            string UserId = Convert.ToString(_context.Users.Where(i => i.Email == UserName).FirstOrDefault().Id);
            var myMainDiplomProjectDbContext = _context.Posts.Include(i => i.PostHashTags).Include(i => i.User).Where(i => i.User.Id == UserId).FirstOrDefault();
            return View(myMainDiplomProjectDbContext);
        }

        // GET: UserAdditionalInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UserAdditionalInfo == null)
            {
                return NotFound();
            }

            var userAdditionalInfo = await _context.UserAdditionalInfo
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userAdditionalInfo == null)
            {
                return NotFound();
            }

            return View(userAdditionalInfo);
        }

        // GET: UserAdditionalInfoes/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: UserAdditionalInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,Description,ShowDescription,WorkPlase,ShowWorkPlase,UserInterests,ShowUserInterests,Education,ShowEducation")] UserAdditionalInfo userAdditionalInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userAdditionalInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userAdditionalInfo.UserId);
            return View(userAdditionalInfo);
        }

        // GET: UserAdditionalInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UserAdditionalInfo == null)
            {
                return NotFound();
            }

            var userAdditionalInfo = await _context.UserAdditionalInfo.FindAsync(id);
            if (userAdditionalInfo == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userAdditionalInfo.UserId);
            return View(userAdditionalInfo);
        }

        // POST: UserAdditionalInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Description,ShowDescription,WorkPlase,ShowWorkPlase,UserInterests,ShowUserInterests,Education,ShowEducation")] UserAdditionalInfo userAdditionalInfo)
        {
            if (id != userAdditionalInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userAdditionalInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserAdditionalInfoExists(userAdditionalInfo.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userAdditionalInfo.UserId);
            return View(userAdditionalInfo);
        }

        // GET: UserAdditionalInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UserAdditionalInfo == null)
            {
                return NotFound();
            }

            var userAdditionalInfo = await _context.UserAdditionalInfo
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userAdditionalInfo == null)
            {
                return NotFound();
            }

            return View(userAdditionalInfo);
        }

        // POST: UserAdditionalInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UserAdditionalInfo == null)
            {
                return Problem("Entity set 'MyMainDiplomProjectDbContext.UserAdditionalInfo'  is null.");
            }
            var userAdditionalInfo = await _context.UserAdditionalInfo.FindAsync(id);
            if (userAdditionalInfo != null)
            {
                _context.UserAdditionalInfo.Remove(userAdditionalInfo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }




        public IActionResult Test()
        {
            string UserId = Convert.ToString(_context.Users.Where(i => i.Email == User.Identity.Name).FirstOrDefault().Id);
            var test = _context.UserAdditionalInfo.Where(i => i.UserId == UserId);
            if (test != null)
            {

            }

            return View();
        }
        [HttpPost]
        public IActionResult EditDesctiption(PostViewModel model)
        {
            if (ModelState.IsValid)
            {
                string UserId = Convert.ToString(_context.Users.Where(i => i.Email == User.Identity.Name).FirstOrDefault().Id);
                var test = _context.UserAdditionalInfo.Where(i => i.UserId == UserId);
                if (test != null)
                {
                    var additionalInfo = _context.UserAdditionalInfo.FindAsync(UserId);

                    
                    UserAdditionalInfo userAdditionalInfo = new UserAdditionalInfo
                    {
                        
                        UserId = UserId,
                        //Description = model.Description;

                    };
                    //_context.Update(additionalInfo);
                    //_context.SaveChangesAsync();
                }
            }
                return View("EditDesctiption", model);
        }



        private bool UserAdditionalInfoExists(int id)
        {
          return (_context.UserAdditionalInfo?.Any(e => e.Id == id)).GetValueOrDefault();
        }


    }
}
