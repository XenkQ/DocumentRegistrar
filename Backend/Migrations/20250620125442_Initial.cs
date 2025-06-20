using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contractors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Symbol = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contractors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdmissionDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ContractorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdmissionDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdmissionDocuments_Contractors_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "Contractors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentPositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameOfProduct = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MeasurementUnit = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    AdmissionDocumentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentPositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentPositions_AdmissionDocuments_AdmissionDocumentId",
                        column: x => x.AdmissionDocumentId,
                        principalTable: "AdmissionDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdmissionDocuments_ContractorId",
                table: "AdmissionDocuments",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_AdmissionDocuments_Symbol",
                table: "AdmissionDocuments",
                column: "Symbol",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contractors_Symbol",
                table: "Contractors",
                column: "Symbol",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentPositions_AdmissionDocumentId",
                table: "DocumentPositions",
                column: "AdmissionDocumentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentPositions");

            migrationBuilder.DropTable(
                name: "AdmissionDocuments");

            migrationBuilder.DropTable(
                name: "Contractors");
        }
    }
}
