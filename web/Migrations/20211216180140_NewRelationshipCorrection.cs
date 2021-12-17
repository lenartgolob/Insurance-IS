using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web.Migrations
{
    public partial class NewRelationshipCorrection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "InsuranceTypeID",
                table: "Insurance subject type",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Insurance subject type_InsuranceTypeID",
                table: "Insurance subject type",
                column: "InsuranceTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Insurance subject type_Insurance type_InsuranceTypeID",
                table: "Insurance subject type",
                column: "InsuranceTypeID",
                principalTable: "Insurance type",
                principalColumn: "InsuranceTypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Insurance subject type_Insurance type_InsuranceTypeID",
                table: "Insurance subject type");

            migrationBuilder.DropIndex(
                name: "IX_Insurance subject type_InsuranceTypeID",
                table: "Insurance subject type");

            migrationBuilder.DropColumn(
                name: "InsuranceTypeID",
                table: "Insurance subject type");

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
    }
}
