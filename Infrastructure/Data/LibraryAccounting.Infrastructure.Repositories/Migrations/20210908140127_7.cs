using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryAccounting.Infrastructure.Repositories.Migrations
{
    public partial class _7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BookId", "BookingDate" },
                values: new object[] { 3, new DateTime(2021, 9, 8, 17, 1, 27, 167, DateTimeKind.Local).AddTicks(3946) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Author", "Genre", "Publisher", "Title" },
                values: new object[] { "Кехо Джон", "Психология", "Попурри", "Подсознание может все!" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "Title",
                value: "История");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Publisher", "Title" },
                values: new object[] { "Москва", "Биология" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Genre", "Publisher", "Title" },
                values: new object[,]
                {
                    { 4, "Некто", "Наука", "Питер", "Химия" },
                    { 5, "Стивен Кови", "Книги по личностному росту от Стивена Кови", "Альпина Паблишер", "Семь навыков высокоэффективных людей." },
                    { 6, "А. Ивич; Рис. Э. Беньяминсона, Б. Кыштымова", "Детская литература", "Москва", "Семьдесят богатырей" },
                    { 7, "Д.И. Менделеев", "Наука", "АСТ", "Периодическая система химических элементов" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "1234567890");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "dasf34rfew43");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "RoleId" },
                values: new object[,]
                {
                    { 4, "Vanek@gmail.com", "Ваня", "23534534623423", 1 },
                    { 5, "DemRh@gmail.com", "Дмитрий", "п54вув324ук", 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BookId", "BookingDate" },
                values: new object[] { 2, new DateTime(2021, 9, 8, 16, 51, 57, 687, DateTimeKind.Local).AddTicks(8597) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Author", "Genre", "Publisher", "Title" },
                values: new object[] { "Некто", "Наука", "Москва", "История" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "Title",
                value: "Биология");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Publisher", "Title" },
                values: new object[] { "Питер", "Химия" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "1235567890");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "1232567890");
        }
    }
}
