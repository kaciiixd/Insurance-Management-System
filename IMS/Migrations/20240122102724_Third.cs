using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Migrations
{
    /// <inheritdoc />
    public partial class Third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientInsurance");

            migrationBuilder.DropColumn(
                name: "SelectedClient",
                table: "Insurances");

            migrationBuilder.AddColumn<Guid>(
                name: "ClientId",
                table: "Insurances",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Insurances_ClientId",
                table: "Insurances",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Insurances_Clients_ClientId",
                table: "Insurances",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Insurances_Clients_ClientId",
                table: "Insurances");

            migrationBuilder.DropIndex(
                name: "IX_Insurances_ClientId",
                table: "Insurances");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Insurances");

            migrationBuilder.AddColumn<string>(
                name: "SelectedClient",
                table: "Insurances",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ClientInsurance",
                columns: table => new
                {
                    ClientsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsurancesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientInsurance", x => new { x.ClientsId, x.InsurancesId });
                    table.ForeignKey(
                        name: "FK_ClientInsurance_Clients_ClientsId",
                        column: x => x.ClientsId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientInsurance_Insurances_InsurancesId",
                        column: x => x.InsurancesId,
                        principalTable: "Insurances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientInsurance_InsurancesId",
                table: "ClientInsurance",
                column: "InsurancesId");
        }
    }
}
