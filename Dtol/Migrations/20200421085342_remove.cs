using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dtol.Migrations
{
    public partial class remove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MydutyClaim_Info");

            migrationBuilder.DropTable(
                name: "OndutyClaims_Info");

            migrationBuilder.DropTable(
                name: "Normalization_Info");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Normalization_Info",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CheckInTime = table.Column<DateTime>(nullable: true),
                    CheckOutTime = table.Column<DateTime>(nullable: true),
                    CommunityName = table.Column<string>(nullable: true),
                    CommunityNameCode = table.Column<string>(nullable: true),
                    PointsEarned = table.Column<string>(nullable: true),
                    ServiceContent = table.Column<string>(nullable: true),
                    XiaoCommunityName = table.Column<string>(nullable: true),
                    XiaoCommunityNameeCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Normalization_Info", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "OndutyClaims_Info",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EndTime = table.Column<DateTime>(nullable: true),
                    IsReportNum = table.Column<int>(nullable: false),
                    Normalization_InfoId = table.Column<int>(nullable: true),
                    StartTime = table.Column<DateTime>(nullable: true),
                    TotalReportNum = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OndutyClaims_Info", x => x.id);
                    table.ForeignKey(
                        name: "FK_OndutyClaims_Info_Normalization_Info_Normalization_InfoId",
                        column: x => x.Normalization_InfoId,
                        principalTable: "Normalization_Info",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MydutyClaim_Info",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimTime = table.Column<DateTime>(nullable: true),
                    EndDutyTime = table.Column<DateTime>(nullable: true),
                    Normalization_InfoId = table.Column<int>(nullable: true),
                    OndutyClaims_Infoid = table.Column<int>(nullable: true),
                    StartDutyTime = table.Column<DateTime>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    Userid = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MydutyClaim_Info", x => x.id);
                    table.ForeignKey(
                        name: "FK_MydutyClaim_Info_Normalization_Info_Normalization_InfoId",
                        column: x => x.Normalization_InfoId,
                        principalTable: "Normalization_Info",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MydutyClaim_Info_OndutyClaims_Info_OndutyClaims_Infoid",
                        column: x => x.OndutyClaims_Infoid,
                        principalTable: "OndutyClaims_Info",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MydutyClaim_Info_Normalization_InfoId",
                table: "MydutyClaim_Info",
                column: "Normalization_InfoId");

            migrationBuilder.CreateIndex(
                name: "IX_MydutyClaim_Info_OndutyClaims_Infoid",
                table: "MydutyClaim_Info",
                column: "OndutyClaims_Infoid");

            migrationBuilder.CreateIndex(
                name: "IX_OndutyClaims_Info_Normalization_InfoId",
                table: "OndutyClaims_Info",
                column: "Normalization_InfoId");
        }
    }
}
