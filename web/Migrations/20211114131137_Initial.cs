using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Insurance type",
                columns: table => new
                {
                    InsuranceTypeID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insurance type", x => x.InsuranceTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Insured",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstMidName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insured", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Insurance policy",
                columns: table => new
                {
                    InsurancePolicyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsuranceTypeID = table.Column<int>(type: "int", nullable: false),
                    InsuredID = table.Column<int>(type: "int", nullable: false),
                    DateFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateTo = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insurance policy", x => x.InsurancePolicyID);
                    table.ForeignKey(
                        name: "FK_Insurance policy_Insurance type_InsuranceTypeID",
                        column: x => x.InsuranceTypeID,
                        principalTable: "Insurance type",
                        principalColumn: "InsuranceTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Insurance policy_Insured_InsuredID",
                        column: x => x.InsuredID,
                        principalTable: "Insured",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Insurance policy_InsuranceTypeID",
                table: "Insurance policy",
                column: "InsuranceTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Insurance policy_InsuredID",
                table: "Insurance policy",
                column: "InsuredID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Insurance policy");

            migrationBuilder.DropTable(
                name: "Insurance type");

            migrationBuilder.DropTable(
                name: "Insured");
        }
    }
}
