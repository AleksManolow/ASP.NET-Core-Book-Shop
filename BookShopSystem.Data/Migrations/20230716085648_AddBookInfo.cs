using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookShopSystem.Data.Migrations
{
    public partial class AddBookInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AgeRestriction", "Author", "Description", "GenreId", "ImageUrn", "IsActive", "ManagerId", "Price", "ReleaseDate", "Title" },
                values: new object[] { new Guid("3f83c7c1-b65a-4253-8700-c7bd2927f51b"), 12, "Leo Tolstoy", "Acclaimed by many as the world's greatest novel, Anna Karenina provides a vast panorama of contemporary life in Russia and of humanity in general.", 1, "https://www.goodreads.com/book/show/15823480-anna-karenina", true, new Guid("b9517783-f4cd-4c5b-043d-08db771ab7f4"), 20m, new DateTime(1877, 6, 26, 10, 57, 31, 172, DateTimeKind.Unspecified).AddTicks(8595), "Anna Karenina" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AgeRestriction", "Author", "Description", "GenreId", "ImageUrn", "IsActive", "ManagerId", "Price", "ReleaseDate", "Title" },
                values: new object[] { new Guid("ad92a723-5317-450b-bb09-429957388125"), 16, "H.G. Wells", "The War of the Worlds by H.G. Wells is about a fictional invasion of Southern England by Martians. The military is powerless against the Martians' superior weapons, and many people die. The Martians are eventually killed by bacterial infection.", 5, "https://www.gutenberg.org/files/36/36-h/36-h.htm", true, new Guid("b9517783-f4cd-4c5b-043d-08db771ab7f4"), 30m, new DateTime(1898, 6, 26, 10, 57, 31, 172, DateTimeKind.Unspecified).AddTicks(8595), "The War of the Worlds" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("3f83c7c1-b65a-4253-8700-c7bd2927f51b"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("ad92a723-5317-450b-bb09-429957388125"));
        }
    }
}
