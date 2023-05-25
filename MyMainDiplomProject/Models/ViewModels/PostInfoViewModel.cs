using MyMainDiplomProject.Areas.Identity.Data;

namespace MyMainDiplomProject.Models.ViewModels
{
    public class PostInfoViewModel
    {
        public List<MyMainDiplomProjectUser>? Users { get; set; }
        public List<Post>? Posts { get; set; }
        public List<UserAdditionalInfo>? UserAdditionalInfos { get; set; }
        public List<FollowList>? FollowLists { get; set; }

        //Big post

        public Post? Post { get; set; }
        public UserAdditionalInfo? UserAdditionalInfo { get; set; }
    }
}
