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
            builder
                .Entity<Book>()
                .Property(x => x.ImageUrl)
                .HasColumnName("ImageUrl");


            builder
                .Entity<Book>()
                .Property(b => b.NumberOfSales)
                .HasDefaultValue(0);
               

            builder.Entity<Genre>()
                .HasData(new Genre()
                {
                    Id = 1,
                    Name= "Fantasy",
                },
                new Genre()
                {
                    Id = 2,
                    Name = "Sci-Fi"
                },
                new Genre()
                {
                    Id = 3,
                    Name = "Mystery"
                },
                new Genre()
                {
                    Id = 4,
                    Name = "Thriller"
                },
                new Genre()
                {
                    Id = 5,
                    Name = "Romance"
                },
                new Genre()
                {
                    Id = 6,
                    Name = "Westerns"
                },
                new Genre()
                {
                    Id = 7,
                    Name = "Dystopian"
                },
                new Genre()
                {
                    Id = 8,
                    Name = "Contemporary"
                });

            builder.Entity<Book>()
                .HasData(new Book()
                {
                    Title = "Anna Karenina",
                    Author = "Leo Tolstoy",
                    Description = "Acclaimed by many as the world's greatest novel, Anna Karenina provides a vast panorama of contemporary life in Russia and of humanity in general.",
                    ImageUrl = "https://data.logograph.com/resize/LyricTheatre/multimedia/Image/4561/Art%20Cinema%20April%20Webslug%20-%20Anna%20Karenina.jpg?width=1500",
                    Price = 20,
                    AgeRestriction = 12,
                    ReleaseDate = DateTime.Parse("1877-06-26 10:57:31.1728595"),
                    GenreId = 1,
                    ManagerId = Guid.Parse("b9517783-f4cd-4c5b-043d-08db771ab7f4")

                },
                new Book()
                {
                    Title = "The War of the Worlds",
                    Author = "H.G. Wells",
                    Description = "The War of the Worlds by H.G. Wells is about a fictional invasion of Southern England by Martians. The military is powerless against the Martians' superior weapons, and many people die. The Martians are eventually killed by bacterial infection.",
                    ImageUrl = "https://productimages.worldofbooks.com/1494745429.jpg",
                    Price = 30,
                    AgeRestriction = 16,
                    ReleaseDate = DateTime.Parse("1898-06-26 10:57:31.1728595"),
                    GenreId = 5,
                    ManagerId = Guid.Parse("b9517783-f4cd-4c5b-043d-08db771ab7f4")

                });

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