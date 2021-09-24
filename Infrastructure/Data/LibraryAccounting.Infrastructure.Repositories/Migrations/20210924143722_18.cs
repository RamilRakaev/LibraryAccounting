using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryAccounting.Infrastructure.Repositories.Migrations
{
    public partial class _18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "608299f7-9f22-4e2f-8236-8e275f6c5da8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "80e7c843-111f-4b21-b1ba-55b422ef2f17");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "022849ab-2bdd-4cf4-85b3-40d37599e570");

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "BookingDate",
                value: new DateTime(2021, 9, 24, 17, 37, 22, 261, DateTimeKind.Local).AddTicks(61));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RoleId", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, "708cb70f-cfd4-408d-bc9c-be994a36375d", "Ivan@gmail.com", true, false, null, null, null, null, "298115994", null, false, 2, null, false, "Ivan" },
                    { 2, 0, "7a941247-e3bc-48a0-ac83-f0fde4d8611e", "Danil@gmail.com", true, false, null, null, null, "e23D23df32", null, null, false, 3, null, false, "Danil" },
                    { 3, 0, "0531b278-9aa3-484a-919e-d11352844896", "Denis@gmail.com", true, false, null, null, null, "Fd3D23d32r4", null, null, false, 1, null, false, "Denis" },
                    { 4, 0, "d1ed4655-7b1f-425d-a6b1-1579b37dd606", "Vanek@gmail.com", true, false, null, null, null, "Dgf34eR34r34r4", null, null, false, 1, null, false, "Vanya" },
                    { 5, 0, "452f4ebd-9029-4634-b93a-12aeb299acbd", "DemRh@gmail.com", true, false, null, null, null, "DE32f34rf38jL", null, null, false, 1, null, false, "Dmitry" }
                });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "BookingDate",
                value: new DateTime(2021, 9, 22, 15, 43, 20, 170, DateTimeKind.Local).AddTicks(8862));
        }
    }
}
