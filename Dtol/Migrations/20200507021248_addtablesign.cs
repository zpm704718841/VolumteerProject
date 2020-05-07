using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dtol.Migrations
{
    public partial class addtablesign : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MydutyClaim_Sign",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    Userid = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    CheckTime = table.Column<DateTime>(nullable: true),
                    type = table.Column<string>(nullable: true),
                    CreateUser = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    UpdateUser = table.Column<string>(nullable: true),
                    OndutyClaims_InfoId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MydutyClaim_Sign", x => x.id);
                    table.ForeignKey(
                        name: "FK_MydutyClaim_Sign_OndutyClaims_Info_OndutyClaims_InfoId",
                        column: x => x.OndutyClaims_InfoId,
                        principalTable: "OndutyClaims_Info",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MydutyClaim_Sign_OndutyClaims_InfoId",
                table: "MydutyClaim_Sign",
                column: "OndutyClaims_InfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MydutyClaim_Sign");
        }
    }
}
