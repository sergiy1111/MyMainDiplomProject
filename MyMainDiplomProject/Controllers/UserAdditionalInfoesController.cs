using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyMainDiplomProject.Data;
using MyMainDiplomProject.Models;
using MyMainDiplomProject.Models.ViewModel;
using MyMainDiplomProject.Models.ViewModels;
using static System.Net.WebRequestMethods;

namespace MyMainDiplomProject.Controllers
{
    public class UserAdditionalInfoesController : Controller
    {
        private readonly MyMainDiplomProjectDbContext _context;

        public UserAdditionalInfoesController(MyMainDiplomProjectDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string UserId)
        {
            if (UserId == "MyProfile")
            {
                UserId = _context.Users.Where(i => i.Email == User.Identity.Name).FirstOrDefault().Id;
            }

            if (_context.UserAdditionalInfo.Where(i => i.UserId == UserId).IsNullOrEmpty())
            {
                UserAdditionalInfo additionalInfo = new UserAdditionalInfo
                {
                    UserId = UserId,
                    ShowDescription = false,
                    ShowEducation = false,
                    ShowWorkPlase = false,
                    ShowUserInterests = false,
                    UserAvatar = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/59/User-avatar.svg/2048px-User-avatar.svg.png"
                };
                _context.Add(additionalInfo);
                _context.SaveChanges();
            }

            var myMainDiplomProjectDbContext = _context.UserAdditionalInfo.Include(i => i.User).Where(i => i.UserId == UserId).FirstOrDefault();

            return View(myMainDiplomProjectDbContext);
        }

        public async Task<IActionResult> UserList()
        {
            foreach (var item in _context.Users)
            {
                if (_context.UserAdditionalInfo.Where(i => i.UserId == item.Id).IsNullOrEmpty())
                {
                    UserAdditionalInfo additionalInfo = new UserAdditionalInfo
                    {
                        UserId = item.Id,
                        ShowDescription = false,
                        ShowEducation = false,
                        ShowWorkPlase = false,
                        ShowUserInterests = false,
                        UserAvatar = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/59/User-avatar.svg/2048px-User-avatar.svg.png"
                    };
                    _context.Add(additionalInfo);
                    _context.SaveChanges();
                }
            }

            var myMainDiplomProjectDbContext = _context.UserAdditionalInfo.Include(i => i.User) ;
            return View(await myMainDiplomProjectDbContext.ToListAsync());

        }

        public async Task<IActionResult> UserProfile(string Id)
        {
            var myMainDiplomProjectDbContext = _context.UserAdditionalInfo.Include(i => i.User).Where(i => i.UserId == Id).FirstOrDefault();

            return View(myMainDiplomProjectDbContext);
        }

        public async Task<IActionResult> MyUserProfile()
        {
            var myMainDiplomProjectDbContext = _context.UserAdditionalInfo.Include(i => i.User).Where(i => i.UserId == _context.Users.Where(i => i.Email == User.Identity.Name).FirstOrDefault().Id).FirstOrDefault();
            return View("UserProfile", myMainDiplomProjectDbContext);
        }

        public async Task<IActionResult> AdminStatsUser()
        {

            DateTime ThirtyDaysAgo = DateTime.Now.AddDays(-30);
            DateTime NinetyDaysAgo = DateTime.Now.AddDays(-90);
            DateTime YearAgo = DateTime.Now.AddDays(-365);
            DateTime FourteenYearsAgo = DateTime.Now.AddYears(-14);
            DateTime TwentyNineYearsAgo = DateTime.Now.AddYears(-29);
            DateTime SixtyYearsAgo = DateTime.Now.AddYears(-59);


            int TotalUsers = _context.Users.Count();
            int ActivUser = _context.Posts.Where(i => i.CreatedDateRime >= ThirtyDaysAgo).Select(i => i.UserId).Distinct().Count();
            int UsersHaveWork = _context.UserAdditionalInfo.Where(i => i.WorkPlase != null).Count();
            int UsersHaveEducation = _context.UserAdditionalInfo.Where(i => i.Education != null).Count();

            StatsViewModel model = new StatsViewModel
            {
                TotalUsers = TotalUsers,
                ActivUser = ActivUser,
                PassiveUsers = TotalUsers - ActivUser,
                UsersForMonth = _context.Users.Where(i => i.registerDate >= ThirtyDaysAgo).Count(),
                UsersForThreeMonth = _context.Users.Where(i => i.registerDate >= NinetyDaysAgo).Count(),
                UsersForYear = _context.Users.Where(i => i.registerDate >= YearAgo).Count(),
                UsersHaveWork = ((UsersHaveWork / TotalUsers) * 100),
                UsersHaveEducation = ((UsersHaveEducation / TotalUsers) * 100),
                AllFollowCount = _context.FollowLists.Count(),
                YoungUsers = _context.Users.Where(i => i.dateOfBirthday >= TwentyNineYearsAgo && i.dateOfBirthday <= FourteenYearsAgo).Count(),
                MiddleAgedUsers = _context.Users.Where(i => i.dateOfBirthday > TwentyNineYearsAgo && i.dateOfBirthday <= SixtyYearsAgo).Count(),
                OldAgedUsers = _context.Users.Where(i => i.dateOfBirthday > SixtyYearsAgo).Count()

            };

            return View(model);
        }

