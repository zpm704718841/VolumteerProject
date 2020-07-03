using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dtol.Migrations
{
    public partial class addtables2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "bak1",
                table: "user_Depart",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "bak2",
                table: "user_Depart",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "bak3",
                table: "user_Depart",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "bak4",
                table: "user_Depart",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "bak5",
                table: "user_Depart",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "latitude",
                table: "user_Depart",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "longitude",
                table: "user_Depart",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CheckAddress",
                table: "MydutyClaim_Sign",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Checklatitude",
                table: "MydutyClaim_Sign",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Checklongitude",
                table: "MydutyClaim_Sign",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "bak1",
                table: "MydutyClaim_Sign",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "bak2",
                table: "MydutyClaim_Sign",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "bak3",
                table: "MydutyClaim_Sign",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "bak4",
                table: "MydutyClaim_Sign",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "bak5",
                table: "MydutyClaim_Sign",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "V_OpenidUnionid",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    openid = table.Column<string>(nullable: true),
                    unionid = table.Column<string>(nullable: true),
                    bak1 = table.Column<string>(nullable: true),
                    bak2 = table.Column<string>(nullable: true),
                    bak3 = table.Column<string>(nullable: true),
                    bak4 = table.Column<string>(nullable: true),
                    bak5 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_V_OpenidUnionid", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "V_OpenidUnionid");

            migrationBuilder.DropColumn(
                name: "bak1",
                table: "user_Depart");

            migrationBuilder.DropColumn(
                name: "bak2",
                table: "user_Depart");

            migrationBuilder.DropColumn(
                name: "bak3",
                table: "user_Depart");

            migrationBuilder.DropColumn(
                name: "bak4",
                table: "user_Depart");

            migrationBuilder.DropColumn(
                name: "bak5",
                table: "user_Depart");

            migrationBuilder.DropColumn(
                name: "latitude",
                table: "user_Depart");

            migrationBuilder.DropColumn(
                name: "longitude",
                table: "user_Depart");

            migrationBuilder.DropColumn(
                name: "CheckAddress",
                table: "MydutyClaim_Sign");

            migrationBuilder.DropColumn(
                name: "Checklatitude",
                table: "MydutyClaim_Sign");

            migrationBuilder.DropColumn(
                name: "Checklongitude",
                table: "MydutyClaim_Sign");

            migrationBuilder.DropColumn(
                name: "bak1",
                table: "MydutyClaim_Sign");

            migrationBuilder.DropColumn(
                name: "bak2",
                table: "MydutyClaim_Sign");

            migrationBuilder.DropColumn(
                name: "bak3",
                table: "MydutyClaim_Sign");

            migrationBuilder.DropColumn(
                name: "bak4",
                table: "MydutyClaim_Sign");

            migrationBuilder.DropColumn(
                name: "bak5",
                table: "MydutyClaim_Sign");
        }
    }
}
