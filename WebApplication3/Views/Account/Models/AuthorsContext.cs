using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WebApplication3.Models
{
    public class AuthorsContext: DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Event> Events { get; set; }

        public AuthorsContext() : base("DefaultConnection")
    { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>().HasMany(c => c.Authors)
                .WithMany(s => s.Events)
                .Map(t => t.MapLeftKey("EventId")
                .MapRightKey("AuthorId")
                .ToTable("EventAuthor"));
        }

    }
}