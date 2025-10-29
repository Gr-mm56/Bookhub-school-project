using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class UserImageCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Images_ProfilePhotoId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ProfilePhotoId",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "Images",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PublisherId",
                table: "Images",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AuthorId", "PublisherId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AuthorId", "PublisherId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AuthorId", "PublisherId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "AuthorId", "PublisherId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "AuthorId", "PublisherId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "AuthorId", "PublisherId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "AuthorId", "PublisherId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "AuthorId", "PublisherId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "AuthorId", "PublisherId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "AuthorId", "PublisherId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "AuthorId", "PublisherId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "AuthorId", "PublisherId" },
                values: new object[] { null, null });

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProfilePhotoId",
                table: "Users",
                column: "ProfilePhotoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_AuthorId",
                table: "Images",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_PublisherId",
                table: "Images",
                column: "PublisherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Authors_AuthorId",
                table: "Images",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Publishers_PublisherId",
                table: "Images",
                column: "PublisherId",
                principalTable: "Publishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Images_ProfilePhotoId",
                table: "Users",
                column: "ProfilePhotoId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Authors_AuthorId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Publishers_PublisherId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Images_ProfilePhotoId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ProfilePhotoId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Images_AuthorId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_PublisherId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "PublisherId",
                table: "Images");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProfilePhotoId",
                table: "Users",
                column: "ProfilePhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Images_ProfilePhotoId",
                table: "Users",
                column: "ProfilePhotoId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
