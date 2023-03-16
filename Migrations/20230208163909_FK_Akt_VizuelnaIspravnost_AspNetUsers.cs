using Microsoft.EntityFrameworkCore.Migrations;

namespace WaterHeaterTest.Migrations
{
    public partial class FK_Akt_VizuelnaIspravnost_AspNetUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_Akt_Vizuelna_Ispravnost_Bojler_AspNetUsers_IdKorisnikKontrolisao",
                table: "Akt_Vizuelna_Ispravnost_Bojler",
                column: "IdKorisnikKontrolisao",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
               name: "FK_Akt_Vizuelna_Ispravnost_Bojler_AspNetUsers_IdKorisnikKontrolisao",
               table: "Akt_Vizuelna_Ispravnost_Bojler");
        }
    }
}
