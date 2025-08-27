using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProvaPub.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Numbers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Number = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Numbers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Value = table.Column<decimal>(type: "TEXT", nullable: false),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Chris Lueilwitz" },
                    { 2, "Dora Olson" },
                    { 3, "Frederick Mueller" },
                    { 4, "Mathew Howe" },
                    { 5, "Lynn Hills" },
                    { 6, "Gordon Hammes" },
                    { 7, "Alice Dibbert" },
                    { 8, "Ernest Bode" },
                    { 9, "Jim Lehner" },
                    { 10, "Paul Legros" },
                    { 11, "Ian Harris" },
                    { 12, "Linda Nitzsche" },
                    { 13, "Laurence Reilly" },
                    { 14, "Erik Hirthe" },
                    { 15, "Chelsea Emmerich" },
                    { 16, "Jennie Swift" },
                    { 17, "Jackie Fay" },
                    { 18, "Noel Aufderhar" },
                    { 19, "Lamar Crist" },
                    { 20, "Gerald Langosh" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Small Steel Pizza" },
                    { 2, "Rustic Wooden Towels" },
                    { 3, "Rustic Fresh Shirt" },
                    { 4, "Rustic Rubber Towels" },
                    { 5, "Refined Concrete Towels" },
                    { 6, "Refined Steel Fish" },
                    { 7, "Rustic Concrete Shoes" },
                    { 8, "Gorgeous Soft Salad" },
                    { 9, "Rustic Wooden Bike" },
                    { 10, "Sleek Concrete Sausages" },
                    { 11, "Unbranded Fresh Keyboard" },
                    { 12, "Handmade Frozen Ball" },
                    { 13, "Fantastic Soft Sausages" },
                    { 14, "Ergonomic Rubber Bacon" },
                    { 15, "Rustic Granite Chicken" },
                    { 16, "Unbranded Concrete Towels" },
                    { 17, "Awesome Frozen Shoes" },
                    { 18, "Licensed Frozen Pants" },
                    { 19, "Practical Wooden Bike" },
                    { 20, "Ergonomic Concrete Chair" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Numbers_Number",
                table: "Numbers",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Numbers");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
