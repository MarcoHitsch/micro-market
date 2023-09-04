using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace catalog_service.Migrations
{
    public partial class DataSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "VatRate" },
                values: new object[,]
                {
                    { new Guid("5bcc6ae7-a565-4a42-9995-ce657a4b6293"), "Soft", 19m },
                    { new Guid("708c04fd-4242-4e00-8035-2a966f45f69d"), "Zusatz", 7m },
                    { new Guid("c42d3be1-7684-4f0d-ba9a-f011981f1da2"), "Alkohol", 19m }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { new Guid("32d8205f-6340-45f7-9da2-754b9fd4becf"), new Guid("5bcc6ae7-a565-4a42-9995-ce657a4b6293"), "Auch Energon genannt", "Bilig Energy", 0.95m, 12 },
                    { new Guid("5d495be8-9c44-4680-9352-4f30e4b49f5b"), new Guid("5bcc6ae7-a565-4a42-9995-ce657a4b6293"), "Nektar", "Sauerkirsch", 1.15m, 25 },
                    { new Guid("984688cc-509d-401b-98e0-fe41efdb397f"), new Guid("c42d3be1-7684-4f0d-ba9a-f011981f1da2"), "Gin", "Gin", 18.5m, 5 },
                    { new Guid("d5c62730-c3d7-4c5c-9818-ded8318ba7e8"), new Guid("c42d3be1-7684-4f0d-ba9a-f011981f1da2"), "Kostengünstige Alternative zu Jägermeister", "Jagdfürst", 5.99m, 21 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("708c04fd-4242-4e00-8035-2a966f45f69d"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("32d8205f-6340-45f7-9da2-754b9fd4becf"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("5d495be8-9c44-4680-9352-4f30e4b49f5b"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("984688cc-509d-401b-98e0-fe41efdb397f"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d5c62730-c3d7-4c5c-9818-ded8318ba7e8"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("5bcc6ae7-a565-4a42-9995-ce657a4b6293"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c42d3be1-7684-4f0d-ba9a-f011981f1da2"));
        }
    }
}
