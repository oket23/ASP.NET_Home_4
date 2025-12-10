using Home_4.BLL.Models;
using Home_4.DAL.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Home_4.DAL;

public class HomeContext : DbContext
{
    public HomeContext(DbContextOptions<HomeContext> options) : base(options)
    {
    }
    
    public DbSet<Member> Members => Set<Member>();
    public DbSet<Membership> Memberships => Set<Membership>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    
    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MembersConfiguration).Assembly);
    }
    
}