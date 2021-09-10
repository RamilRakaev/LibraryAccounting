using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryAccounting.Infrastructure.Repositories.Migrations
{
    public partial class _13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "453a563d-4a58-435b-9d0b-34877fc4a4b7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "5b2ad63f-602c-40e9-b618-19edfc05c527");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "eb3c67c1-9559-4214-8464-b4478775e59b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "Password", "UserName" },
                values: new object[] { "dfd9dffa-6bcb-4266-8fab-5ef66a751b73", "sdfgDs23de", "Ivan" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "Password", "UserName" },
                values: new object[] { "2516e755-cf2a-4bf4-84e3-58a89fdd5580", "e23D23df32", "Danil" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "Password", "UserName" },
                values: new object[] { "1315cc2c-1297-443c-a181-17de7eb45ad2", "Fd3D23d32r4", "Denis" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "Password", "UserName" },
                values: new object[] { "89a787d3-1706-41dd-80c4-f4105eefa04a", "Dgf34eR34r34r4", "Vanya" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ConcurrencyStamp", "Password", "UserName" },
                values: new object[] { "88876866-0c1a-49a8-ade9-3dcf43a3a0ee", "DE32f34r", "Dmitry" });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "BookingDate",
                value: new DateTime(2021, 9, 10, 16, 43, 30, 934, DateTimeKind.Local).AddTicks(5840));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "295d3d47-210e-4924-a01f-a0fbe3ef4fad");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "cc0dc96a-0023-4285-9cad-dbd2c4f04fe7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "9a0d3818-7e6a-46cb-bded-639da382311b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "Password", "UserName" },
                values: new object[] { "d73c5e1d-42b9-499f-8096-0584398f5f4f", "1234567890", "Иван" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "Password", "UserName" },
                values: new object[] { "9fc59c58-bbda-41af-8ce4-56063738fd59", "1234567890", "Данил" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "Password", "UserName" },
                values: new object[] { "411baaec-205b-448f-ab5b-51e369e1a450", "dasf34rfew43", "Денис" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "Password", "UserName" },
                values: new object[] { "8a130523-858a-4b4e-b189-e0e7070687eb", "23534534623423", "Ваня" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ConcurrencyStamp", "Password", "UserName" },
                values: new object[] { "bda09efa-a744-4608-905d-c23c9bd12d28", "п54вув324ук", "Дмитрий" });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "BookingDate",
                value: new DateTime(2021, 9, 10, 11, 12, 49, 301, DateTimeKind.Local).AddTicks(4985));
        }
    }
}
