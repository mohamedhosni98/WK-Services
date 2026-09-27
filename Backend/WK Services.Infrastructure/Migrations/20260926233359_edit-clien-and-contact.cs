using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WK_Services.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class editclienandcontact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Contacts_CreatedByContactId",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Contacts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_ClientId",
                table: "Contacts",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Clients_ClientId",
                table: "Contacts",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Contacts_CreatedByContactId",
                table: "Orders",
                column: "CreatedByContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Clients_ClientId",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Contacts_CreatedByContactId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_ClientId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Contacts");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Contacts_CreatedByContactId",
                table: "Orders",
                column: "CreatedByContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
