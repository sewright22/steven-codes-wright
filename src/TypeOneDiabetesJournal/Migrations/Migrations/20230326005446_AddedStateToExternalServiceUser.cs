using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AddedStateToExternalServiceUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExternalServiceUsers_Token_AccessTokenId",
                table: "ExternalServiceUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ExternalServiceUsers_Token_RefreshTokenId",
                table: "ExternalServiceUsers");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Email",
                keyValue: null,
                column: "Email",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "users",
                type: "longtext",
                nullable: false,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldCollation: "utf8mb4_general_ci")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "RefreshTokenId",
                table: "ExternalServiceUsers",
                type: "int(11)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int(11)");

            migrationBuilder.AlterColumn<int>(
                name: "AccessTokenId",
                table: "ExternalServiceUsers",
                type: "int(11)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int(11)");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "ExternalServiceUsers",
                type: "longtext",
                nullable: true,
                collation: "utf8_general_ci")
                .Annotation("MySql:CharSet", "utf8");

            migrationBuilder.AddForeignKey(
                name: "FK_ExternalServiceUsers_Token_AccessTokenId",
                table: "ExternalServiceUsers",
                column: "AccessTokenId",
                principalTable: "Token",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExternalServiceUsers_Token_RefreshTokenId",
                table: "ExternalServiceUsers",
                column: "RefreshTokenId",
                principalTable: "Token",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExternalServiceUsers_Token_AccessTokenId",
                table: "ExternalServiceUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ExternalServiceUsers_Token_RefreshTokenId",
                table: "ExternalServiceUsers");

            migrationBuilder.DropColumn(
                name: "State",
                table: "ExternalServiceUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "users",
                type: "longtext",
                nullable: true,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldCollation: "utf8mb4_general_ci")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "RefreshTokenId",
                table: "ExternalServiceUsers",
                type: "int(11)",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int(11)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AccessTokenId",
                table: "ExternalServiceUsers",
                type: "int(11)",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int(11)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ExternalServiceUsers_Token_AccessTokenId",
                table: "ExternalServiceUsers",
                column: "AccessTokenId",
                principalTable: "Token",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExternalServiceUsers_Token_RefreshTokenId",
                table: "ExternalServiceUsers",
                column: "RefreshTokenId",
                principalTable: "Token",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