        public async Task<IActionResult> AdminStatsPost()
        {
            DateTime ThirtyDaysAgo = DateTime.Now.AddDays(-30);
            DateTime NinetyDaysAgo = DateTime.Now.AddDays(-90);
            DateTime YearAgo = DateTime.Now.AddDays(-365);

            StatsViewModel model = new StatsViewModel
            {
                TotalPosts = _context.Posts.Count(),
                PostsForMonth = _context.Posts.Where(i => i.CreatedDateRime >= ThirtyDaysAgo).Count(),
                PostsForThreeMonth = _context.Posts.Where(i => i.CreatedDateRime >= NinetyDaysAgo).Count(),
                PostsForYear = _context.Posts.Where(i => i.CreatedDateRime >= YearAgo).Count(),
                PostsWithPictures = _context.Posts.Where(i => i.Files != null).Count(),
                PostsWithHasTags = _context.Posts.Where(i => i.PostHashTags != null).Count(),
            };
            return View(model);
        }

        public async Task<IActionResult> AdminStatsComment()
        {
            DateTime ThirtyDaysAgo = DateTime.Now.AddDays(-30);
            DateTime NinetyDaysAgo = DateTime.Now.AddDays(-90);
            DateTime YearAgo = DateTime.Now.AddDays(-365);

            StatsViewModel model = new StatsViewModel
            {
                TotalComments = _context.Comments.Count(),
                CommentsFowMounth = _context.Comments.Where(i => i.CreateDate >= ThirtyDaysAgo).Count(),
                CommentsForThreeMounth = _context.Comments.Where(i => i.CreateDate >= NinetyDaysAgo).Count(),
                CommentsForYear = _context.Comments.Where(i => i.CreateDate >= YearAgo).Count(),
            };
            return View(model);
        }

        public async Task<IActionResult> AdminStatsHashTags()
        {
            DateTime ThirtyDaysAgo = DateTime.Now.AddDays(-30);
            DateTime NinetyDaysAgo = DateTime.Now.AddDays(-90);
            DateTime YearAgo = DateTime.Now.AddDays(-365);

            var topHashtags = _context.HashTags.GroupBy(ht => ht.Name).OrderByDescending(g => g.Count()).Take(3).Select(g => new { Name = g.Key, Count = g.Count() }).ToList();

            StatsViewModel model = new StatsViewModel
            {
                TopHasTag1 = topHashtags.Count >= 1 ? topHashtags[0].Name : "",
                TopHasTag2 = topHashtags.Count >= 2 ? topHashtags[1].Name : "",
                TopHasTag3 = topHashtags.Count >= 3 ? topHashtags[2].Name : "",
            };
            return View(model);
        }

        public async Task<IActionResult> Search(string name, int? minFollowers, int? maxFollowers, string numUsers, string userType)
        {
            string AutorizeUserId = _context.Users.Where(i => i.Email == User.Identity.Name).FirstOrDefault().Id;
            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(i => i.userNikName.Contains(name));
            }

            if (minFollowers.HasValue || maxFollowers.HasValue)
            {
                var followersQuery = _context.FollowLists.GroupBy(i => i.UserId).Select(i => new { UserId = i.Key, FollowersCount = i.Count() });

                if (minFollowers.HasValue)
                {
                    followersQuery = followersQuery.Where(i => i.FollowersCount >= minFollowers);
                }

                if (maxFollowers.HasValue)
                {
                    followersQuery = followersQuery.Where(i => i.FollowersCount <= maxFollowers);
                }

                var userIdsWithFollowers = await followersQuery.Select(i => i.UserId).ToListAsync();
                query = query.Where(i => userIdsWithFollowers.Contains(i.Id));
            }

