using Microsoft.EntityFrameworkCore.Migrations;

namespace WaterHeaterTest.Migrations
{
    public partial class FK_AktVizIspBojler_SerBr_IdSerBr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_Akt_VizuelnaIspravnost_SerijskiB_IdSerijskiB",
                table: "Akt_Vizuelna_Ispravnost_Bojler",
                column: "IdSerijskiBrojBojler",
                principalTable: "Sif_Serijski_Broj_Bojler",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Akt_VizuelnaIspravnost_SerijskiB_IdSerijskiB",
                table: "Akt_Vizuelna_Ispravnost_Bojler");
        }
    }
}
