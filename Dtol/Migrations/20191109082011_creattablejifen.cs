using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dtol.Migrations
{
    public partial class creattablejifen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Volunteer_Score",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    ContentID = table.Column<string>(nullable: true),
                    tableName = table.Column<string>(nullable: true),
                    VID = table.Column<string>(nullable: true),
                    type = table.Column<string>(nullable: true),
                    Score = table.Column<int>(nullable: false),
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
                    table.PrimaryKey("PK_Volunteer_Score", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Volunteer_Score");
        }
    }
}