            if (numUsers == "verified")
            {
                var verifiedUserIds = _context.UserRoles.Where(i => i.RoleId == "2");
                //query = query.Where(i => verifiedUserIds.Contains(i.Id));
            }

            if (userType == "friend")
            {
                var followerUserIds = await _context.FollowLists.Where(f => f.FolloverUserId == AutorizeUserId).Select(f => f.UserId).ToListAsync();
                query = query.Where(u => followerUserIds.Contains(u.Id));
            }

            var searchResults = await query.ToListAsync();

            return View(searchResults);
        }

        public ActionResult InputOrEdit(int ChangeId)
        {
            string UserId = _context.Users.Where(i => i.Email == User.Identity.Name).FirstOrDefault().Id;

                if (_context.UserAdditionalInfo.Where(i => i.UserId == UserId).IsNullOrEmpty())
                {
                    UserAdditionalInfo additionalInfo = new UserAdditionalInfo
                    {
                        UserId = UserId,
                        ShowDescription = false,
                        ShowEducation = false,
                        ShowWorkPlase = false,
                        ShowUserInterests = false,
                        UserAvatar = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/59/User-avatar.svg/2048px-User-avatar.svg.png"
                    };
                    _context.Add(additionalInfo);
                    _context.SaveChanges();
                }

            ChangeSeatingsViewModel model = new ChangeSeatingsViewModel
            {
                Id = ChangeId,
                UserAdditional = _context.UserAdditionalInfo.Where(i => i.UserId == UserId).FirstOrDefault(),
            };
        
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult InputOrEdit(ChangeSeatingsViewModel model)
        {
            if(model.Id != null)
            {
                UserAdditionalInfo NewInfo = _context.UserAdditionalInfo.Where(i => i.UserId == model.UserAdditional.UserId).FirstOrDefault();
                NewInfo.User = _context.Users.Where(i => i.Id == model.UserAdditional.UserId).FirstOrDefault();

                if(model.Id == 1)
                {
                    if (model.UserAdditional.Description != null)
                    {
                        NewInfo.Description = model.UserAdditional.Description;
                        NewInfo.ShowDescription = model.UserAdditional.ShowDescription;
                    }
                    else
                    {
                        NewInfo.Description = "";
                        NewInfo.ShowDescription = false;
                    }
                }

                else if (model.Id == 2)
                {
                    if(model.UserAdditional.WorkPlase != null)
                    {
                        NewInfo.WorkPlase = model.UserAdditional.WorkPlase;
                        NewInfo.ShowWorkPlase = model.UserAdditional.ShowWorkPlase;
                    }
                    else
                    {
                        NewInfo.WorkPlase = "";
                        NewInfo.ShowWorkPlase = false;
                    }
                }

                else if (model.Id == 3)
                {
                    if(model.UserAdditional.UserInterests != null)
                    {
                        NewInfo.UserInterests = model.UserAdditional.UserInterests;
                        NewInfo.ShowUserInterests = model.UserAdditional.ShowUserInterests;
                    }
                    else
                    {
                        NewInfo.UserInterests = "";
                        NewInfo.ShowUserInterests = false;
                    }
                }

                else if (model.Id == 4)
                {
                    if(model.UserAdditional.Education != null)
                    {
                        NewInfo.Education = model.UserAdditional.Education;
                        NewInfo.ShowEducation = model.UserAdditional.ShowEducation;
                    }
                    else
                    {
                        NewInfo.Education = "";
                        NewInfo.ShowEducation = false;
                    }
                }

                else if (model.Id == 5)
                {
                    if (model.UserAdditional.UserAvatar != null)
                    {
                        NewInfo.UserAvatar = model.UserAdditional.UserAvatar; 
                    }
                    else
                    {
                        NewInfo.UserAvatar = "";
                    }
                }

                else if (model.Id == 6)
                {
                    if (model.Name != null)
                    {
                        NewInfo.User.userNikName = model.Name;
                    }
                    else
                    {
                        NewInfo.User.userNikName = "UserName";
                    }
                }

                else if (model.Id == 7)
                {
                    if (model.Email != null)
                    {
                        NewInfo.User.Email = model.Email;
                    }
                }

                _context.Update(NewInfo);
                _context.SaveChanges();
            }
            return PartialView(model);
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
