using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class BookGenreRelationFixv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RelBookGenre_Books_BookId",
                table: "RelBookGenre");

            migrationBuilder.AddForeignKey(
                name: "FK_RelBookGenre_Books_BookId",
                table: "RelBookGenre",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RelBookGenre_Books_BookId",
                table: "RelBookGenre");

            migrationBuilder.AddForeignKey(
                name: "FK_RelBookGenre_Books_BookId",
                table: "RelBookGenre",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
