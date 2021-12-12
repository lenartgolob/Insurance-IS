using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web.Migrations
{
    public partial class requiredAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Insurance type_AspNetUsers_OwnerId",
                table: "Insurance type");

            migrationBuilder.DropIndex(
                name: "IX_Insurance type_OwnerId",
                table: "Insurance type");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Insurance type");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Insured",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "Insured",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Insurance subtype",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Insured");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "Insured");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Insurance subtype");

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Insurance type",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Insurance type_OwnerId",
                table: "Insurance type",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Insurance type_AspNetUsers_OwnerId",
                table: "Insurance type",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
