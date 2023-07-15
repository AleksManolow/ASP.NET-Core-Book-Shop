using BookShopSystem.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookShopSystem.Data
{
    public class BookShopDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public BookShopDbContext(DbContextOptions<BookShopDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Purchase>()
                .HasKey(p => new { p.UserId, p.BookId });

            builder.Entity<Purchase>()
                .HasOne(p => p.User)
                .WithMany(g => g.Books)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Purchase>()
                .HasOne(p => p.Book)
                .WithMany(u => u.Users)
                .HasForeignKey(p => p.BookId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Wish>()
                .HasKey(w => new { w.UserId, w.BookId });

            builder.Entity<Wish>()
                .HasOne(w => w.User)
                .WithMany(u => u.Wishes)
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Wish>()
                .HasOne(w => w.Book)
                .WithMany(u => u.WishesBook)
                .HasForeignKey(w => w.BookId)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(builder);
        }
        public DbSet<Book> Books { get; init; } = null!;
        public DbSet<Genre> Genres { get; init; } = null!;
        public DbSet<Manager> Managers { get; init; } = null!;
        public DbSet<Wish> Wishes { get; init; } = null!;
        public DbSet<Purchase> Purchases { get; init; } = null!;

    }
}