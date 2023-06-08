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
        public string? TopHasTag1 { get; set; }
        public string? TopHasTag2 { get; set; }
        public string? TopHasTag3 { get; set; }
        public string? TopHasTag4 { get; set; }
        public string? TopHasTag5 { get; set; }
        public string? TopHasTag6 { get; set; }

        //
        public List<string>? PostHashTags { get; set; }
        public List<HashTags>? HashTags { get; set; }
    }
}
