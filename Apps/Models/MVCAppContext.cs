using MassTransit.Mediator;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apps.MVCApp.Models
{
    public class MVCAppContext : IdentityDbContext<AppUser>
    {        
        public MVCAppContext(DbContextOptions<MVCAppContext> options, IMediator mediator)
            : base(options)
        {

           Database.EnsureCreated();
        }

       

        public DbSet<Request> requests { get; set; }

       
        public DbSet<Client> clients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           //optionsBuilder.UseLazyLoadingProxies();         
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            //builder.Entity<AppUser>(b =>
            //{
            //    b.HasMany(e => e.requests)
            //    .WithOne(e => e.user)
            //    .HasForeignKey(e => e.user_id)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.Restrict);
            //});


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
