using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyMainDiplomProject.Areas.Identity.Data;
using MyMainDiplomProject.Models;
using System.Reflection.Emit;

namespace MyMainDiplomProject.Data
{
    public class MyMainDiplomProjectDbContext : IdentityDbContext<MyMainDiplomProjectUser>
    {
        public MyMainDiplomProjectDbContext(DbContextOptions<MyMainDiplomProjectDbContext> options)
            : base(options)
        {
        }

        public MyMainDiplomProjectDbContext()
        {
        }

        public DbSet<Comments> Comments { get; set; }
        public DbSet<Files> Files { get; set; }
        public DbSet<FollowList> FollowLists { get; set; }
        public DbSet<HashTags> HashTags { get; set; }
        public DbSet<Likes> Likes { get; set; }
        public DbSet<Post> Posts { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
        public DbSet<MyMainDiplomProject.Models.UserAdditionalInfo> UserAdditionalInfo { get; set; } = default!;

        public static MyMainDiplomProjectDbContext Create()
        {
            return new MyMainDiplomProjectDbContext();
        }
    }

}

