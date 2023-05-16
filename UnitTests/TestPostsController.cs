using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyMainDiplomProject.Controllers;
using System;

namespace UnitTests
{
    [TestClass]
    public class TestPostsController
    {
        [TestMethod]
        public void TestMethodCrea()
        {
            PostsController controller = new PostsController(); 
            var result = controller.Store()
        }
    }
}
