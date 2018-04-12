using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Msmaldi.Financeiro.Website.Data.Migrations
{
    public partial class AddStockQuotesDailyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stock",
                columns: table => new
                {
                    Symbol = table.Column<string>(type: "varchar(16)", nullable: false),
                    Currency = table.Column<string>(type: "varchar(16)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock", x => x.Symbol);
                });

            migrationBuilder.CreateTable(
                name: "StockQuoteDaily",
                columns: table => new
                {
                    Symbol = table.Column<string>(type: "varchar(16)", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Close = table.Column<double>(nullable: false),
                    Open = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockQuoteDaily", x => new { x.Symbol, x.Date });
                    table.ForeignKey(
                        name: "FK_StockQuoteDaily_Stock_Symbol",
                        column: x => x.Symbol,
                        principalTable: "Stock",
                        principalColumn: "Symbol",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockQuoteDaily");

            migrationBuilder.DropTable(
                name: "Stock");
        }
    }
}
