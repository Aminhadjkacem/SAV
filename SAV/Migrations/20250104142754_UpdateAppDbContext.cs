using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SAV.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAppDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PiecesRechange",
                columns: new[] { "Id", "ApplicationUserId", "Name", "Price" },
                values: new object[,]
                {
                    { 3, null, "Battery", 49.99m },
                    { 4, null, "Screen", 129.99m },
                    { 5, null, "Hard Drive", 79.99m },
                    { 6, null, "RAM Module", 59.99m }
                });

            migrationBuilder.InsertData(
                table: "Techniciens",
                columns: new[] { "Id", "Name", "Phone" },
                values: new object[,]
                {
                    { 3, "John Doe", "123-456-7890" },
                    { 4, "Jane Smith", "987-654-3210" },
                    { 5, "Alex Johnson", "555-123-4567" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PiecesRechange",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PiecesRechange",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PiecesRechange",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "PiecesRechange",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Techniciens",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Techniciens",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Techniciens",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
