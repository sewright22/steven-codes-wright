using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AddedTokenTypeNavigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Token");

            migrationBuilder.AddColumn<int>(
                name: "TokenTypeId",
                table: "Token",
                type: "int(11)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "ExternalServiceUsers",
                type: "longtext",
                nullable: true,
                collation: "utf8_general_ci")
                .Annotation("MySql:CharSet", "utf8");

            migrationBuilder.InsertData(
                table: "TokenType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Refresh" },
                    { 2, "Access" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Token_TokenTypeId",
                table: "Token",
                column: "TokenTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Token_TokenType_TokenTypeId",
                table: "Token",
                column: "TokenTypeId",
                principalTable: "TokenType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Token_TokenType_TokenTypeId",
                table: "Token");

            migrationBuilder.DropIndex(
                name: "IX_Token_TokenTypeId",
                table: "Token");

            migrationBuilder.DeleteData(
                table: "TokenType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TokenType",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "TokenTypeId",
                table: "Token");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "ExternalServiceUsers");

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Token",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
