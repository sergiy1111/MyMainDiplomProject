using MyMainDiplomProject.Areas.Identity.Data;

namespace MyMainDiplomProject.Models.ViewModels
{
    public class UserProfileViewModel
    {
        public MyMainDiplomProjectUser User { get; set; }
        public List<Post>? Posts { get; set; }
        public UserAdditionalInfo? UserAdditionalInfo { get; set; }
        public List<FollowList>? FollowLists { get; set; }
    }
}
