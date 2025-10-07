using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class RemainingEntitiesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Books",
                newName: "ImageId");

            migrationBuilder.AddColumn<int>(
                name: "ProfilePhotoId",
                table: "Users",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FileUrl = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Surname = table.Column<string>(type: "TEXT", nullable: false),
                    ProfilePhotoId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Authors_Images_ProfilePhotoId",
                        column: x => x.ProfilePhotoId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    ProfilePhotoId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Publishers_Images_ProfilePhotoId",
                        column: x => x.ProfilePhotoId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rel_Book_Author",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "INTEGER", nullable: false),
                    BookId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rel_Book_Author", x => new { x.AuthorId, x.BookId });
                    table.ForeignKey(
                        name: "FK_Rel_Book_Author_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rel_Book_Author_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookPublisher",
                columns: table => new
                {
                    BooksId = table.Column<int>(type: "INTEGER", nullable: false),
                    PublishersId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookPublisher", x => new { x.BooksId, x.PublishersId });
                    table.ForeignKey(
                        name: "FK_BookPublisher_Books_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookPublisher_Publishers_PublishersId",
                        column: x => x.PublishersId,
                        principalTable: "Publishers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageId",
                value: 7);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageId",
                value: 8);

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "CreatedAt", "FileUrl", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "assets/users/john_doe.jpg", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "assets/users/jane_smith.jpg", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "assets/users/taro_yamada.jpg", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "assets/users/anna_kowalska.jpg", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "assets/users/peter_novak.jpg", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "assets/books/fellowship_of_the_ring.jpg", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "assets/books/two_towers.jpg", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "assets/books/return_of_the_king.jpg", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "assets/authors/tolkien.jpg", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "assets/authors/rowling.jpg", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "assets/publishers/harpercollins.jpg", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "assets/publishers/penguin.jpg", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "ProfilePhotoId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "ProfilePhotoId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "ProfilePhotoId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "ProfilePhotoId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                column: "ProfilePhotoId",
                value: 5);

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "CreatedAt", "Name", "ProfilePhotoId", "Surname", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "J.R.R.", 9, "Tolkien", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "J.K.", 10, "Rowling", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "Address", "CreatedAt", "Name", "ProfilePhotoId", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "195 Broadway, New York, NY 10007, USA", new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "HarperCollins", 11, new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "1745 Broadway, New York, NY 10019, USA", new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Penguin Random House", 12, new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Rel_Book_Author",
                columns: new[] { "AuthorId", "BookId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 2, 1 },
                    { 2, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProfilePhotoId",
                table: "Users",
                column: "ProfilePhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_ImageId",
                table: "Books",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_ProfilePhotoId",
                table: "Authors",
                column: "ProfilePhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_BookPublisher_PublishersId",
                table: "BookPublisher",
                column: "PublishersId");

            migrationBuilder.CreateIndex(
                name: "IX_Publishers_ProfilePhotoId",
                table: "Publishers",
                column: "ProfilePhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_Rel_Book_Author_BookId",
                table: "Rel_Book_Author",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Images_ImageId",
                table: "Books",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Images_ProfilePhotoId",
                table: "Users",
                column: "ProfilePhotoId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Images_ImageId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Images_ProfilePhotoId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "BookPublisher");

            migrationBuilder.DropTable(
                name: "Rel_Book_Author");

            migrationBuilder.DropTable(
                name: "Publishers");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Users_ProfilePhotoId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Books_ImageId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ProfilePhotoId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "Books",
                newName: "AuthorId");

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "AuthorId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "AuthorId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "AuthorId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageId",
                value: 5);
        }
    }
}
