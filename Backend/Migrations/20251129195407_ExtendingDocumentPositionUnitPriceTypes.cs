using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class ExtendingDocumentPositionUnitPriceTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DocumentPositionTypeId",
                table: "DocumentPositions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "DocumentPositions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "DocumentPositionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentPositionTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentPositions_DocumentPositionTypeId",
                table: "DocumentPositions",
                column: "DocumentPositionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentPositions_DocumentPositionTypes_DocumentPositionTypeId",
                table: "DocumentPositions",
                column: "DocumentPositionTypeId",
                principalTable: "DocumentPositionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentPositions_DocumentPositionTypes_DocumentPositionTypeId",
                table: "DocumentPositions");

            migrationBuilder.DropTable(
                name: "DocumentPositionTypes");

            migrationBuilder.DropIndex(
                name: "IX_DocumentPositions_DocumentPositionTypeId",
                table: "DocumentPositions");

            migrationBuilder.DropColumn(
                name: "DocumentPositionTypeId",
                table: "DocumentPositions");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "DocumentPositions");
        }
    }
}
