using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class MilestoneMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    EntityName = table.Column<string>(type: "TEXT", nullable: false),
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false),
                    Action = table.Column<string>(type: "TEXT", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModificationDetails = table.Column<string>(type: "TEXT", nullable: false),
                    EditCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Surname = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    ProfilePhotoId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    ISBN = table.Column<string>(type: "TEXT", maxLength: 17, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 300, nullable: true),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    PublisherId = table.Column<int>(type: "INTEGER", nullable: true),
                    ImageId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RelBookAuthor",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "INTEGER", nullable: false),
                    BookId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelBookAuthor", x => new { x.AuthorId, x.BookId });
                    table.ForeignKey(
                        name: "FK_RelBookAuthor_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RelBookAuthor_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RelBookGenre",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "INTEGER", nullable: false),
                    BookId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelBookGenre", x => new { x.BookId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_RelBookGenre_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RelBookGenre_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalValue = table.Column<double>(type: "REAL", nullable: false),
                    OrderId = table.Column<int>(type: "INTEGER", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BookId = table.Column<int>(type: "INTEGER", nullable: false),
                    CartId = table.Column<int>(type: "INTEGER", nullable: false),
                    Count = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseItems_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FileUrl = table.Column<string>(type: "TEXT", nullable: false),
                    PublisherId = table.Column<int>(type: "INTEGER", nullable: true),
                    AuthorId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    ProfilePhotoId = table.Column<int>(type: "INTEGER", nullable: true),
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
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    Surname = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    Country = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    City = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    Street = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    ProfilePhotoId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Images_ProfilePhotoId",
                        column: x => x.ProfilePhotoId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Stars = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    BookId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ratings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WishlistItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    BookId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishlistItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WishlistItems_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishlistItems_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Literary Fiction", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Biography", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Children's", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Young Adult", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Adventure", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fantasy", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Thriller", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Science Fiction", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Romance", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mystery", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Action", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Poetry", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Historical Fiction", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Horror", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Self-Help", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "AuthorId", "CreatedAt", "FileUrl", "PublisherId", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "assets/users/john_doe.jpg", null, new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, null, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "assets/users/jane_smith.jpg", null, new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, null, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "assets/users/taro_yamada.jpg", null, new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, null, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "assets/users/anna_kowalska.jpg", null, new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, null, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "assets/users/peter_novak.jpg", null, new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, null, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "assets/books/fellowship_of_the_ring.jpg", null, new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, null, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "assets/books/two_towers.jpg", null, new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, null, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "assets/books/return_of_the_king.jpg", null, new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, null, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "assets/authors/tolkien.jpg", null, new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, null, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "assets/authors/rowling.jpg", null, new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, null, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "assets/publishers/harpercollins.jpg", null, new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, null, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "assets/publishers/penguin.jpg", null, new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "City", "Country", "CreatedAt", "Name", "ProfilePhotoId", "Street", "Surname", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Olafberg", "Reunion", new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wellington", null, "VonRueden Rapid", "Kuphal", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Diegofurt", "Denmark", new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Florence", null, "Brakus Grove", "Murray", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Framiburgh", "Kiribati", new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Stacey", null, "Corkery Lodge", "Buckridge", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Roryberg", "Saint Helena", new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dannie", null, "Emma Bridge", "Kihn", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "Lake Emiliostad", "Nauru", new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rebeca", null, "Alison Mountain", "Waters", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "Lake Vincenzoton", "New Caledonia", new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Angel", null, "Berge Causeway", "Bradtke", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "Orlandoburgh", "Andorra", new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dennis", null, "Torrance Row", "King", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "CreatedAt", "Name", "ProfilePhotoId", "Surname", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wellington", 6, "Kuphal", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Malcolm", 2, "Russel", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kadin", 5, "VonRueden", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Florence", 2, "Murray", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Melyna", 5, "Gleason", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Carts",
                columns: new[] { "Id", "CreatedAt", "OrderDate", "OrderId", "TotalValue", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 2, 17, 16, 51, 5, 965, DateTimeKind.Unspecified).AddTicks(8834), 1729, 292.50999999999999, new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 26, 8, 45, 7, 378, DateTimeKind.Unspecified).AddTicks(9460), 1535, 231.18000000000001, new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 3, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 25, 10, 39, 3, 278, DateTimeKind.Unspecified).AddTicks(2620), 1631, 204.75999999999999, new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 4, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 4, 2, 20, 45, 33, 659, DateTimeKind.Unspecified).AddTicks(5317), 1674, 212.91, new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 5, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 12, 4, 38, 58, 477, DateTimeKind.Unspecified).AddTicks(5940), 1898, 30.940000000000001, new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 6, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 138.91999999999999, new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 6 },
                    { 7, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 31, 21, 33, 41, 734, DateTimeKind.Unspecified).AddTicks(7149), 1644, 73.489999999999995, new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 7 }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "Address", "CreatedAt", "Name", "ProfilePhotoId", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "36272 VonRueden Rapid, Murphyton, French Southern Territories", new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kuphal, Rempel and O'Reilly", 4, new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "24768 Corkery Lodge, South Emmatown, Solomon Islands", new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kohler Group", 4, new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "0161 Alison Mountain, Windlerton, South Georgia and the South Sandwich Islands", new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bradtke - Sauer", 1, new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "CreatedAt", "Description", "ISBN", "ImageId", "Price", "PublisherId", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Et minima facilis ratione praesentium tempora dignissimos placeat et voluptas. Iusto voluptatem sit illum. Et aut dolor voluptatem harum architecto eaque provident veritatis aspernatur. Quia dolorem sapiente dicta eum veritatis illo magnam.", "666-1-6362726-71-3", 2, 45.380000000000003, 1, "Dolores qui nam assumenda labore sed sint.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fugit dicta ea deserunt. Et nam facere voluptate quis. Voluptas quis voluptates doloribus quia.", "666-1-7929691-67-3", 2, 11.52, 2, "Omnis nemo voluptatum recusandae qui.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aut veritatis aperiam deserunt. Soluta saepe unde voluptatem cumque rem eos corporis deserunt dolorem. Culpa ullam quia et consequatur ut reprehenderit laudantium voluptates blanditiis. Soluta neque consequatur qui et omnis. Est minima eius earum rerum rem dicta quo.", "666-1-2622497-02-9", 2, 29.260000000000002, 2, "Nisi magnam provident repellendus sequi reiciendis.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Similique aspernatur libero ut perspiciatis. Rerum velit minus provident exercitationem officiis qui facilis enim. Voluptas molestiae hic cum non eligendi nam et rerum. Est quaerat odio nostrum debitis voluptas dolores.", "666-1-2202765-64-1", 2, 14.43, 1, "Quidem et quia aliquam numquam voluptas.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Officiis iusto labore. Recusandae consequuntur vel aut odio expedita delectus quod et reprehenderit. Saepe illo porro molestias doloribus eum numquam aut. Beatae omnis quisquam pariatur fugiat iusto explicabo. Iste veniam vel aut nihil ipsam est ipsam nemo at.", "666-1-8459889-46-8", 1, 40.229999999999997, 2, "Rerum enim impedit odio nobis.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sit eius perferendis. Consectetur quod earum consectetur nobis non quia consectetur. Tenetur velit amet quia quia quidem accusantium. Ut fuga quisquam assumenda doloribus iusto animi et omnis porro. Nisi eos earum.", "666-1-9566378-14-5", 1, 11.26, 2, "Consequuntur ut provident tempore quo et et.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aut numquam mollitia nostrum eos velit corporis. Veniam voluptates vel eos nulla. Aut quis nam magni animi in ut incidunt. Et labore et.", "666-1-1791411-97-2", 4, 21.02, 2, "Occaecati veritatis.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Iste tempora esse temporibus quidem aut. Aspernatur illo incidunt suscipit. Et mollitia quos fugit laudantium eaque ut veniam facere. Rerum minima illum sed omnis architecto. Saepe suscipit laudantium mollitia. Magni adipisci veniam aut quia.", "666-1-5213774-93-3", 4, 18.25, 2, "Est quam optio tempore consequatur facere qui.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aut architecto voluptates neque eveniet officiis adipisci. Laudantium exercitationem fugiat culpa. Aspernatur nihil voluptatem ut asperiores nostrum pariatur fugiat molestiae. Doloribus aut beatae aperiam sapiente rerum sit. Ratione voluptates et est quas cupiditate aut inventore fugit est. Iure sed commodi ullam.", "666-1-2851824-67-1", 2, 17.469999999999999, 1, "Ipsum tenetur maiores ut.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Autem quaerat itaque quia. Eligendi ut quis. Quo est optio aperiam ut dolorem quidem. Temporibus ut mollitia consequatur id.", "666-1-4918824-43-6", 4, 35.149999999999999, 1, "Ea pariatur veniam nulla quia nobis quia.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ullam odio magni qui. Voluptatem veniam maxime vel. Labore qui consectetur consectetur ea cumque eveniet provident quae animi. Earum vitae molestias est ullam fuga in. Harum maxime reiciendis a debitis.\n\nAut dolores ut eum expedita vitae. Placeat itaque veniam ut nihil. Rerum assumenda sint hic. Qui ex ea.", "666-1-8661076-53-6", 3, 7.0999999999999996, 1, "Perspiciatis facilis itaque.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sed iure est eos tempora doloremque et exercitationem hic. Quos nobis qui. Hic et omnis laborum cum sint tempora ut. Illo aut sit quis.\n\nQuo accusantium ab. Eos iusto quia. Rerum voluptates incidunt nulla sit recusandae recusandae aspernatur beatae.", "666-1-3895716-94-6", 3, 19.989999999999998, 2, "Quidem impedit qui placeat eos voluptatem assumenda.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Eum libero cumque ducimus libero quia id voluptatem. Non animi autem explicabo quis dolorum nemo sequi nesciunt id. Dolores sed doloremque perferendis corrupti perferendis nulla optio et. Dignissimos quam animi quis alias nihil sit dignissimos.", "666-1-1588781-20-4", 1, 33.649999999999999, 1, "Fugiat sunt quisquam saepe necessitatibus provident distinctio.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Est a expedita ea et ea exercitationem corrupti. Est est veniam velit. Quo itaque minima porro numquam. Aut porro quia veniam a vitae sit. Sapiente quo et debitis autem.", "666-1-9237867-24-7", 4, 29.140000000000001, 1, "Et facilis.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sed ut quo. Ea dolorem quia molestiae cupiditate. Impedit pariatur et esse tenetur tempora. Ipsam error est non nam.\n\nAd ex distinctio placeat quibusdam quam veniam. Animi nemo et commodi. Consectetur error quia reprehenderit quam repudiandae expedita aut. Optio alias deleniti aliquid. Fugit aliquam et vero.", "666-1-3415064-45-5", 4, 24.300000000000001, 1, "Error delectus voluptatem error.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 16, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Temporibus placeat rerum est fugit. Distinctio ut dignissimos magnam ipsum maiores ut qui. Asperiores nisi sunt sint nesciunt.\n\nConsequatur voluptatum esse voluptates aspernatur. Necessitatibus est maiores commodi sit quis et consequuntur nihil. Maiores iure quas nesciunt nobis pariatur aut voluptatum. Ratione et animi incidunt explicabo. Beatae nostrum et itaque. Incidunt sed natus fugiat vel tempore.", "666-1-4111425-15-5", 2, 13.23, 2, "Aspernatur quae neque.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 17, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vitae similique amet tenetur molestiae repellendus dolorem tempore dolor perferendis. Quia molestias nobis voluptas iste aperiam perspiciatis. Odit nihil deleniti saepe reiciendis consequatur nostrum similique laborum et.\n\nAutem cum aperiam sint. Illo laborum quaerat voluptatem corporis maiores doloremque deserunt autem. Et hic sit rem aliquam.", "666-1-6869496-90-2", 4, 29.469999999999999, 2, "Mollitia eum et similique est aut magni.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 18, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Et quam veniam tempore et iusto qui ullam animi placeat. Dolorum molestiae sit fugiat consequuntur rem et sint repudiandae. Nulla nam consequuntur aut.", "666-1-6291228-68-2", 1, 28.780000000000001, 1, "Sint animi.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 19, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Temporibus sed vero possimus id debitis a. Qui expedita possimus nesciunt similique. Voluptatem fugit deserunt iusto sit eveniet voluptas adipisci. Repellat velit labore et iusto.", "666-1-7298722-01-3", 4, 32.460000000000001, 2, "Nihil dolorem qui a.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 20, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Omnis aperiam temporibus quia sit vel placeat. Quia eos ut et ducimus occaecati corrupti. Nihil exercitationem enim. Quam id harum. Consequatur neque saepe voluptatem repellendus quae sint vero.\n\nCum minus delectus inventore minima totam omnis nihil voluptate et. Non voluptate atque aperiam nisi saepe dolor. Id dicta accusantium in. Ipsum sit atque et. Consequuntur earum quos et cupiditate nulla possimus aspernatur commodi.", "666-1-2549480-54-3", 1, 8.4800000000000004, 2, "Laudantium nulla excepturi.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 21, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Necessitatibus quaerat voluptatibus sit eveniet minima tempore aut consequatur. Rerum ad et non voluptas in nemo quis eligendi eum. Sunt voluptatem architecto. Commodi ut aut debitis.\n\nAdipisci molestiae ad rem possimus eos ut illum. Nisi recusandae architecto. Assumenda consequatur velit et autem non nam laudantium a. Est quibusdam repellat voluptates sit sed nemo accusamus incidunt illo. Ducimus pariatur delectus illo. Sed laboriosam iste.", "666-1-0074241-16-9", 3, 42.659999999999997, 1, "Quo sit sit.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 22, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Repellendus veniam maxime voluptatum cumque sit tempore quia nesciunt incidunt. Quis cum voluptas voluptates voluptatem voluptates natus. Id id ipsa consequatur delectus odio error assumenda reiciendis. Quis esse vel et. Omnis enim saepe error laudantium alias quisquam. Voluptatibus recusandae assumenda inventore doloribus a enim sint similique doloribus.\n\nAliquid quod repudiandae dolor. Eos magnam eligendi. Repellendus vel adipisci. Explicabo omnis quidem nihil. Aspernatur omnis rerum fuga veritatis. Adipisci vel sunt vel nam exercitationem.", "666-1-3113462-22-4", 4, 29.789999999999999, 1, "Sit reprehenderit.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 23, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Modi saepe sed enim est soluta. Officia et error iusto accusantium illum. Iure quo quae eos delectus minima enim illum est accusantium. Facere et ipsam et qui cum harum doloribus et. Sed commodi vel quas magni aut cupiditate.", "666-1-1537081-15-9", 4, 45.359999999999999, 2, "Dicta incidunt consequatur ea.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 24, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ut sed necessitatibus aut. Sit ea at. Incidunt ea quo qui repellendus. Perferendis quo et cumque et culpa facere error rerum quam. Et quia quam enim ea provident voluptatem. Iste tempore accusamus excepturi sapiente eaque consequatur aut.\n\nCum vel alias in praesentium aut vel unde. Nesciunt qui numquam quidem dolore. Dignissimos ea tempora iusto architecto eum. Occaecati soluta incidunt enim ratione sed ut nam nulla. Labore repudiandae delectus ipsa deserunt numquam quo ipsam.", "666-1-7880231-41-0", 4, 23.469999999999999, 2, "Repellat illo non.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 25, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tenetur quia facilis id. Rerum reiciendis est minima nisi. Amet sint sed sit aut natus ea omnis necessitatibus.", "666-1-0615654-48-8", 1, 43.68, 2, "Nesciunt magni laboriosam.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "PurchaseItems",
                columns: new[] { "Id", "BookId", "CartId", "Count", "CreatedAt", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 25, 3, 5, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 13, 4, 3, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 19, 3, 4, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 17, 6, 4, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 20, 3, 4, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 7, 4, 2, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 14, 6, 3, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 24, 1, 5, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, 18, 3, 4, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, 10, 6, 2, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "BookId", "CreatedAt", "Stars", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, 25, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 2, 13, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 3, 19, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 4, 17, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 6 },
                    { 5, 20, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 6, 7, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 7, 14, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 6 },
                    { 8, 24, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 }
                });

            migrationBuilder.InsertData(
                table: "RelBookAuthor",
                columns: new[] { "AuthorId", "BookId" },
                values: new object[,]
                {
                    { 1, 7 },
                    { 1, 8 },
                    { 1, 10 },
                    { 1, 14 },
                    { 1, 18 },
                    { 1, 19 },
                    { 1, 20 },
                    { 1, 21 },
                    { 1, 22 },
                    { 2, 2 },
                    { 2, 9 },
                    { 2, 19 },
                    { 2, 25 },
                    { 3, 1 },
                    { 3, 7 },
                    { 3, 8 },
                    { 3, 9 },
                    { 3, 10 },
                    { 3, 11 },
                    { 3, 13 },
                    { 3, 23 },
                    { 3, 25 },
                    { 4, 1 },
                    { 4, 2 },
                    { 4, 3 },
                    { 4, 4 },
                    { 4, 5 },
                    { 4, 6 },
                    { 4, 12 },
                    { 4, 15 },
                    { 4, 16 },
                    { 4, 17 },
                    { 4, 24 },
                    { 5, 3 },
                    { 5, 12 },
                    { 5, 15 },
                    { 5, 20 },
                    { 5, 21 }
                });

            migrationBuilder.InsertData(
                table: "RelBookGenre",
                columns: new[] { "BookId", "GenreId" },
                values: new object[,]
                {
                    { 1, 8 },
                    { 1, 11 },
                    { 1, 12 },
                    { 2, 4 },
                    { 2, 9 },
                    { 2, 14 },
                    { 3, 1 },
                    { 3, 6 },
                    { 3, 10 },
                    { 4, 4 },
                    { 4, 11 },
                    { 4, 12 },
                    { 5, 5 },
                    { 6, 2 },
                    { 6, 8 },
                    { 6, 14 },
                    { 7, 5 },
                    { 8, 3 },
                    { 8, 8 },
                    { 9, 7 },
                    { 10, 6 },
                    { 10, 10 },
                    { 10, 13 },
                    { 11, 2 },
                    { 11, 5 },
                    { 12, 4 },
                    { 12, 10 },
                    { 12, 14 },
                    { 13, 3 },
                    { 13, 5 },
                    { 13, 10 },
                    { 14, 8 },
                    { 15, 2 },
                    { 16, 3 },
                    { 16, 13 },
                    { 17, 2 },
                    { 17, 3 },
                    { 17, 13 },
                    { 18, 4 },
                    { 19, 1 },
                    { 19, 10 },
                    { 20, 5 },
                    { 20, 8 },
                    { 20, 15 },
                    { 21, 6 },
                    { 21, 12 },
                    { 21, 15 },
                    { 22, 5 },
                    { 22, 10 },
                    { 22, 14 },
                    { 23, 4 },
                    { 23, 6 },
                    { 23, 12 },
                    { 24, 3 },
                    { 25, 2 }
                });

            migrationBuilder.InsertData(
                table: "WishlistItems",
                columns: new[] { "Id", "BookId", "CreatedAt", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, 21, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 7 },
                    { 2, 13, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 3, 4, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 6 },
                    { 4, 18, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 5, 11, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserId",
                table: "AspNetUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Authors_ProfilePhotoId",
                table: "Authors",
                column: "ProfilePhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_ImageId",
                table: "Books",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_PublisherId",
                table: "Books",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_AuthorId",
                table: "Images",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_PublisherId",
                table: "Images",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_Publishers_ProfilePhotoId",
                table: "Publishers",
                column: "ProfilePhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseItems_BookId",
                table: "PurchaseItems",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseItems_CartId",
                table: "PurchaseItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_BookId",
                table: "Ratings",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserId",
                table: "Ratings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RelBookAuthor_BookId",
                table: "RelBookAuthor",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_RelBookGenre_GenreId",
                table: "RelBookGenre",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProfilePhotoId",
                table: "Users",
                column: "ProfilePhotoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WishlistItems_BookId",
                table: "WishlistItems",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistItems_UserId",
                table: "WishlistItems",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Users_UserId",
                table: "AspNetUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Images_ProfilePhotoId",
                table: "Authors",
                column: "ProfilePhotoId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Images_ImageId",
                table: "Books",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Publishers_PublisherId",
                table: "Books",
                column: "PublisherId",
                principalTable: "Publishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Users_UserId",
                table: "Carts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Publishers_PublisherId",
                table: "Images",
                column: "PublisherId",
                principalTable: "Publishers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Images_ProfilePhotoId",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_Publishers_Images_ProfilePhotoId",
                table: "Publishers");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "PurchaseItems");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "RelBookAuthor");

            migrationBuilder.DropTable(
                name: "RelBookGenre");

            migrationBuilder.DropTable(
                name: "WishlistItems");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Publishers");
        }
    }
}
