using Microsoft.EntityFrameworkCore.Migrations;

namespace WaterHeaterTest.Migrations
{
    public partial class FK_AgrVizuelnaGreskaOpis_SifVizuelnaGreska : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_Agr_VizuelnaGreska_VizuelnaGreska_IdVizuelnaGreska",
                table: "Agr_Vizuelna_Greska_Opis",
                column: "IdVizuelnaGreska",
                principalTable: "Sif_Vizuelna_Greska",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agr_VizuelnaGreska_VizuelnaGreska_IdVizuelnaGreska",
                table: "Agr_Vizuelna_Greska_Opis");
        }
    }
}
