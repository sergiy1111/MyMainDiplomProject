using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyMainDiplomProject.Data;
using MyMainDiplomProject.Models;
using MyMainDiplomProject.Models.ViewModel;

namespace MyMainDiplomProject.Controllers
{
    public class CommentsController : Controller
    {
        private readonly MyMainDiplomProjectDbContext _context;

        public CommentsController(MyMainDiplomProjectDbContext context)
        {
            _context = context;
        }
        /*
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CommentViewModel model)
        {
            Comments NewComment = new Comments
            {
                UserId = Convert.ToString(_context.Users.Where(i => i.Email == User.Identity.Name).FirstOrDefault().Id),
                Text = model.Text,
            };

            _context.Add(NewComment);
            _context.SaveChangesAsync();
            return Ok();
        }
        */
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CommentViewModel model, int postId)
        {
            Comments newComment = new Comments
            {
                UserId = Convert.ToString(_context.Users.FirstOrDefault(i => i.Email == User.Identity.Name)?.Id),
                Text = model.Text,
                PostId = postId,
                CreateDate = DateTime.Now()
            };

            _context.Add(newComment);
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
