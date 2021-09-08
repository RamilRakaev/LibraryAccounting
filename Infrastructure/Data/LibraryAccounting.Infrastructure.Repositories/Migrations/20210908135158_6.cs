using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryAccounting.Infrastructure.Repositories.Migrations
{
    public partial class _6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "BookId", "BookingDate", "ClientId", "IsReturned", "IsTransmitted", "ReturnDate", "TransferDate" },
                values: new object[] { 1, 2, new DateTime(2021, 9, 8, 16, 51, 57, 687, DateTimeKind.Local).AddTicks(8597), 1, false, false, null, null });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Genre", "Publisher", "Title" },
                values: new object[,]
                {
                    { 1, "Некто", "Наука", "Москва", "История" },
                    { 2, "Некто", "Наука", "Москва", "Биология" },
                    { 3, "Некто", "Наука", "Питер", "Химия" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
