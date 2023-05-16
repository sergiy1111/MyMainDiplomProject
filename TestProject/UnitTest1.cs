using Microsoft.EntityFrameworkCore;
using MyMainDiplomProject.Controllers;
using MyMainDiplomProject.Models;
using MyMainDiplomProject.Models.ViewModel;

namespace TestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethodStore()
        {
            PostsController obj = new PostsController(null);
            PostViewModel model = new PostViewModel
            {
                Text = "It is test text"
            };
            obj.Store(model);
        }

        [TestMethod]
        public void TestMethodDelete()
        {
            PostsController obj = new PostsController(null);
            int PostId = 0;
            obj.Delete(PostId);
        }

        [TestMethod]
        public void TestMethodAddLike()
        {
            LikesController obj = new LikesController(null);
            int PostId = 0;
            obj.AddLike(PostId);
        }
    }
}