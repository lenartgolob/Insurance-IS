using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web.Migrations
{
    public partial class NewRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InsuranceTypeID",
                table: "Insurance subject",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Insurance subject_InsuranceTypeID",
                table: "Insurance subject",
                column: "InsuranceTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Insurance subject_Insurance type_InsuranceTypeID",
                table: "Insurance subject",
                column: "InsuranceTypeID",
                principalTable: "Insurance type",
                principalColumn: "InsuranceTypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Insurance subject_Insurance type_InsuranceTypeID",
                table: "Insurance subject");

            migrationBuilder.DropIndex(
                name: "IX_Insurance subject_InsuranceTypeID",
                table: "Insurance subject");

            migrationBuilder.DropColumn(
                name: "InsuranceTypeID",
                table: "Insurance subject");
        }
    }
}
