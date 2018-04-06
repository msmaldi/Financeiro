using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Msmaldi.Financeiro.Website.Data.Migrations
{
    public partial class CreateTableCDBsComCDI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CDBsComCDI",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DataDaAplicacao = table.Column<DateTime>(type: "date", nullable: false),
                    DataDoVencimento = table.Column<DateTime>(type: "date", nullable: false),
                    PrecoUnitario = table.Column<double>(nullable: false),
                    Quantidade = table.Column<int>(nullable: false),
                    Taxa = table.Column<double>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CDBsComCDI", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CDBsComCDI_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResgateCDBComCDI",
                columns: table => new
                {
                    CDBComCDIId = table.Column<int>(nullable: false),
                    Data = table.Column<DateTime>(type: "date", nullable: false),
                    Quantidade = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResgateCDBComCDI", x => new { x.CDBComCDIId, x.Data });
                    table.ForeignKey(
                        name: "FK_ResgateCDBComCDI_CDBsComCDI_CDBComCDIId",
                        column: x => x.CDBComCDIId,
                        principalTable: "CDBsComCDI",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CDBsComCDI_UserId",
                table: "CDBsComCDI",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResgateCDBComCDI");

            migrationBuilder.DropTable(
                name: "CDBsComCDI");
        }
    }
}
