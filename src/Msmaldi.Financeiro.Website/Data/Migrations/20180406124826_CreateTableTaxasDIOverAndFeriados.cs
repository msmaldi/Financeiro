using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Msmaldi.Financeiro.Website.Data.Migrations
{
    public partial class CreateTableTaxasDIOverAndFeriados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Feriados",
                columns: table => new
                {
                    Data = table.Column<DateTime>(type: "date", nullable: false),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feriados", x => x.Data);
                });

            migrationBuilder.CreateTable(
                name: "TaxasDIOver",
                columns: table => new
                {
                    Data = table.Column<DateTime>(type: "date", nullable: false),
                    Taxa = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxasDIOver", x => x.Data);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Feriados");

            migrationBuilder.DropTable(
                name: "TaxasDIOver");
        }
    }
}
