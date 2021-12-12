using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web.Migrations
{
    public partial class PolicyOwner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Insurance policy",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Insurance policy_OwnerId",
                table: "Insurance policy",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Insurance policy_AspNetUsers_OwnerId",
                table: "Insurance policy",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Insurance policy_AspNetUsers_OwnerId",
                table: "Insurance policy");

            migrationBuilder.DropIndex(
                name: "IX_Insurance policy_OwnerId",
                table: "Insurance policy");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Insurance policy");
        }
    }
}
