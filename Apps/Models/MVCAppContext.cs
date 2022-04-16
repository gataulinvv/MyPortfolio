using MassTransit.Mediator;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Apps.MVCApp.Models
{
    public class MVCAppContext : IdentityDbContext<AppUser>
    {
        public MVCAppContext(DbContextOptions<MVCAppContext> options, IMediator mediator) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Request> requests { get; set; }
        public DbSet<Client> clients { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Client>(b =>
            {
                b.HasMany(e => e.requests)
                .WithOne(e => e.client)
                .HasForeignKey(e => e.clientid)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            });
        }
    }
}
