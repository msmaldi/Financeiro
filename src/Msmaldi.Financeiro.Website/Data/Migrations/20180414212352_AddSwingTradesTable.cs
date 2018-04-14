using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Msmaldi.Financeiro.Website.Data.Migrations
{
    public partial class AddSwingTradesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SwingTrades",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Quantidade = table.Column<int>(nullable: false),
                    Symbol = table.Column<string>(type: "varchar(16)", nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    ValorDeAquisicao = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SwingTrades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SwingTrades_Stocks_Symbol",
                        column: x => x.Symbol,
                        principalTable: "Stocks",
                        principalColumn: "Symbol",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SwingTrades_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SwingTrades_Symbol",
                table: "SwingTrades",
                column: "Symbol");

            migrationBuilder.CreateIndex(
                name: "IX_SwingTrades_UserId",
                table: "SwingTrades",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SwingTrades");
        }
    }
}
