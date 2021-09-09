using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryAccounting.Infrastructure.Repositories.Migrations
{
    public partial class _11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "BookingDate",
                value: new DateTime(2021, 9, 9, 17, 33, 40, 223, DateTimeKind.Local).AddTicks(3047));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "BookingDate",
                value: new DateTime(2021, 9, 9, 17, 28, 53, 406, DateTimeKind.Local).AddTicks(4417));
        }
    }
}
