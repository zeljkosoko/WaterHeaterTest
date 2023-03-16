using Microsoft.EntityFrameworkCore.Migrations;

namespace WaterHeaterTest.Migrations
{
    public partial class Agr_AktElektroispravnost_FK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_AgrAktElektroispravnost",
                table: "Agr_AktElektroispravnostBojler_SifElektropult",
                column: "IdAktElektroispravnostBojler",
                principalTable: "AktElektroispravnostBojler",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AgrSifElektropult",
                table: "Agr_AktElektroispravnostBojler_SifElektropult",
                column: "IdSifElektropult",
                principalTable: "SifElektropult",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_AgrAktElektroispravnost",
                table: "Agr_AktElektroispravnostBojler_SifElektropult");

            migrationBuilder.DropForeignKey(name: "FK_AgrSifElektropult",
                table: "Agr_AktElektroispravnostBojler_SifElektropult");
        }
    }
}
