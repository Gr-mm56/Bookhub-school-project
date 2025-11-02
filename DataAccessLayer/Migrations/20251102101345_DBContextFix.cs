using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class DBContextFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Images_ProfilePhotoId",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Authors_AuthorId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_AuthorId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Authors_ProfilePhotoId",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Images");

            migrationBuilder.AlterColumn<int>(
                name: "ProfilePhotoId",
                table: "Publishers",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "ProfilePhotoId",
                table: "Authors",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_ProfilePhotoId",
                table: "Authors",
                column: "ProfilePhotoId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Images_ProfilePhotoId",
                table: "Authors",
                column: "ProfilePhotoId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Images_ProfilePhotoId",
                table: "Authors");

            migrationBuilder.DropIndex(
                name: "IX_Authors_ProfilePhotoId",
                table: "Authors");

            migrationBuilder.AlterColumn<int>(
                name: "ProfilePhotoId",
                table: "Publishers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "Images",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProfilePhotoId",
                table: "Authors",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 1,
                column: "AuthorId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 2,
                column: "AuthorId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 3,
                column: "AuthorId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 4,
                column: "AuthorId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 5,
                column: "AuthorId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 6,
                column: "AuthorId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 7,
                column: "AuthorId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 8,
                column: "AuthorId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 9,
                column: "AuthorId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 10,
                column: "AuthorId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 11,
                column: "AuthorId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 12,
                column: "AuthorId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Images_AuthorId",
                table: "Images",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_ProfilePhotoId",
                table: "Authors",
                column: "ProfilePhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Images_ProfilePhotoId",
                table: "Authors",
                column: "ProfilePhotoId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Authors_AuthorId",
                table: "Images",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
