using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Voyager.Domain.Entities;
using Voyager.Domain.Entities.Common;
using Voyager.Domain.Identity;

namespace Voyager.Persistence.Contexts
{
    public class VoyagerDbContext : IdentityDbContext<AppUser>
    {
        public VoyagerDbContext(DbContextOptions<VoyagerDbContext> options) : base(options) { }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<HotelManager> HotelManagers { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<BaseEntity>();

            foreach (var data in datas)
            {
                switch (data.State)
                {
                    case EntityState.Added:
                        data.Entity.DateCreated = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        data.Entity.DateModified = DateTime.UtcNow;
                        break;
                    default:
                        break;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
