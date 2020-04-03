using Microsoft.EntityFrameworkCore;
using Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFramework
{
    public class ContextClass : DbContext
    {
        public DbSet<UserPoco> User { get; set; }
        public DbSet<BookPoco> Book { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-FKSSKG6\HUMBERBRIDGING;Initial Catalog = LibraryDatabse;Integrated Security=True;");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookPoco>(b =>
                {
                    b.HasKey(b => b.BookId);
                    b.HasOne(u => u.User).WithMany(b => b.Books);
                });
            base.OnModelCreating(modelBuilder);
        }
    }
}
