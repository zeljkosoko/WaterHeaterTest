using Microsoft.EntityFrameworkCore.Migrations;

namespace WaterHeaterTest.Migrations
{
    public partial class FK_AktElektroIspravnost_TEST_RESULTS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_AktElektroIspravnostBojler_TEST_RESULTS_IdTestResults",
                table: "AktElektroIspravnostBojler",
                column: "IdTestResults",
                principalTable: "TEST_RESULTS",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
               name: "FK_AktElektroIspravnostBojler_TEST_RESULTS_IdTestResults",
               table: "AktElektroIspravnostBojler");
        }
    }
}
