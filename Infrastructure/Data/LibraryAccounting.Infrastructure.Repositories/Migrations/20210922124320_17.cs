using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryAccounting.Infrastructure.Repositories.Migrations
{
    public partial class _17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "c90b7d29-5384-4003-82ba-bc81dc3e2d97");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "348f600e-df7f-44af-be83-63b25fd99d62");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "43977bbe-a697-4fde-8d1a-d0014a304a46");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "Password", "PasswordHash" },
                values: new object[] { "708cb70f-cfd4-408d-bc9c-be994a36375d", true, null, "298115994" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed" },
                values: new object[] { "7a941247-e3bc-48a0-ac83-f0fde4d8611e", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed" },
                values: new object[] { "0531b278-9aa3-484a-919e-d11352844896", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed" },
                values: new object[] { "d1ed4655-7b1f-425d-a6b1-1579b37dd606", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed" },
                values: new object[] { "452f4ebd-9029-4634-b93a-12aeb299acbd", true });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "BookingDate",
                value: new DateTime(2021, 9, 22, 15, 43, 20, 170, DateTimeKind.Local).AddTicks(8862));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "23b79ae3-41cd-4ba9-9b89-a59eb643dd7a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "27b81a7e-dedc-42ca-b4ae-4f9beb051011");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "1f97ff11-3bdc-47bb-9805-a40d24bbe586");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "Password", "PasswordHash" },
                values: new object[] { "c4381a87-8bf2-4474-8860-33c1997b2cb1", false, "sdfgDs23de", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed" },
                values: new object[] { "e495f0e8-1cdf-4c3e-b953-457323234a81", false });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed" },
                values: new object[] { "c0cec7a8-11d4-43de-a67c-a53d160438b7", false });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed" },
                values: new object[] { "e919cbda-cae5-4309-aede-97ac1c73c194", false });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed" },
                values: new object[] { "34d45867-a49a-48fa-9227-a5b0184257bf", false });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "BookingDate",
                value: new DateTime(2021, 9, 16, 15, 15, 34, 707, DateTimeKind.Local).AddTicks(2452));
        }
    }
}
