using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WaterHeaterTest.Migrations
{
    public partial class Added_SifArtikal_Model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SifArtikal",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sifra = table.Column<string>(nullable: true),
                    Naziv = table.Column<string>(nullable: true),
                    IdPc = table.Column<int>(nullable: false),
                    IdKlasa = table.Column<int>(nullable: false),
                    DatumKreiran = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SifArtikal", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SifArtikal");
        }
    }
}
