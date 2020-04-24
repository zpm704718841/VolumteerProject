using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dtol.Migrations
{
    public partial class NormalizationClaim : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Normalization_Info",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CommunityName = table.Column<string>(nullable: true),
                    CommunityNameCode = table.Column<string>(nullable: true),
                    XiaoCommunityName = table.Column<string>(nullable: true),
                    XiaoCommunityNameeCode = table.Column<string>(nullable: true),
                    PointsEarned = table.Column<string>(nullable: true),
                    CheckInTime = table.Column<DateTime>(nullable: true),
                    CheckOutTime = table.Column<DateTime>(nullable: true),
                    ServiceContent = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Normalization_Info", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "VActivity_PublicShow",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    ContentID = table.Column<string>(nullable: true),
                    CommunityID = table.Column<string>(nullable: true),
                    Community = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    VID = table.Column<string>(nullable: true),
                    NickName = table.Column<string>(nullable: true),
                    Headimgurl = table.Column<string>(nullable: true),
                    Experience = table.Column<string>(nullable: true),
                    isPublic = table.Column<int>(nullable: false),
                    Memo = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    CreateUser = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    UpdateUser = table.Column<string>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    bak1 = table.Column<string>(nullable: true),
                    bak2 = table.Column<string>(nullable: true),
                    bak3 = table.Column<string>(nullable: true),
                    bak4 = table.Column<string>(nullable: true),
                    bak5 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VActivity_PublicShow", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "VActivity_PublicShow_GiveLike",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    PublicShowID = table.Column<string>(nullable: true),
                    VID = table.Column<string>(nullable: true),
                    NickName = table.Column<string>(nullable: true),
                    Headimgurl = table.Column<string>(nullable: true),
                    Memo = table.Column<string>(nullable: true),
                    CreateUser = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    UpdateUser = table.Column<string>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    bak1 = table.Column<string>(nullable: true),
                    bak2 = table.Column<string>(nullable: true),
                    bak3 = table.Column<string>(nullable: true),
                    bak4 = table.Column<string>(nullable: true),
                    bak5 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VActivity_PublicShow_GiveLike", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Volunteer_Message",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    VID = table.Column<string>(nullable: true),
                    VNO = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Contents = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    CreateUser = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    UpdateUser = table.Column<string>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    bak1 = table.Column<string>(nullable: true),
                    bak2 = table.Column<string>(nullable: true),
                    bak3 = table.Column<string>(nullable: true),
                    bak4 = table.Column<string>(nullable: true),
                    bak5 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Volunteer_Message", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "OndutyClaims_Info",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StartTime = table.Column<DateTime>(nullable: true),
                    EndTime = table.Column<DateTime>(nullable: true),
                    IsReportNum = table.Column<int>(nullable: false),
                    TotalReportNum = table.Column<int>(nullable: false),
                    Normalization_InfoId = table.Column<int>(nullable: true)
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
                    Userid = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    ClaimTime = table.Column<DateTime>(nullable: true),
                    StartDutyTime = table.Column<DateTime>(nullable: true),
                    EndDutyTime = table.Column<DateTime>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Normalization_InfoId = table.Column<int>(nullable: true),
                    OndutyClaims_Infoid = table.Column<int>(nullable: true)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MydutyClaim_Info");

            migrationBuilder.DropTable(
                name: "VActivity_PublicShow");

            migrationBuilder.DropTable(
                name: "VActivity_PublicShow_GiveLike");

            migrationBuilder.DropTable(
                name: "Volunteer_Message");

            migrationBuilder.DropTable(
                name: "OndutyClaims_Info");

            migrationBuilder.DropTable(
                name: "Normalization_Info");
        }
    }
}
