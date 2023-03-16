using Microsoft.EntityFrameworkCore.Migrations;

namespace WaterHeaterTest.Migrations
{
    public partial class FK_Akt_VizuelnaIspravnost_AgrVizuelna_IdAgrVizuelna : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IdAgrVizuelnaGreskaOpis",
                table: "Akt_Vizuelna_Ispravnost_Bojler",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IdAgrVizuelnaGreskaOpis",
                table: "Akt_Vizuelna_Ispravnost_Bojler",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
