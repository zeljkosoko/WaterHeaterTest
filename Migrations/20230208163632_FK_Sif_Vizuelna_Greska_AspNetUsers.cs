using Microsoft.EntityFrameworkCore.Migrations;

namespace WaterHeaterTest.Migrations
{
    public partial class FK_Sif_Vizuelna_Greska_AspNetUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_Sif_Vizuelna_Greska_AspNetUsers_IdKorisnikKreirao",
                table: "Sif_Vizuelna_Greska",
                column: "IdKorisnikKreirao",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sif_Vizuelna_Greska_AspNetUsers_IdKorisnikKreirao",
                table: "Sif_Vizuelna_Greska");
        }
    }
}
