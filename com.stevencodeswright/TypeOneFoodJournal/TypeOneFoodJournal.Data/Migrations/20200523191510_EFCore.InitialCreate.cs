using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TypeOneFoodJournal.Data.Migrations
{
    public partial class EFCoreInitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    InsulinAmount = table.Column<decimal>(nullable: false),
                    UpFront = table.Column<int>(nullable: false),
                    Extended = table.Column<int>(nullable: false),
                    TimeExtended = table.Column<decimal>(nullable: false),
                    TimeOffset = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JournalEntries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Logged = table.Column<DateTime>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalEntries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NutritionalInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Calories = table.Column<int>(nullable: false),
                    Protein = table.Column<int>(nullable: false),
                    Carbohydrates = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NutritionalInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JournalEntryDoses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    JournalEntryId = table.Column<int>(nullable: false),
                    DoseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalEntryDoses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JournalEntryDoses_Doses_DoseId",
                        column: x => x.DoseId,
                        principalTable: "Doses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JournalEntryDoses_JournalEntries_JournalEntryId",
                        column: x => x.JournalEntryId,
                        principalTable: "JournalEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JournalEntryNutritionalInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    JournalEntryId = table.Column<int>(nullable: false),
                    NutritionalInfoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalEntryNutritionalInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JournalEntryNutritionalInfos_JournalEntries_JournalEntryId",
                        column: x => x.JournalEntryId,
                        principalTable: "JournalEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JournalEntryNutritionalInfos_NutritionalInfos_NutritionalInf~",
                        column: x => x.NutritionalInfoId,
                        principalTable: "NutritionalInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JournalEntryTags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    JournalEntryId = table.Column<int>(nullable: false),
                    TagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalEntryTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JournalEntryTags_JournalEntries_JournalEntryId",
                        column: x => x.JournalEntryId,
                        principalTable: "JournalEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JournalEntryTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntryDoses_DoseId",
                table: "JournalEntryDoses",
                column: "DoseId");

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntryDoses_JournalEntryId",
                table: "JournalEntryDoses",
                column: "JournalEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntryNutritionalInfos_JournalEntryId",
                table: "JournalEntryNutritionalInfos",
                column: "JournalEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntryNutritionalInfos_NutritionalInfoId",
                table: "JournalEntryNutritionalInfos",
                column: "NutritionalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntryTags_JournalEntryId",
                table: "JournalEntryTags",
                column: "JournalEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntryTags_TagId",
                table: "JournalEntryTags",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JournalEntryDoses");

            migrationBuilder.DropTable(
                name: "JournalEntryNutritionalInfos");

            migrationBuilder.DropTable(
                name: "JournalEntryTags");

            migrationBuilder.DropTable(
                name: "Doses");

            migrationBuilder.DropTable(
                name: "NutritionalInfos");

            migrationBuilder.DropTable(
                name: "JournalEntries");

            migrationBuilder.DropTable(
                name: "Tags");
        }
    }
}
