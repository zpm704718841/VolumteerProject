using Microsoft.EntityFrameworkCore.Migrations;

namespace Dtol.Migrations
{
    public partial class edittablenew2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MydutyClaim_Info_Normalization_Info_Normalization_InfoId",
                table: "MydutyClaim_Info");

            migrationBuilder.DropForeignKey(
                name: "FK_MydutyClaim_Info_OndutyClaims_Info_OndutyClaims_Infoid",
                table: "MydutyClaim_Info");

            migrationBuilder.DropIndex(
                name: "IX_MydutyClaim_Info_Normalization_InfoId",
                table: "MydutyClaim_Info");

            migrationBuilder.DropColumn(
                name: "Normalization_InfoId",
                table: "MydutyClaim_Info");

            migrationBuilder.RenameColumn(
                name: "OndutyClaims_Infoid",
                table: "MydutyClaim_Info",
                newName: "OndutyClaims_InfoId");

            migrationBuilder.RenameIndex(
                name: "IX_MydutyClaim_Info_OndutyClaims_Infoid",
                table: "MydutyClaim_Info",
                newName: "IX_MydutyClaim_Info_OndutyClaims_InfoId");

            migrationBuilder.AddColumn<string>(
                name: "Subdistrict",
                table: "OndutyClaims_Info",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubdistrictID",
                table: "OndutyClaims_Info",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MydutyClaim_Info_OndutyClaims_Info_OndutyClaims_InfoId",
                table: "MydutyClaim_Info",
                column: "OndutyClaims_InfoId",
                principalTable: "OndutyClaims_Info",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MydutyClaim_Info_OndutyClaims_Info_OndutyClaims_InfoId",
                table: "MydutyClaim_Info");

            migrationBuilder.DropColumn(
                name: "Subdistrict",
                table: "OndutyClaims_Info");

            migrationBuilder.DropColumn(
                name: "SubdistrictID",
                table: "OndutyClaims_Info");

            migrationBuilder.RenameColumn(
                name: "OndutyClaims_InfoId",
                table: "MydutyClaim_Info",
                newName: "OndutyClaims_Infoid");

            migrationBuilder.RenameIndex(
                name: "IX_MydutyClaim_Info_OndutyClaims_InfoId",
                table: "MydutyClaim_Info",
                newName: "IX_MydutyClaim_Info_OndutyClaims_Infoid");

            migrationBuilder.AddColumn<string>(
                name: "Normalization_InfoId",
                table: "MydutyClaim_Info",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MydutyClaim_Info_Normalization_InfoId",
                table: "MydutyClaim_Info",
                column: "Normalization_InfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_MydutyClaim_Info_Normalization_Info_Normalization_InfoId",
                table: "MydutyClaim_Info",
                column: "Normalization_InfoId",
                principalTable: "Normalization_Info",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MydutyClaim_Info_OndutyClaims_Info_OndutyClaims_Infoid",
                table: "MydutyClaim_Info",
                column: "OndutyClaims_Infoid",
                principalTable: "OndutyClaims_Info",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
