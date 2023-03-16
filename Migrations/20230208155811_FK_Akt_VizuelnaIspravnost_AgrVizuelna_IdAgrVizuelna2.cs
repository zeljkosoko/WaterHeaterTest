using Microsoft.EntityFrameworkCore.Migrations;

namespace WaterHeaterTest.Migrations
{
    public partial class FK_Akt_VizuelnaIspravnost_AgrVizuelna_IdAgrVizuelna2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_Akt_VizuelnaIspravnost_SerijskiB_IdSerijskiB2",
                table: "Akt_Vizuelna_Ispravnost_Bojler",
                column: "IdAgrVizuelnaGreskaOpis",
                principalTable: "Agr_Vizuelna_Greska_Opis",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Akt_VizuelnaIspravnost_SerijskiB_IdSerijskiB2",
                table: "Akt_Vizuelna_Ispravnost_Bojler");
        }
    }
}
