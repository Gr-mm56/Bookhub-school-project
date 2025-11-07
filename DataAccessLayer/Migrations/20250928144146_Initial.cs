using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    ISBN = table.Column<string>(type: "TEXT", maxLength: 17, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 300, nullable: true),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    AuthorId = table.Column<int>(type: "INTEGER", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Stars = table.Column<int>(type: "INTEGER", nullable: false),
                    BookId = table.Column<int>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookGenre",
                columns: table => new
                {
                    BooksId = table.Column<int>(type: "INTEGER", nullable: false),
                    GenresId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookGenre", x => new { x.BooksId, x.GenresId });
                    table.ForeignKey(
                        name: "FK_BookGenre_Books_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookGenre_Genres_GenresId",
                        column: x => x.GenresId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "Created", "Description", "ISBN", "Price", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 9, 28, 16, 41, 45, 478, DateTimeKind.Local).AddTicks(9530), "The first volume in J.R.R. Tolkien's epic adventure, starting the journey to destroy the One Ring.", "978-0618260243", 12.99m, "The Fellowship of the Ring" },
                    { 2, 1, new DateTime(2025, 9, 28, 16, 41, 45, 479, DateTimeKind.Local).AddTicks(1333), "The second volume of the trilogy, where the fellowship is scattered and the war for Middle-earth escalates.", "978-0618260281", 14.50m, "The Two Towers" },
                    { 3, 1, new DateTime(2025, 9, 28, 16, 41, 45, 479, DateTimeKind.Local).AddTicks(1353), "The final volume, chronicling the final destruction of the Ring and the ultimate fate of Middle-earth.", "978-0618260304", 15.99m, "The Return of the King" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Created", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 9, 28, 16, 41, 45, 474, DateTimeKind.Local).AddTicks(7649), "Science Fiction" },
                    { 2, new DateTime(2025, 9, 28, 16, 41, 45, 477, DateTimeKind.Local).AddTicks(8729), "Fantasy" },
                    { 3, new DateTime(2025, 9, 28, 16, 41, 45, 477, DateTimeKind.Local).AddTicks(8762), "Mystery" },
                    { 4, new DateTime(2025, 9, 28, 16, 41, 45, 477, DateTimeKind.Local).AddTicks(8766), "Thriller" },
                    { 5, new DateTime(2025, 9, 28, 16, 41, 45, 477, DateTimeKind.Local).AddTicks(8769), "Romance" },
                    { 6, new DateTime(2025, 9, 28, 16, 41, 45, 477, DateTimeKind.Local).AddTicks(8772), "Historical Fiction" },
                    { 7, new DateTime(2025, 9, 28, 16, 41, 45, 477, DateTimeKind.Local).AddTicks(8774), "Horror" },
                    { 8, new DateTime(2025, 9, 28, 16, 41, 45, 477, DateTimeKind.Local).AddTicks(8777), "Biography" },
                    { 9, new DateTime(2025, 9, 28, 16, 41, 45, 477, DateTimeKind.Local).AddTicks(8779), "Self-Help" },
                    { 10, new DateTime(2025, 9, 28, 16, 41, 45, 477, DateTimeKind.Local).AddTicks(8782), "Poetry" },
                    { 11, new DateTime(2025, 9, 28, 16, 41, 45, 477, DateTimeKind.Local).AddTicks(8784), "Young Adult" },
                    { 12, new DateTime(2025, 9, 28, 16, 41, 45, 477, DateTimeKind.Local).AddTicks(8787), "Children's" },
                    { 13, new DateTime(2025, 9, 28, 16, 41, 45, 477, DateTimeKind.Local).AddTicks(8789), "Adventure" },
                    { 14, new DateTime(2025, 9, 28, 16, 41, 45, 477, DateTimeKind.Local).AddTicks(8792), "Action" },
                    { 15, new DateTime(2025, 9, 28, 16, 41, 45, 477, DateTimeKind.Local).AddTicks(8795), "Literary Fiction" }
                });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "BookId", "Created", "DateCreated", "DateModified", "Stars" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 9, 28, 16, 41, 45, 479, DateTimeKind.Local).AddTicks(5128), new DateTime(2025, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 2, 2, new DateTime(2025, 9, 28, 16, 41, 45, 479, DateTimeKind.Local).AddTicks(6701), new DateTime(2025, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookGenre_GenresId",
                table: "BookGenre",
                column: "GenresId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_BookId",
                table: "Ratings",
                column: "BookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookGenre");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
