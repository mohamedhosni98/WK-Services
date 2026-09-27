using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WK_Services.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seedintialdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "City", "Country", "Email", "Name", "Phone", "Shipment_Address" },
                values: new object[,]
                {
                    { 1, "Cairo", "Egypt", "info@client1.com", "client1", "01000000001", "Address 1" },
                    { 2, "Giza", "Egypt", "info@client2.com", "client2", "01000000002", "Address 2" }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Air Mail service - sample description.", "AM" },
                    { 2, "Land Line service - sample description.", "LL" },
                    { 3, "Fast Box service - sample description.", "FB" },
                    { 4, "Legacy Land Line service - sample description.", "Old LL" }
                });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "ClientId", "Email", "Mobile", "Name", "Password", "User_Name" },
                values: new object[,]
                {
                    { 1, 1, "ahmed@client1.com", "01005500111", "ahmed", "GSEv3AiHWIvYxmVU39ZAzzO5S3HYyaHK9uOTYWD18pI=", "contact1_1" },
                    { 2, 2, "sara@client2.com", "01005500222", "sara", "GSEv3AiHWIvYxmVU39ZAzzO5S3HYyaHK9uOTYWD18pI=", "contact2_1" },
                    { 3, 2, "mona@client2.com", "01005500333", "mona", "GSEv3AiHWIvYxmVU39ZAzzO5S3HYyaHK9uOTYWD18pI=", "contact2_2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
