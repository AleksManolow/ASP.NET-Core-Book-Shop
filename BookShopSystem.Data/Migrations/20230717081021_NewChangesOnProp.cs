using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookShopSystem.Data.Migrations
{
    public partial class NewChangesOnProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("1bd0621e-7d16-4382-bd9e-75dfa71b5754"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("d4756aab-b1f5-4b72-8343-6379f7f73b04"));

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AgeRestriction", "Author", "Description", "GenreId", "ImageUrl", "IsActive", "ManagerId", "Price", "ReleaseDate", "Title" },
                values: new object[] { new Guid("71694f32-dd73-4399-a0a8-bc718f2aadcb"), 16, "H.G. Wells", "The War of the Worlds by H.G. Wells is about a fictional invasion of Southern England by Martians. The military is powerless against the Martians' superior weapons, and many people die. The Martians are eventually killed by bacterial infection.", 5, "https://www.gutenberg.org/files/36/36-h/36-h.htm", true, new Guid("b9517783-f4cd-4c5b-043d-08db771ab7f4"), 30m, new DateTime(1898, 6, 26, 10, 57, 31, 172, DateTimeKind.Unspecified).AddTicks(8595), "The War of the Worlds" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AgeRestriction", "Author", "Description", "GenreId", "ImageUrl", "IsActive", "ManagerId", "Price", "ReleaseDate", "Title" },
                values: new object[] { new Guid("98fe9b08-3139-48c1-a87a-b7ba6fec81e5"), 12, "Leo Tolstoy", "Acclaimed by many as the world's greatest novel, Anna Karenina provides a vast panorama of contemporary life in Russia and of humanity in general.", 1, "https://www.goodreads.com/book/show/15823480-anna-karenina", true, new Guid("b9517783-f4cd-4c5b-043d-08db771ab7f4"), 20m, new DateTime(1877, 6, 26, 10, 57, 31, 172, DateTimeKind.Unspecified).AddTicks(8595), "Anna Karenina" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("71694f32-dd73-4399-a0a8-bc718f2aadcb"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("98fe9b08-3139-48c1-a87a-b7ba6fec81e5"));

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AgeRestriction", "Author", "Description", "GenreId", "ImageUrl", "IsActive", "ManagerId", "NumberOfSales", "Price", "ReleaseDate", "Title" },
                values: new object[] { new Guid("1bd0621e-7d16-4382-bd9e-75dfa71b5754"), 16, "H.G. Wells", "The War of the Worlds by H.G. Wells is about a fictional invasion of Southern England by Martians. The military is powerless against the Martians' superior weapons, and many people die. The Martians are eventually killed by bacterial infection.", 5, "https://www.gutenberg.org/files/36/36-h/36-h.htm", true, new Guid("b9517783-f4cd-4c5b-043d-08db771ab7f4"), 0, 30m, new DateTime(1898, 6, 26, 10, 57, 31, 172, DateTimeKind.Unspecified).AddTicks(8595), "The War of the Worlds" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AgeRestriction", "Author", "Description", "GenreId", "ImageUrl", "IsActive", "ManagerId", "NumberOfSales", "Price", "ReleaseDate", "Title" },
                values: new object[] { new Guid("d4756aab-b1f5-4b72-8343-6379f7f73b04"), 12, "Leo Tolstoy", "Acclaimed by many as the world's greatest novel, Anna Karenina provides a vast panorama of contemporary life in Russia and of humanity in general.", 1, "https://www.goodreads.com/book/show/15823480-anna-karenina", true, new Guid("b9517783-f4cd-4c5b-043d-08db771ab7f4"), 0, 20m, new DateTime(1877, 6, 26, 10, 57, 31, 172, DateTimeKind.Unspecified).AddTicks(8595), "Anna Karenina" });
        }
    }
}
