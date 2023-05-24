using System.Security.Cryptography.X509Certificates;

namespace MyMainDiplomProject.Models.ViewModels
{
    public class StatsViewModel
    {
        //User stats
        public int? TotalUsers { get; set; }
        public int? ActivUser { get; set;}
        public int? PassiveUsers { get; set; }
        public int? UsersForMonth { get; set;}
        public int? UsersForThreeMonth { get; set; }
        public int? UsersForYear { get; set; }
        public int? UsersHaveWork { get; set; }
        public int? UsersHaveEducation { get; set; }
        public int? AllFollowCount { get; set; }
        public int? YoungUsers { get; set; }
        public int? MiddleAgedUsers { get; set; }
        public int? OldAgedUsers { get; set; }

        //Post stats
        public int? TotalPosts { get; set; }
        public int? PostsForMonth { get; set; }
        public int? PostsForThreeMonth { get; set; }
        public int? PostsForYear { get; set; }
        public int? PostsWithPictures { get; set; }
        public int? PostsWithHasTags { get; set; }

        //Comment stats

        public int? TotalComments { get; set; }
        public int? CommentsFowMounth { get; set; }
        public int? CommentsForThreeMounth { get; set; }
        public int? CommentsForYear { get; set; }

        //Hastags stats
        public int? TotalHasTags { get; set; }
        public string? TopHasTag1 { get; set; }
        public string? TopHasTag2 { get; set; }
        public string? TopHasTag3 { get; set; }

    }
}
