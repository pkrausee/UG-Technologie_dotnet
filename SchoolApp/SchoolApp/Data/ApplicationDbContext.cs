using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Models;

namespace SchoolApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Grade> Grade { get; set; }

        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modeluilder);
            modelBuilder.Entity<Book>()
                .HasOne(p => p.Author)
                .WithMany(b => b.Books)
                .HasForeignKey(p => p.AuthorId);
            modelBuilder.Entity<AppAuthor>().HasNoKey();
        }
        */
    }
}
