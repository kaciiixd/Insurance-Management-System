using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Clients_ClientId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Insurances_InsuranceId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_ClientId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_InsuranceId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "InsuranceId",
                table: "Clients");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientInsurance");

            migrationBuilder.AddColumn<Guid>(
                name: "ClientId",
                table: "Clients",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InsuranceId",
                table: "Clients",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_ClientId",
                table: "Clients",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_InsuranceId",
                table: "Clients",
                column: "InsuranceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Clients_ClientId",
                table: "Clients",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Insurances_InsuranceId",
                table: "Clients",
                column: "InsuranceId",
                principalTable: "Insurances",
                principalColumn: "Id");
        }
    }
}
