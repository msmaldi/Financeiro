using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Msmaldi.Financeiro.Website.Data.Migrations
{
    public partial class AddCryptoCurrenciesSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CryptoCurrencies",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(8)", nullable: false),
                    Name = table.Column<string>(type: "varchar(32)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CryptoCurrencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CryptoCurrencyLastTickers",
                columns: table => new
                {
                    CriptoCurrencyId = table.Column<string>(nullable: false),
                    Buy = table.Column<double>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Last = table.Column<double>(nullable: false),
                    Sell = table.Column<double>(nullable: false),
                    Source = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CryptoCurrencyLastTickers", x => x.CriptoCurrencyId);
                    table.ForeignKey(
                        name: "FK_CryptoCurrencyLastTickers_CryptoCurrencies_CriptoCurrencyId",
                        column: x => x.CriptoCurrencyId,
                        principalTable: "CryptoCurrencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CryptoWallets",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CriptoCurrencyId = table.Column<string>(nullable: true),
                    Label = table.Column<string>(type: "varchar(60)", nullable: false),
                    Quantidade = table.Column<double>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    ValorDeAquisicao = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CryptoWallets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CryptoWallets_CryptoCurrencies_CriptoCurrencyId",
                        column: x => x.CriptoCurrencyId,
                        principalTable: "CryptoCurrencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CryptoWallets_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CryptoWallets_CriptoCurrencyId",
                table: "CryptoWallets",
                column: "CriptoCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_CryptoWallets_UserId",
                table: "CryptoWallets",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CryptoCurrencyLastTickers");

            migrationBuilder.DropTable(
                name: "CryptoWallets");

            migrationBuilder.DropTable(
                name: "CryptoCurrencies");
        }
    }
}
