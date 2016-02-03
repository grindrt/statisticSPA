using System.Data.Entity;
using StatisticSPA.Models;

namespace StatisticSPA.DAL
{
  public class StatisticContext : DbContext
  {
    public StatisticContext()
      : base("name=StatisticContext")
    {
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Client>()
        .HasMany(x => x.Groups).WithMany(x => x.Clients)
        .Map(x => x.MapLeftKey("ClientId")
          .MapRightKey("GroupId")
          .ToTable("ClientGroups"));
    }

    public virtual DbSet<Client> Client { get; set; }
    public virtual DbSet<Group> Group { get; set; }
  }
}