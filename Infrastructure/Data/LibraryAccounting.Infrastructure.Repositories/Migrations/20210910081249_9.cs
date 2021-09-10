using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryAccounting.Infrastructure.Repositories.Migrations
{
    public partial class _9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "295d3d47-210e-4924-a01f-a0fbe3ef4fad", "client", null },
                    { 2, "cc0dc96a-0023-4285-9cad-dbd2c4f04fe7", "librarian", null },
                    { 3, "9a0d3818-7e6a-46cb-bded-639da382311b", "admin", null }
                });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "BookingDate",
                value: new DateTime(2021, 9, 10, 11, 12, 49, 301, DateTimeKind.Local).AddTicks(4985));

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RoleId", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 3, 0, "411baaec-205b-448f-ab5b-51e369e1a450", "Denis@gmail.com", false, false, null, null, null, "dasf34rfew43", null, null, false, 1, null, false, "Денис" },
                    { 4, 0, "8a130523-858a-4b4e-b189-e0e7070687eb", "Vanek@gmail.com", false, false, null, null, null, "23534534623423", null, null, false, 1, null, false, "Ваня" },
                    { 5, 0, "bda09efa-a744-4608-905d-c23c9bd12d28", "DemRh@gmail.com", false, false, null, null, null, "п54вув324ук", null, null, false, 1, null, false, "Дмитрий" },
                    { 1, 0, "d73c5e1d-42b9-499f-8096-0584398f5f4f", "Ivan@gmail.com", false, false, null, null, null, "1234567890", null, null, false, 2, null, false, "Иван" },
                    { 2, 0, "9fc59c58-bbda-41af-8ce4-56063738fd59", "Danil@gmail.com", false, false, null, null, null, "1234567890", null, null, false, 3, null, false, "Данил" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "BookingDate",
                value: new DateTime(2021, 9, 10, 11, 8, 32, 81, DateTimeKind.Local).AddTicks(9343));
        }
    }
}
