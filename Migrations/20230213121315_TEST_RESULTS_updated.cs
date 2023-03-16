using Microsoft.EntityFrameworkCore.Migrations;

namespace WaterHeaterTest.Migrations
{
    public partial class TEST_RESULTS_updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SERIALNR",
                table: "TEST_RESULTS");

            migrationBuilder.AddColumn<int>(
                name: "IdSerijskiBrojBojler",
                table: "TEST_RESULTS",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sif_Serijski_Broj_BojlerId",
                table: "TEST_RESULTS",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TEST_RESULTS_Sif_Serijski_Broj_BojlerId",
                table: "TEST_RESULTS",
                column: "Sif_Serijski_Broj_BojlerId");

            migrationBuilder.AddForeignKey(
                name: "FK_TEST_RESULTS_Sif_Serijski_Broj_Bojler_Sif_Serijski_Broj_BojlerId",
                table: "TEST_RESULTS",
                column: "Sif_Serijski_Broj_BojlerId",
                principalTable: "Sif_Serijski_Broj_Bojler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TEST_RESULTS_Sif_Serijski_Broj_Bojler_Sif_Serijski_Broj_BojlerId",
                table: "TEST_RESULTS");

            migrationBuilder.DropIndex(
                name: "IX_TEST_RESULTS_Sif_Serijski_Broj_BojlerId",
                table: "TEST_RESULTS");

            migrationBuilder.DropColumn(
                name: "IdSerijskiBrojBojler",
                table: "TEST_RESULTS");

            migrationBuilder.DropColumn(
                name: "Sif_Serijski_Broj_BojlerId",
                table: "TEST_RESULTS");

            migrationBuilder.AddColumn<string>(
                name: "SERIALNR",
                table: "TEST_RESULTS",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
