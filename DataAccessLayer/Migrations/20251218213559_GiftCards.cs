using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class GiftCards : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppliedGiftCardCouponId",
                table: "Carts",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GiftCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PriceReduction = table.Column<double>(type: "REAL", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiftCards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiftCardCoupons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    GiftCardId = table.Column<int>(type: "INTEGER", nullable: false),
                    UsedInOrderId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiftCardCoupons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GiftCardCoupons_Carts_UsedInOrderId",
                        column: x => x.UsedInOrderId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GiftCardCoupons_GiftCards_GiftCardId",
                        column: x => x.GiftCardId,
                        principalTable: "GiftCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 1,
                column: "AppliedGiftCardCouponId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 2,
                column: "AppliedGiftCardCouponId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 3,
                column: "AppliedGiftCardCouponId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 4,
                column: "AppliedGiftCardCouponId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 5,
                column: "AppliedGiftCardCouponId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 6,
                column: "AppliedGiftCardCouponId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 7,
                column: "AppliedGiftCardCouponId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Carts_AppliedGiftCardCouponId",
                table: "Carts",
                column: "AppliedGiftCardCouponId");

            migrationBuilder.CreateIndex(
                name: "idx_giftcardcoupon_code",
                table: "GiftCardCoupons",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GiftCardCoupons_GiftCardId",
                table: "GiftCardCoupons",
                column: "GiftCardId");

            migrationBuilder.CreateIndex(
                name: "IX_GiftCardCoupons_UsedInOrderId",
                table: "GiftCardCoupons",
                column: "UsedInOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_GiftCardCoupons_AppliedGiftCardCouponId",
                table: "Carts",
                column: "AppliedGiftCardCouponId",
                principalTable: "GiftCardCoupons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_GiftCardCoupons_AppliedGiftCardCouponId",
                table: "Carts");

            migrationBuilder.DropTable(
                name: "GiftCardCoupons");

            migrationBuilder.DropTable(
                name: "GiftCards");

            migrationBuilder.DropIndex(
                name: "IX_Carts_AppliedGiftCardCouponId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "AppliedGiftCardCouponId",
                table: "Carts");
        }
    }
}
