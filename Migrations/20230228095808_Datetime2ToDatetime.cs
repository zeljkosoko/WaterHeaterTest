using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace WaterHeaterTest.Migrations
{
    public partial class Datetime2ToDatetime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DatumKreiranja",
                table: "Agr_AktElektroispravnostBojler_SifElektropult",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2"
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DatumKreiranja",
                table: "Agr_AktElektroispravnostBojler_SifElektropult",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime"
                );
        }
    }
}
