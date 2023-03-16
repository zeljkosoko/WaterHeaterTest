using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WaterHeaterTest.Migrations
{
    public partial class Agr_AktElektroispravnost_SifElektropult_Model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agr_AktElektroispravnostBojler_SifElektropult",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAktElektroispravnostBojler = table.Column<int>(nullable: false),
                    IdSifElektropult = table.Column<int>(nullable: false),
                    DatumKreiranja = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agr_AktElektroispravnostBojler_SifElektropult", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agr_AktElektroispravnostBojler_SifElektropult");
        }
    }
}
