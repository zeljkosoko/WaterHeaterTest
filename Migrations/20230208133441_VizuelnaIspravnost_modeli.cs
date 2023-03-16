using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WaterHeaterTest.Migrations
{
    public partial class VizuelnaIspravnost_modeli : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agr_Vizuelna_Greska_Opis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdVizuelnaGreska = table.Column<int>(nullable: false),
                    Opis = table.Column<string>(nullable: true),
                    Komentar = table.Column<string>(nullable: true),
                    DatumKreiranja = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agr_Vizuelna_Greska_Opis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Akt_Vizuelna_Ispravnost_Bojler",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSerijskiBrojBojler = table.Column<int>(nullable: false),
                    Ispravan = table.Column<bool>(nullable: false),
                    IdAgrVizuelnaGreskaOpis = table.Column<int>(nullable: true),
                    IdKorisnikKontrolisao = table.Column<int>(nullable: false),
                    DatumKontrolisanja = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Akt_Vizuelna_Ispravnost_Bojler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sif_Serijski_Broj_Bojler",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: false),
                    DatumKreiranja = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sif_Serijski_Broj_Bojler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sif_Vizuelna_Greska",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: false),
                    Tip = table.Column<int>(nullable: false),
                    DatumKreiranja = table.Column<DateTime>(nullable: false),
                    IdKorisnikKreirao = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sif_Vizuelna_Greska", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agr_Vizuelna_Greska_Opis");

            migrationBuilder.DropTable(
                name: "Akt_Vizuelna_Ispravnost_Bojler");

            migrationBuilder.DropTable(
                name: "Sif_Serijski_Broj_Bojler");

            migrationBuilder.DropTable(
                name: "Sif_Vizuelna_Greska");
        }
    }
}
