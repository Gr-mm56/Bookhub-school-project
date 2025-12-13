using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class PaymentStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentStatus",
                table: "Carts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "OrderDate", "OrderId", "PaymentStatus" },
                values: new object[] { null, null, 0 });

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PaymentStatus", "TotalValue" },
                values: new object[] { 1, 199.21000000000001 });

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "OrderDate", "OrderId", "PaymentStatus" },
                values: new object[] { null, null, 0 });

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "PaymentStatus", "TotalValue" },
                values: new object[] { 1, 70.040000000000006 });

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "OrderDate", "OrderId", "PaymentStatus" },
                values: new object[] { null, null, 0 });

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "PaymentStatus", "TotalValue" },
                values: new object[] { 0, 34.280000000000001 });

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "OrderDate", "OrderId", "PaymentStatus" },
                values: new object[] { null, null, 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "Carts");

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "OrderDate", "OrderId" },
                values: new object[] { new DateTime(2025, 2, 17, 16, 51, 5, 965, DateTimeKind.Unspecified).AddTicks(8834), 1729 });

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 2,
                column: "TotalValue",
                value: 231.18000000000001);

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "OrderDate", "OrderId" },
                values: new object[] { new DateTime(2024, 5, 25, 10, 39, 3, 278, DateTimeKind.Unspecified).AddTicks(2620), 1631 });

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 4,
                column: "TotalValue",
                value: 212.91);

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "OrderDate", "OrderId" },
                values: new object[] { new DateTime(2024, 3, 12, 4, 38, 58, 477, DateTimeKind.Unspecified).AddTicks(5940), 1898 });

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 6,
                column: "TotalValue",
                value: 138.91999999999999);

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "OrderDate", "OrderId" },
                values: new object[] { new DateTime(2024, 3, 31, 21, 33, 41, 734, DateTimeKind.Unspecified).AddTicks(7149), 1644 });
        }
    }
}
