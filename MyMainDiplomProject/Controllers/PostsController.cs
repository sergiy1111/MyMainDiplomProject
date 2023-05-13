using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyMainDiplomProject.Data;
using MyMainDiplomProject.Models;

namespace MyMainDiplomProject.Controllers
{
    public class PostsController : Controller
    {
        private readonly MyMainDiplomProjectDbContext _context;

        public PostsController(MyMainDiplomProjectDbContext context)
        {
            _context = context;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            var myMainDiplomProjectDbContext = _context.Posts.Include(p => p.User);
            return View(await myMainDiplomProjectDbContext.ToListAsync());
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: /Post/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostCreateViewModel ViewModel)
        {
            if (ModelState.IsValid)
            {
                Post NewPost = new Post();
                NewPost.Text = ViewModel.Text;
                NewPost.CreatedDateRime = DateTime.Now;
                //NewPost.UserId = Convert.ToInt32(_userManager.GetUserId(User));
                NewPost.User = _context.Users.Where(i => i.Email == User.Identity.Name).FirstOrDefault();
                var UserId = NewPost.User.Id;
                NewPost.UserId = UserId;

                /*
                if (ViewModel.PostHashTags.Count() != 0)
                {
                    List<HashTags> HashTags = new List<HashTags>();

                    foreach (var Item in ViewModel.PostHashTags)
                    {
                        if (_context.HashTags.Find(Item) != null)
                        {
                            HashTags NewHashTags = new HashTags();
                            NewHashTags.Name = Item;
                            _context.HashTags.Add(NewHashTags);

                            PostHashTags NewPostHagsTag = new PostHashTags();
                            NewPostHagsTag.PostId = _context.HashTags.Find(Item).Id;
                            _context.PostHashTags.Add(NewPostHagsTag);
                            _context.SaveChanges();
                        }
                    }
                }
                */
                _context.Posts.Add(NewPost);
                _context.SaveChanges();
            }
            return View();
        }


        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", post.UserId);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Text,CreatedDateRime")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", post.UserId);
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Posts == null)
            {
                return Problem("Entity set 'MyMainDiplomProjectDbContext.Posts'  is null.");
            }
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
          return (_context.Posts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
