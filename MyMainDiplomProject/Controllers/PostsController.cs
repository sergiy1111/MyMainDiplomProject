using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyMainDiplomProject.Data;
using MyMainDiplomProject.Models;
using MyMainDiplomProject.Models.ViewModel;
using MyMainDiplomProject.Models.ViewModels;

namespace MyMainDiplomProject.Controllers
{
    public class PostsController : Controller
    {
        private readonly MyMainDiplomProjectDbContext _context;

        public PostsController(MyMainDiplomProjectDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var topHashtags = _context.HashTags
                .GroupBy(ht => ht.Name)
                .OrderByDescending(g => g.Count())
                .Take(3)
                .Select(g => new { Name = g.Key, Count = g.Count() }).ToList();

            PostInfoViewModel model = new PostInfoViewModel
            {
                Posts = _context.Posts.Include(p => p.User).Include(i => i.PostHashTags).Include(i => i.Files).Include(i => i.Likes).Include(i => i.Comments).OrderByDescending(i => i.CreatedDateRime).ToList(),
                Users = _context.Users.ToList(),
                UserAdditionalInfos = _context.UserAdditionalInfo.ToList(),
                FollowLists = _context.FollowLists.ToList(),
                TopHasTag1 = topHashtags.Count >= 1 ? topHashtags[0].Name : "",
                TopHasTag2 = topHashtags.Count >= 2 ? topHashtags[1].Name : "",
                TopHasTag3 = topHashtags.Count >= 3 ? topHashtags[2].Name : "",
                TopHasTag4 = topHashtags.Count >= 4 ? topHashtags[3].Name : "",
                TopHasTag5 = topHashtags.Count >= 5 ? topHashtags[4].Name : ""
            };
           
            return RedirectToAction("Index1", model);
        }

