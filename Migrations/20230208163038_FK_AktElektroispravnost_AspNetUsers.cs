using Microsoft.EntityFrameworkCore.Migrations;

namespace WaterHeaterTest.Migrations
{
    public partial class FK_AktElektroispravnost_AspNetUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_AktElektroispravnostBojler_AspNetUsers_IdKorisnikKontrolisao",
                table: "AktElektroispravnostBojler",
                column: "IdKorisnikKontrolisao",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AktElektroispravnostBojler_AspNetUsers_IdKorisnikKontrolisao",
                table: "AktElektroispravnost_Bojler");
        }
    }
}
