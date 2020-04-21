using Microsoft.EntityFrameworkCore.Migrations;

namespace Dtol.Migrations
{
    public partial class NewChangeClo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ActivityID",
                table: "VActivity_Relate_Type",
                newName: "ContentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContentID",
                table: "VActivity_Relate_Type",
                newName: "ActivityID");
        }
    }
}