        public async Task<IActionResult> Index1()
        {
            var topHashtags = _context.HashTags
                .GroupBy(ht => ht.Name)
                .OrderByDescending(g => g.Count())
                .Take(3)
                .Select(g => new { Name = g.Key, Count = g.Count() }).ToList();

            PostInfoViewModel model = new PostInfoViewModel
            {
                Posts = _context.Posts.Include(p => p.User).Include(i => i.PostHashTags).Include(i => i.Files).Include(i => i.Likes).Include(i => i.Comments).OrderByDescending(i => i.CreatedDateRime).ToList(),
                Users = _context.Users.ToList(),
                UserAdditionalInfos = _context.UserAdditionalInfo.ToList(),
                FollowLists = _context.FollowLists.ToList(),
                TopHasTag1 = topHashtags.Count >= 1 ? topHashtags[0].Name : "",
                TopHasTag2 = topHashtags.Count >= 2 ? topHashtags[1].Name : "",
                TopHasTag3 = topHashtags.Count >= 3 ? topHashtags[2].Name : "",
                TopHasTag4 = topHashtags.Count >= 4 ? topHashtags[3].Name : "",
                TopHasTag5 = topHashtags.Count >= 5 ? topHashtags[4].Name : ""
            };

            return View(model);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Store(PostViewModel model)
        {
            if (ModelState.IsValid)
            {
                var post = new Post
                {
                    UserId = Convert.ToString(_context.Users.Where(i => i.Email == User.Identity.Name).FirstOrDefault().Id),
                    Text = model.Text,
                    CreatedDateRime = DateTime.Now,
                    Files = new List<Files>(),
                    PostHashTags = new List<HashTags>()
                };

                if (model.HashTags != null)
                {
                    foreach (var tag in model.HashTags)
                    {
                       if(_context.HashTags.Where(i => i.Name == tag).Count() > 0)
                       {
                            post.PostHashTags.Add(_context.HashTags.Where(i => i.Name == tag).FirstOrDefault());
                       }
                       else
                       {
                            post.PostHashTags.Add(new HashTags() { Name = tag });
                       }
                    }
                }

                var file = new Files
                {
                    FileName = @"https://i1.sndcdn.com/avatars-000319437721-fr8dmf-t500x500.jpg"
                };
                post.Files.Add(file);


                _context.Posts.Add(post);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }

            return View("Create", model);
        }

        public async Task<IActionResult> UserProfile(string Id)
        {
            var myMainDiplomProjectDbContext = _context.Posts
                .Include(i => i.User)
                .Include(i => i.Files)
                .Include(i => i.PostHashTags)
                .Include(i => i.Files)
                .Include(i => i.Comments)
                .Include(i => i.Likes)
                .Where(i => i.UserId == Id)
                .FirstOrDefault();

            UserProfileViewModel model = new UserProfileViewModel
            {
                User = _context.Users.Where(i => i.Id == _context.Users.Where(i => i.Id == Id).FirstOrDefault().Id).FirstOrDefault(),
                Posts = _context.Posts.Include(i => i.Files).Include(i => i.Likes).Include(i => i.PostHashTags).Include(i => i.Comments).Where(i => i.UserId == _context.Users.Where(i => i.Id == Id).FirstOrDefault().Id).ToList(),
                UserAdditionalInfo = _context.UserAdditionalInfo.Where(i => i.UserId == _context.Users.Where(i => i.Id == Id).FirstOrDefault().Id).FirstOrDefault(),
                FollowLists = _context.FollowLists.Where(i => i.UserId == Id).ToList()
            };
            return View("UserProfile", model);
        }

        [Authorize]
        public async Task<IActionResult> MyUserProfile()
        {
            string Id = _context.Users.Where(i => i.Email == User.Identity.Name).FirstOrDefault().Id;

            UserProfileViewModel model = new UserProfileViewModel
            {
                User = _context.Users.Where(i => i.Id == Id).FirstOrDefault(),
                Posts = _context.Posts.Include(i => i.Files).Include(i => i.Likes).Include(i => i.PostHashTags).Include(i => i.Comments).Where(i => i.UserId == Id).ToList(),
                UserAdditionalInfo = _context.UserAdditionalInfo.Where(i => i.UserId == Id).FirstOrDefault(),
                FollowLists = _context.FollowLists.Where(i => i.UserId == Id).ToList()
            };
            return View("UserProfile", model);

        }

        public ActionResult BigPost(int Id)
        {
            var post = _context.Posts
                .Include(i => i.User)
                .Include(i => i.Files)
                .Include(i => i.PostHashTags)
                .Include(i => i.Files)
                .Include(i => i.Comments)
                .Include(i => i.Likes)
                .Where(i => i.Id == Id)
                .FirstOrDefault();

            PostInfoViewModel model = new PostInfoViewModel
            {
                Post = post,
                UserAdditionalInfo = _context.UserAdditionalInfo.Where(i => i.UserId == post.UserId).FirstOrDefault()
            };
            return PartialView(model);
            
        }

        public ActionResult SearchByHashTag(string HasTag)
        {
            List<Post> filteredPosts = _context.Posts
                .Include(p => p.User)
                .Include(i => i.PostHashTags)
                .Include(i => i.Files)
                .Include(i => i.Likes)
                .Include(i => i.Comments)
                .Where(p => p.PostHashTags.Any(i => i.Name == HasTag))
                .OrderByDescending(i => i.CreatedDateRime)
                .ToList();

            PostInfoViewModel model = new PostInfoViewModel
            {
                Posts = filteredPosts,
                Users = _context.Users.ToList(),
                UserAdditionalInfos = _context.UserAdditionalInfo.ToList(),
                FollowLists = _context.FollowLists.ToList(),
            };

            return PartialView(model);
        }


        public ActionResult Search(string name, string hashtag, string userType)
        {
            List<Post> filteredPosts = new List<Post>();
            List<FollowList> follow = _context.FollowLists.ToList();
            string userId; 


            filteredPosts = _context.Posts
                    .Include(p => p.User)
                    .Include(i => i.PostHashTags)
                    .Include(i => i.Files)
                    .Include(i => i.Likes)
                    .Include(i => i.Comments)
                    .OrderByDescending(i => i.CreatedDateRime)
                    .ToList();

            if (name != null)
            {
                filteredPosts = filteredPosts.Where(i => i.User.userNikName.Contains(name)).ToList();
            }

            if (hashtag != null)
            {
                filteredPosts = filteredPosts.Where(i => i.PostHashTags.Any(i => i.Name == hashtag)).ToList();
            }

            if (userType == "friend")
            {
                userId = Convert.ToString(_context.Users.Where(i => i.Email == User.Identity.Name).FirstOrDefault().Id);
                follow = _context.FollowLists.Where(i => i.UserId == userId).ToList();
                filteredPosts = filteredPosts.Join(
                    follow,
                    post => post.UserId,
                    f => f.FolloverUserId,
                    (post, f) => post
                ).ToList();
            }

            else if (userType == "all")
            {
                userId = Convert.ToString(_context.Users.Where(i => i.Email == User.Identity.Name).FirstOrDefault().Id);
                follow = _context.FollowLists.Where(i => i.UserId != userId).ToList();
                filteredPosts = filteredPosts.Join(
                    follow,
                    post => post.UserId,
                    f => f.FolloverUserId,
                    (post, f) => post
                ).ToList();
            }

            PostInfoViewModel model = new PostInfoViewModel
            {
                Posts = filteredPosts,
                Users = _context.Users.ToList(),
                UserAdditionalInfos = _context.UserAdditionalInfo.ToList(),
                FollowLists = follow,
            };

            return PartialView(model);
        }








        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.User).Include(p => p.Files)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
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

