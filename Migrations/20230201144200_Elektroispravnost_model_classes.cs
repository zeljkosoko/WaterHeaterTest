using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WaterHeaterTest.Migrations
{
    public partial class Elektroispravnost_model_classes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AktElektroispravnostBojler",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTestResults = table.Column<int>(nullable: false),
                    IdKorisnikKontrolisao = table.Column<string>(nullable: true),
                    DatumKontrolisanja = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AktElektroispravnostBojler", x => x.Id);
                });

          

            migrationBuilder.CreateTable(
                name: "STEP_CR",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CHECKRMIN = table.Column<string>(nullable: true),
                    DEVNR = table.Column<string>(nullable: true),
                    ERRCODE = table.Column<string>(nullable: true),
                    MTXMINUS = table.Column<string>(nullable: true),
                    MTXMINUSSTR = table.Column<string>(nullable: true),
                    MTXPLUS = table.Column<string>(nullable: true),
                    MTXPLUSSTR = table.Column<string>(nullable: true),
                    MYRESULT = table.Column<string>(nullable: true),
                    NRFAILED = table.Column<string>(nullable: true),
                    NRPASSED = table.Column<string>(nullable: true),
                    RMAX = table.Column<string>(nullable: true),
                    RMIN = table.Column<string>(nullable: true),
                    RREAL = table.Column<string>(nullable: true),
                    RREALOL = table.Column<string>(nullable: true),
                    STEPNR = table.Column<string>(nullable: true),
                    STEPTITLE = table.Column<string>(nullable: true),
                    TESTTIME = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STEP_CR", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "STEP_CT",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CHECKIMAX = table.Column<string>(nullable: true),
                    DEVNR = table.Column<string>(nullable: true),
                    ERRCODE = table.Column<string>(nullable: true),
                    IMAX = table.Column<string>(nullable: true),
                    IMIN = table.Column<string>(nullable: true),
                    IREAL = table.Column<string>(nullable: true),
                    MTXMINUS = table.Column<string>(nullable: true),
                    MTXMINUSSTR = table.Column<string>(nullable: true),
                    MTXPLUS = table.Column<string>(nullable: true),
                    MTXPLUSSTR = table.Column<string>(nullable: true),
                    MYRESULT = table.Column<string>(nullable: true),
                    NRFAILED = table.Column<string>(nullable: true),
                    NRPASSED = table.Column<string>(nullable: true),
                    STEPNR = table.Column<string>(nullable: true),
                    STEPTITLE = table.Column<string>(nullable: true),
                    TESTTIME = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STEP_CT", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "STEP_FK",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CHECKU1 = table.Column<string>(nullable: true),
                    CHECKU2 = table.Column<string>(nullable: true),
                    CHECKU3 = table.Column<string>(nullable: true),
                    COSPHIMAX1 = table.Column<string>(nullable: true),
                    COSPHIMAX2 = table.Column<string>(nullable: true),
                    COSPHIMAX3 = table.Column<string>(nullable: true),
                    COSPHIMIN1 = table.Column<string>(nullable: true),
                    COSPHIMIN2 = table.Column<string>(nullable: true),
                    COSPHIMIN3 = table.Column<string>(nullable: true),
                    COSPHIREAL1 = table.Column<string>(nullable: true),
                    COSPHIREAL2 = table.Column<string>(nullable: true),
                    COSPHIREAL3 = table.Column<string>(nullable: true),
                    DEVNR = table.Column<string>(nullable: true),
                    ERRCODE = table.Column<string>(nullable: true),
                    GOODTIME = table.Column<string>(nullable: true),
                    GOODTIMEOK = table.Column<string>(nullable: true),
                    IMAX1 = table.Column<string>(nullable: true),
                    IMAX2 = table.Column<string>(nullable: true),
                    IMAX3 = table.Column<string>(nullable: true),
                    IMIN1 = table.Column<string>(nullable: true),
                    IMIN2 = table.Column<string>(nullable: true),
                    IMIN3 = table.Column<string>(nullable: true),
                    IREAL1 = table.Column<string>(nullable: true),
                    IREAL2 = table.Column<string>(nullable: true),
                    IREAL3 = table.Column<string>(nullable: true),
                    KEEPPOWER = table.Column<string>(nullable: true),
                    MTXMINUS = table.Column<string>(nullable: true),
                    MTXMINUSSTR = table.Column<string>(nullable: true),
                    MTXPLUS = table.Column<string>(nullable: true),
                    MTXPLUSSTR = table.Column<string>(nullable: true),
                    MYRESULT = table.Column<string>(nullable: true),
                    NRFAILED = table.Column<string>(nullable: true),
                    NRPASSED = table.Column<string>(nullable: true),
                    PHASE = table.Column<string>(nullable: true),
                    PMAX = table.Column<string>(nullable: true),
                    PMIN = table.Column<string>(nullable: true),
                    POWERREAL = table.Column<string>(nullable: true),
                    STEPNR = table.Column<string>(nullable: true),
                    STEPTITLE = table.Column<string>(nullable: true),
                    TESTTIME = table.Column<string>(nullable: true),
                    UMAX1 = table.Column<string>(nullable: true),
                    UMAX2 = table.Column<string>(nullable: true),
                    UMAX3 = table.Column<string>(nullable: true),
                    UMIN1 = table.Column<string>(nullable: true),
                    UMIN2 = table.Column<string>(nullable: true),
                    UMIN3 = table.Column<string>(nullable: true),
                    UREAL1 = table.Column<string>(nullable: true),
                    UREAL2 = table.Column<string>(nullable: true),
                    UREAL3 = table.Column<string>(nullable: true),
                    USECOSPHI1 = table.Column<string>(nullable: true),
                    USECOSPHI2 = table.Column<string>(nullable: true),
                    USECOSPHI3 = table.Column<string>(nullable: true),
                    USEPHASE1 = table.Column<string>(nullable: true),
                    USEPHASE2 = table.Column<string>(nullable: true),
                    USEPHASE3 = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STEP_FK", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "STEP_H5",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ARCMAX = table.Column<string>(nullable: true),
                    ARCREAL = table.Column<string>(nullable: true),
                    CONNECT = table.Column<string>(nullable: true),
                    DEVNR = table.Column<string>(nullable: true),
                    ERRCODE = table.Column<string>(nullable: true),
                    IMAX = table.Column<string>(nullable: true),
                    IMIN = table.Column<string>(nullable: true),
                    IREAL = table.Column<string>(nullable: true),
                    IRMAX = table.Column<string>(nullable: true),
                    IRMIN = table.Column<string>(nullable: true),
                    ITYPE = table.Column<string>(nullable: true),
                    METHOD = table.Column<string>(nullable: true),
                    MTXMINUS = table.Column<string>(nullable: true),
                    MTXMINUSSTR = table.Column<string>(nullable: true),
                    MTXPLUS = table.Column<string>(nullable: true),
                    MTXPLUSSTR = table.Column<string>(nullable: true),
                    MYRESULT = table.Column<string>(nullable: true),
                    NRFAILED = table.Column<string>(nullable: true),
                    NRPASSED = table.Column<string>(nullable: true),
                    RAMPDOWN = table.Column<string>(nullable: true),
                    RAMPERR = table.Column<string>(nullable: true),
                    RAMPTIME = table.Column<string>(nullable: true),
                    STEPNR = table.Column<string>(nullable: true),
                    STEPTITLE = table.Column<string>(nullable: true),
                    TESTTIME = table.Column<string>(nullable: true),
                    UNOM = table.Column<string>(nullable: true),
                    UREAL = table.Column<string>(nullable: true),
                    USEDARC = table.Column<string>(nullable: true),
                    USTART = table.Column<string>(nullable: true),
                    UTYPE = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STEP_H5", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "STEP_I5",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CONNECT = table.Column<string>(nullable: true),
                    DEVNR = table.Column<string>(nullable: true),
                    ERRCODE = table.Column<string>(nullable: true),
                    IRMAX = table.Column<string>(nullable: true),
                    IRMIN = table.Column<string>(nullable: true),
                    MTXMINUS = table.Column<string>(nullable: true),
                    MTXMINUSSTR = table.Column<string>(nullable: true),
                    MTXPLUS = table.Column<string>(nullable: true),
                    MTXPLUSSTR = table.Column<string>(nullable: true),
                    MYRESULT = table.Column<string>(nullable: true),
                    NRFAILED = table.Column<string>(nullable: true),
                    NRPASSED = table.Column<string>(nullable: true),
                    RAMPDOWN = table.Column<string>(nullable: true),
                    RAMPERR = table.Column<string>(nullable: true),
                    RAMPTIME = table.Column<string>(nullable: true),
                    RMIN = table.Column<string>(nullable: true),
                    RREAL = table.Column<string>(nullable: true),
                    RREALOL = table.Column<string>(nullable: true),
                    SKMODE = table.Column<string>(nullable: true),
                    STEPNR = table.Column<string>(nullable: true),
                    STEPTITLE = table.Column<string>(nullable: true),
                    TESTTIME = table.Column<string>(nullable: true),
                    UNOM = table.Column<string>(nullable: true),
                    UREAL = table.Column<string>(nullable: true),
                    USTART = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STEP_I5", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "STEP_L1",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DEVNR = table.Column<string>(nullable: true),
                    ERRCODE = table.Column<string>(nullable: true),
                    IMAX = table.Column<string>(nullable: true),
                    IMIN = table.Column<string>(nullable: true),
                    IREAL = table.Column<string>(nullable: true),
                    IREALMAX = table.Column<string>(nullable: true),
                    MTXMINUS = table.Column<string>(nullable: true),
                    MTXMINUSSTR = table.Column<string>(nullable: true),
                    MTXPLUS = table.Column<string>(nullable: true),
                    MTXPLUSSTR = table.Column<string>(nullable: true),
                    MYRESULT = table.Column<string>(nullable: true),
                    NRFAILED = table.Column<string>(nullable: true),
                    NRPASSED = table.Column<string>(nullable: true),
                    PHASE = table.Column<string>(nullable: true),
                    STEPNR = table.Column<string>(nullable: true),
                    STEPTITLE = table.Column<string>(nullable: true),
                    TESTTIME = table.Column<string>(nullable: true),
                    UMODE = table.Column<string>(nullable: true),
                    UNOM = table.Column<string>(nullable: true),
                    UREAL = table.Column<string>(nullable: true),
                    UTYPE = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STEP_L1", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "STEP_PW",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DEVNR = table.Column<string>(nullable: true),
                    ERRCODE = table.Column<string>(nullable: true),
                    FASTPW = table.Column<string>(nullable: true),
                    IMIN = table.Column<string>(nullable: true),
                    IREAL = table.Column<string>(nullable: true),
                    METHOD = table.Column<string>(nullable: true),
                    MTXMINUS = table.Column<string>(nullable: true),
                    MTXMINUSSTR = table.Column<string>(nullable: true),
                    MTXPLUS = table.Column<string>(nullable: true),
                    MTXPLUSSTR = table.Column<string>(nullable: true),
                    MYRESULT = table.Column<string>(nullable: true),
                    NRFAILED = table.Column<string>(nullable: true),
                    NRPASSED = table.Column<string>(nullable: true),
                    PWMSET = table.Column<string>(nullable: true),
                    RMAX = table.Column<string>(nullable: true),
                    RMIN = table.Column<string>(nullable: true),
                    RREAL = table.Column<string>(nullable: true),
                    STARTMODE = table.Column<string>(nullable: true),
                    STEPNR = table.Column<string>(nullable: true),
                    STEPTITLE = table.Column<string>(nullable: true),
                    TESTTIME = table.Column<string>(nullable: true),
                    UDROPMAX = table.Column<string>(nullable: true),
                    UDROPREAL = table.Column<string>(nullable: true),
                    UMAX = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STEP_PW", x => x.Id);
                });

           

            migrationBuilder.CreateTable(
                name: "TEST_RESULTS",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    COMMENT1 = table.Column<string>(nullable: true),
                    COMMENT2 = table.Column<string>(nullable: true),
                    DEVICENAME = table.Column<string>(nullable: true),
                    DEVICEVALUE = table.Column<string>(nullable: true),
                    DEVNR = table.Column<string>(nullable: true),
                    ENDDATE = table.Column<string>(nullable: true),
                    ERRCODE = table.Column<string>(nullable: true),
                    MTXMINUS = table.Column<string>(nullable: true),
                    MTXMINUSSTR = table.Column<string>(nullable: true),
                    MTXPLUS = table.Column<string>(nullable: true),
                    MTXPLUSSTR = table.Column<string>(nullable: true),
                    MYRESULT = table.Column<string>(nullable: true),
                    NRFAILED = table.Column<string>(nullable: true),
                    NRPASSED = table.Column<string>(nullable: true),
                    PRODUCTID = table.Column<string>(nullable: true),
                    PROGRAMFILE = table.Column<string>(nullable: true),
                    REMARKNAME = table.Column<string>(nullable: true),
                    REMARKVALUE = table.Column<string>(nullable: true),
                    SERIALNR = table.Column<string>(nullable: true),
                    STARTDATE = table.Column<string>(nullable: true),
                    STATIONID = table.Column<string>(nullable: true),
                    STEPNR = table.Column<string>(nullable: true),
                    STEPTITLE = table.Column<string>(nullable: true),
                    TOTALRESULT = table.Column<string>(nullable: true),
                    USERTESTER = table.Column<string>(nullable: true),
                    WEEKNR = table.Column<string>(nullable: true),
                    STEP_CRId = table.Column<int>(nullable: true),//FK STEP_CR 
                    STEP_PWId = table.Column<int>(nullable: true),
                    STEP_H5Id = table.Column<int>(nullable: true),
                    STEP_I5Id = table.Column<int>(nullable: true),
                    STEP_FKId = table.Column<int>(nullable: true),
                    STEP_L1Id = table.Column<int>(nullable: true),
                    STEP_CTId = table.Column<int>(nullable: true),
                    IdSTEP_CR = table.Column<int>(nullable: true),
                    IdSTEP_PW = table.Column<int>(nullable: true),
                    IdSTEP_H5 = table.Column<int>(nullable: true),
                    IdSTEP_I5 = table.Column<int>(nullable: true),
                    IdSTEP_FK = table.Column<int>(nullable: true),
                    IdSTEP_L1 = table.Column<int>(nullable: true),
                    IdSTEP_CT = table.Column<int>(nullable: true),
                    IdAgr_ExtaStepStack = table.Column<int>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TEST_RESULTS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TEST_RESULTS_STEP_CR_STEP_CRId",
                        column: x => x.STEP_CRId,
                        principalTable: "STEP_CR",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict); 
                    table.ForeignKey(
                        name: "FK_TEST_RESULTS_STEP_CT_STEP_CTId",
                        column: x => x.STEP_CTId,
                        principalTable: "STEP_CT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TEST_RESULTS_STEP_FK_STEP_FKId",
                        column: x => x.STEP_FKId,
                        principalTable: "STEP_FK",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TEST_RESULTS_STEP_H5_STEP_H5Id",
                        column: x => x.STEP_H5Id,
                        principalTable: "STEP_H5",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TEST_RESULTS_STEP_I5_STEP_I5Id",
                        column: x => x.STEP_I5Id,
                        principalTable: "STEP_I5",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TEST_RESULTS_STEP_L1_STEP_L1Id",
                        column: x => x.STEP_L1Id,
                        principalTable: "STEP_L1",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TEST_RESULTS_STEP_PW_STEP_PWId",
                        column: x => x.STEP_PWId,
                        principalTable: "STEP_PW",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

           
            migrationBuilder.CreateIndex(
                name: "IX_TEST_RESULTS_STEP_CRId",
                table: "TEST_RESULTS",
                column: "STEP_CRId");

            migrationBuilder.CreateIndex(
                name: "IX_TEST_RESULTS_STEP_CTId",
                table: "TEST_RESULTS",
                column: "STEP_CTId");

            migrationBuilder.CreateIndex(
                name: "IX_TEST_RESULTS_STEP_FKId",
                table: "TEST_RESULTS",
                column: "STEP_FKId");

            migrationBuilder.CreateIndex(
                name: "IX_TEST_RESULTS_STEP_H5Id",
                table: "TEST_RESULTS",
                column: "STEP_H5Id");

            migrationBuilder.CreateIndex(
                name: "IX_TEST_RESULTS_STEP_I5Id",
                table: "TEST_RESULTS",
                column: "STEP_I5Id");

            migrationBuilder.CreateIndex(
                name: "IX_TEST_RESULTS_STEP_L1Id",
                table: "TEST_RESULTS",
                column: "STEP_L1Id");

            migrationBuilder.CreateIndex(
                name: "IX_TEST_RESULTS_STEP_PWId",
                table: "TEST_RESULTS",
                column: "STEP_PWId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
 
            migrationBuilder.DropTable(
                name: "AktElektroispravnostBojler");

            migrationBuilder.DropTable(
                name: "TEST_RESULTS");

            migrationBuilder.DropTable(
                name: "STEP_CR");

            migrationBuilder.DropTable(
                name: "STEP_CT");

            migrationBuilder.DropTable(
                name: "STEP_FK");

            migrationBuilder.DropTable(
                name: "STEP_H5");

            migrationBuilder.DropTable(
                name: "STEP_I5");

            migrationBuilder.DropTable(
                name: "STEP_L1");

            migrationBuilder.DropTable(
                name: "STEP_PW");

        }
    }
}
