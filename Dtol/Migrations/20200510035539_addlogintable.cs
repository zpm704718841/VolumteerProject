using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dtol.Migrations
{
    public partial class addlogintable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoginType",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    type = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    days = table.Column<string>(nullable: true),
                    hours = table.Column<string>(nullable: true),
                    Memo = table.Column<string>(nullable: true),
                    status = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    bak1 = table.Column<string>(nullable: true),
                    bak2 = table.Column<string>(nullable: true),
                    bak3 = table.Column<string>(nullable: true),
                    bak4 = table.Column<string>(nullable: true),
                    bak5 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LoginType_Log",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    uid = table.Column<string>(nullable: true),
                    typeid = table.Column<string>(nullable: true),
                    status = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    bak1 = table.Column<string>(nullable: true),
                    bak2 = table.Column<string>(nullable: true),
                    bak3 = table.Column<string>(nullable: true),
                    bak4 = table.Column<string>(nullable: true),
                    bak5 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginType_Log", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UserLogin_Log",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    uid = table.Column<string>(nullable: true),
                    Action = table.Column<string>(nullable: true),
                    status = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    bak1 = table.Column<string>(nullable: true),
                    bak2 = table.Column<string>(nullable: true),
                    bak3 = table.Column<string>(nullable: true),
                    bak4 = table.Column<string>(nullable: true),
                    bak5 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogin_Log", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoginType");

            migrationBuilder.DropTable(
                name: "LoginType_Log");

            migrationBuilder.DropTable(
                name: "UserLogin_Log");
        }
    }
}
