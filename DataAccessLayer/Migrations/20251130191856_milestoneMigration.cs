using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class milestoneMigration : Migration
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
                    PrimaryGenreId = table.Column<int>(type: "INTEGER", nullable: false),
                    PublisherId = table.Column<int>(type: "INTEGER", nullable: true),
                    ImageId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Genres_PrimaryGenreId",
                        column: x => x.PrimaryGenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    { 1, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wellington", 10, "Kuphal", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Malcolm", 9, "Russel", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kadin", 10, "VonRueden", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Florence", 9, "Murray", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Melyna", 10, "Gleason", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) }
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
                    { 1, "36272 VonRueden Rapid, Murphyton, French Southern Territories", new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kuphal, Rempel and O'Reilly", 12, new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "24768 Corkery Lodge, South Emmatown, Solomon Islands", new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kohler Group", 12, new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "0161 Alison Mountain, Windlerton, South Georgia and the South Sandwich Islands", new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bradtke - Sauer", 11, new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "CreatedAt", "Description", "ISBN", "ImageId", "Price", "PrimaryGenreId", "PublisherId", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Et minima facilis ratione praesentium tempora dignissimos placeat et voluptas. Iusto voluptatem sit illum. Et aut dolor voluptatem harum architecto eaque provident veritatis aspernatur. Quia dolorem sapiente dicta eum veritatis illo magnam.", "666-1-6362726-71-3", 6, 45.380000000000003, 8, 2, "Dolores qui nam assumenda labore sed sint.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fugit dicta ea deserunt. Et nam facere voluptate quis. Voluptas quis voluptates doloribus quia.", "666-1-7929691-67-3", 8, 11.52, 4, 2, "Voluptatum recusandae qui.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Non aut veritatis aperiam deserunt ut soluta saepe. Voluptatem cumque rem eos corporis deserunt dolorem. Culpa ullam quia et consequatur ut reprehenderit laudantium voluptates blanditiis. Soluta neque consequatur qui et omnis.\n\nMinima eius earum rerum rem dicta quo reprehenderit. Illum quidem et quia aliquam numquam voluptas. Non explicabo sed quod. Sunt soluta ducimus sit non eum quaerat magnam. Aspernatur libero ut perspiciatis est rerum velit.", "666-1-9262249-70-2", null, 45.789999999999999, 3, 2, "Provident repellendus sequi.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Voluptatem magnam sed rerum. Impedit odio nobis dolorem deleniti. Et et pariatur molestiae voluptate id at. Iusto culpa eaque officiis iusto labore a recusandae consequuntur.\n\nOdio expedita delectus quod et reprehenderit minus saepe illo porro. Doloribus eum numquam aut cupiditate beatae omnis. Pariatur fugiat iusto explicabo saepe iste veniam vel. Nihil ipsam est.", "666-1-6266496-62-4", 6, 18.170000000000002, 1, 2, "Qui facilis enim voluptas voluptas molestiae hic.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Architecto sit eius perferendis harum consectetur quod. Consectetur nobis non quia consectetur officia tenetur velit amet quia. Quidem accusantium delectus.", "666-1-0956637-81-4", null, 28.890000000000001, 8, 2, "Itaque consequuntur ut provident tempore quo et.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Qui consequuntur molestiae omnis eius ex. Commodi sed aut. Mollitia nostrum eos velit.\n\nVeniam voluptates vel eos nulla. Aut quis nam magni animi in ut incidunt. Et labore et. Excepturi quo est quam optio tempore consequatur facere qui.", "666-1-0389150-50-1", 6, 38.109999999999999, 4, 1, "Doloribus iusto animi et omnis porro.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Temporibus et mollitia quos fugit. Eaque ut veniam facere odio rerum. Illum sed omnis architecto amet.", "666-1-4945247-68-1", null, 9.3499999999999996, 5, 1, "Repellendus qui doloribus enim ut exercitationem.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Et harum quisquam fugit minima. Ut occaecati aut architecto. Neque eveniet officiis adipisci eos laudantium exercitationem fugiat culpa et. Nihil voluptatem ut. Nostrum pariatur fugiat molestiae in doloribus aut beatae aperiam sapiente. Sit sint ratione voluptates et est quas cupiditate.", "666-1-1854199-92-8", 6, 27.530000000000001, 8, 1, "Commodi magni adipisci veniam aut.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sequi illum quo incidunt dolores ducimus vel nobis libero non. Velit autem quaerat itaque quia. Eligendi ut quis. Quo est optio aperiam ut dolorem quidem.", "666-1-3233948-28-1", 8, 35.75, 6, 2, "Iure sed.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Libero quae asperiores quidem magni. Odio magni qui velit voluptatem. Maxime vel aut labore qui. Consectetur ea cumque eveniet. Quae animi perspiciatis earum vitae molestias est.\n\nIn nostrum harum maxime reiciendis a debitis. Velit aut dolores ut eum. Vitae ullam placeat itaque veniam ut nihil consectetur. Assumenda sint hic eaque qui ex ea dolorum.", "666-1-2256986-61-0", 8, 37.960000000000001, 8, 2, "Id at.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sed iure est eos tempora doloremque et exercitationem hic. Quos nobis qui. Hic et omnis laborum cum sint tempora ut. Illo aut sit quis.\n\nQuo accusantium ab. Eos iusto quia. Rerum voluptates incidunt nulla sit recusandae recusandae aspernatur beatae.", "666-1-3895716-94-6", null, 19.989999999999998, 10, 2, "Placeat eos voluptatem assumenda.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Eum libero cumque ducimus libero quia id voluptatem. Non animi autem explicabo quis dolorum nemo sequi nesciunt id. Dolores sed doloremque perferendis corrupti perferendis nulla optio et. Dignissimos quam animi quis alias nihil sit dignissimos.", "666-1-1588781-20-4", 6, 33.649999999999999, 10, 1, "Sunt quisquam saepe necessitatibus provident distinctio.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ea exercitationem corrupti dolor est est veniam velit quis quo. Minima porro numquam natus aut porro quia veniam a vitae. Quis sapiente quo et. Autem dolor sit corrupti error delectus voluptatem error ut odio.\n\nConsequatur id quam iusto in ut molestiae. Minima quasi sed ut quo autem ea dolorem quia. Cupiditate laudantium impedit pariatur et esse tenetur tempora quis ipsam.", "666-1-6724754-66-6", 7, 48.149999999999999, 7, 1, "Molestiae exercitationem consequatur minus rerum.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Repudiandae expedita aut nesciunt optio alias. Aliquid dolorem fugit aliquam et vero. Quis nemo aspernatur quae neque nihil magni aut dolor. Dolore animi ipsum deserunt in sit.", "666-1-7422532-36-1", null, 30.34, 3, 1, "Provident ad ex distinctio placeat.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nostrum consequatur voluptatum esse voluptates aspernatur eum necessitatibus est maiores. Sit quis et consequuntur nihil. Maiores iure quas nesciunt nobis pariatur aut voluptatum.\n\nEt animi incidunt explicabo. Beatae nostrum et itaque. Incidunt sed natus fugiat vel tempore. Aut ut mollitia eum et.", "666-1-2421994-39-3", 8, 9.0600000000000005, 2, 1, "Placeat rerum est fugit distinctio distinctio.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 16, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dolorem tempore dolor perferendis qui quia molestias nobis voluptas. Aperiam perspiciatis doloribus odit nihil deleniti saepe. Consequatur nostrum similique laborum et sunt consequuntur autem cum aperiam. Vel illo laborum quaerat voluptatem corporis maiores.\n\nAutem quaerat et hic sit rem aliquam. Ut accusantium sint animi cum non hic dolores numquam numquam. Eligendi rerum non non rem ipsum tenetur et quam.", "666-1-6902581-90-5", 8, 13.48, 5, 2, "Consequatur est sapiente laudantium earum.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 17, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dicta sed laboriosam. Dolorem qui a omnis incidunt ut quo porro. Modi beatae ratione vel.\n\nMinima deserunt temporibus sed. Possimus id debitis a aliquam qui expedita possimus nesciunt. Porro voluptatem fugit deserunt iusto sit eveniet. Adipisci commodi repellat velit labore et iusto voluptas facere. Laudantium nulla excepturi exercitationem.", "666-1-6438146-59-1", 7, 43.009999999999998, 5, 2, "Ullam animi placeat vero.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 18, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Occaecati corrupti et nihil exercitationem enim. Quam id harum. Consequatur neque saepe voluptatem repellendus quae sint vero. Molestiae cum minus delectus inventore minima totam.", "666-1-5707113-75-1", null, 43.810000000000002, 5, 2, "Alias cupiditate velit ipsam beatae iste nam.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 19, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Consequuntur earum quos et cupiditate nulla possimus aspernatur commodi. Nulla ullam quo. Sit ipsa beatae possimus velit. Dignissimos sequi eos laborum aut.", "666-1-3872600-41-1", null, 5.8700000000000001, 4, 2, "Animi non voluptate atque aperiam.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 20, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aspernatur sunt voluptatem architecto ratione commodi. Aut debitis hic nobis adipisci molestiae ad rem possimus eos. Illum illo nisi recusandae architecto officiis assumenda consequatur velit et. Non nam laudantium a hic est. Repellat voluptates sit sed nemo accusamus incidunt illo velit.", "666-1-6108622-57-3", 8, 18.77, 1, 2, "Necessitatibus quaerat voluptatibus sit eveniet minima.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 21, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ut deleniti omnis cum. Tenetur repellendus veniam maxime voluptatum cumque sit tempore quia. Incidunt sunt quis cum. Voluptates voluptatem voluptates natus omnis.\n\nIpsa consequatur delectus odio error assumenda reiciendis. Quis esse vel et. Omnis enim saepe error laudantium alias quisquam. Voluptatibus recusandae assumenda inventore doloribus a enim sint similique doloribus. Nesciunt aliquid quod repudiandae dolor aperiam eos magnam eligendi aut.", "666-1-5601033-11-3", null, 24.32, 2, 2, "Sed laboriosam.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 22, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Exercitationem et molestiae atque dicta incidunt consequatur ea. Non quia facere. Accusamus adipisci quia. Non eveniet dolor cum dolores modi saepe. Enim est soluta. Officia et error iusto accusantium illum.\n\nQuo quae eos delectus minima enim. Est accusantium eos facere et ipsam et qui cum. Doloribus et culpa sed commodi vel quas magni. Cupiditate officiis possimus ipsam repellat illo non minus et rerum. Numquam in amet. Sed sunt velit aut vel dolorem.", "666-1-6430786-04-1", null, 22.149999999999999, 9, 1, "Explicabo omnis.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 23, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Quam deserunt et quia quam enim ea provident. Porro iste tempore accusamus excepturi. Eaque consequatur aut sint et cum vel alias in praesentium. Vel unde enim. Qui numquam quidem dolore.\n\nEa tempora iusto architecto eum dolorem. Soluta incidunt enim ratione sed ut nam. Cumque labore repudiandae delectus ipsa deserunt numquam quo ipsam. Vero voluptatem nesciunt magni laboriosam aperiam fuga consequuntur sint quidem.", "666-1-3837907-87-6", 7, 31.120000000000001, 9, 1, "Eaque sit ea at ut incidunt.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 24, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ea omnis necessitatibus labore occaecati architecto eum. Ut magnam quibusdam. Fugit sit enim. Cupiditate ea porro. Expedita nulla et ea. Nisi repellendus nam ducimus et.", "666-1-6389723-81-5", 6, 10.31, 3, 2, "Eos voluptatem ipsum consectetur tenetur quia facilis.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 25, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Omnis et excepturi nemo provident molestiae. Nostrum quisquam qui velit. Ut labore dolorem soluta quas omnis sunt molestiae et tenetur. Quibusdam laborum dolor. Dolor dolorum dicta corporis architecto sunt quibusdam dicta. Maiores praesentium placeat accusantium quaerat consequatur.\n\nPariatur impedit rerum qui nisi deleniti architecto eum quam eum. Consequatur dolorem porro eligendi aliquid magnam repellat libero harum. Consequuntur aliquid maxime voluptatem odio. Ipsa aut perspiciatis libero consequatur esse sunt hic. Ut minima fugiat qui perferendis et expedita quod hic minima.", "666-1-1250616-11-5", 6, 11.5, 5, 2, "Assumenda minima tenetur iusto.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) }
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
                name: "idx_author_name",
                table: "Authors",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "idx_author_surname",
                table: "Authors",
                column: "Surname");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_ProfilePhotoId",
                table: "Authors",
                column: "ProfilePhotoId");

            migrationBuilder.CreateIndex(
                name: "idx_book_price",
                table: "Books",
                column: "Price");

            migrationBuilder.CreateIndex(
                name: "idx_book_title",
                table: "Books",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_Books_ImageId",
                table: "Books",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_PrimaryGenreId",
                table: "Books",
                column: "PrimaryGenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_PublisherId",
                table: "Books",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "idx_genre_name",
                table: "Genres",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Images_AuthorId",
                table: "Images",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_PublisherId",
                table: "Images",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "idx_publisher_name",
                table: "Publishers",
                column: "Name");

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
                name: "Books");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Publishers");
        }
    }
}
