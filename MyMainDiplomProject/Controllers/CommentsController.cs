using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CommentViewModel model, int postId)
        {
            Comments newComment = new Comments
            {
                UserId = Convert.ToString(_context.Users.FirstOrDefault(i => i.Email == User.Identity.Name)?.Id),
                Text = model.Text,
                PostId = postId,
                CreateDate = DateTime.Now
            };

            _context.Add(newComment);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public IActionResult GetComments(int postId)
        {
            // Отримати список коментарів за допомогою postId
            var comments = _context.Comments.Where(c => c.PostId == postId).ToList();

            // Повернути часткове представлення з оновленим списком коментарів
            return PartialView("_CommentsPartial", comments);
        }


    }
}
