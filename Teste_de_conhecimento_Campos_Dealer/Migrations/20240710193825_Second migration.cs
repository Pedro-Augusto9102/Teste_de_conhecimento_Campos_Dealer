using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teste_de_conhecimento_Campos_Dealer.Migrations
{
    /// <inheritdoc />
    public partial class Secondmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Venda_Clientes_idClienteId",
                table: "Venda");

            migrationBuilder.DropForeignKey(
                name: "FK_Venda_Produto_idProdutoId",
                table: "Venda");

            migrationBuilder.DropIndex(
                name: "IX_Venda_idClienteId",
                table: "Venda");

            migrationBuilder.DropIndex(
                name: "IX_Venda_idProdutoId",
                table: "Venda");

            migrationBuilder.AddColumn<Guid>(
                name: "clienteId",
                table: "Venda",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "produtoId",
                table: "Venda",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Venda_clienteId",
                table: "Venda",
                column: "clienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Venda_produtoId",
                table: "Venda",
                column: "produtoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Venda_Clientes_clienteId",
                table: "Venda",
                column: "clienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Venda_Produto_produtoId",
                table: "Venda",
                column: "produtoId",
                principalTable: "Produto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Venda_Clientes_clienteId",
                table: "Venda");

            migrationBuilder.DropForeignKey(
                name: "FK_Venda_Produto_produtoId",
                table: "Venda");

            migrationBuilder.DropIndex(
                name: "IX_Venda_clienteId",
                table: "Venda");

            migrationBuilder.DropIndex(
                name: "IX_Venda_produtoId",
                table: "Venda");

            migrationBuilder.DropColumn(
                name: "clienteId",
                table: "Venda");

            migrationBuilder.DropColumn(
                name: "produtoId",
                table: "Venda");

            migrationBuilder.CreateIndex(
                name: "IX_Venda_idClienteId",
                table: "Venda",
                column: "idClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Venda_idProdutoId",
                table: "Venda",
                column: "idProdutoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Venda_Clientes_idClienteId",
                table: "Venda",
                column: "idClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Venda_Produto_idProdutoId",
                table: "Venda",
                column: "idProdutoId",
                principalTable: "Produto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
