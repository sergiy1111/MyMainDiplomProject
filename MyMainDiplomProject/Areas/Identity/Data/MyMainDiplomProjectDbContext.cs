using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyMainDiplomProject.Areas.Identity.Data;
using MyMainDiplomProject.Models;

namespace MyMainDiplomProject.Data;

public class MyMainDiplomProjectDbContext : IdentityDbContext<MyMainDiplomProjectUser>
{
    public MyMainDiplomProjectDbContext(DbContextOptions<MyMainDiplomProjectDbContext> options)
        : base(options)
    {
    }
    public DbSet<Comments> Comments { get; set; }
    public DbSet<Files> Files { get; set; }
    public DbSet<FollowList> FollowLists { get; set; }
    public DbSet<HashTags> HashTags { get; set; }
    public DbSet<Likes> Likes { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<PostHashTags> PostHashTags { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
