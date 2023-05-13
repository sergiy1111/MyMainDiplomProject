namespace MyMainDiplomProject.Models
{
    public class PostHashTags
    {
        public int Id { get; set; }
        public int HashTagId { get; set; }
        public int PostId { get; set; }
        public virtual HashTags HashTags { get; set; }
        public virtual Post Post { get; set; }

    }
}
