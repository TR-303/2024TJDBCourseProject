using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmCompanyManagement.Server.Migrations
{
    /// <inheritdoc />
    public partial class update3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BillId",
                table: "Projects",
                type: "NVARCHAR2(12)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BillId",
                table: "PhotoEquipments",
                type: "NVARCHAR2(12)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BillId",
                table: "FundingApplications",
                type: "NVARCHAR2(12)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BillId",
                table: "FinishedProducts",
                type: "NVARCHAR2(12)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BillId",
                table: "EquipmentLeases",
                type: "NVARCHAR2(12)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SalaryBillId",
                table: "Employees",
                type: "NVARCHAR2(12)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_BillId",
                table: "Projects",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoEquipments_BillId",
                table: "PhotoEquipments",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_FundingApplications_BillId",
                table: "FundingApplications",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_FinishedProducts_BillId",
                table: "FinishedProducts",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentLeases_BillId",
                table: "EquipmentLeases",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_SalaryBillId",
                table: "Employees",
                column: "SalaryBillId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Bills_SalaryBillId",
                table: "Employees",
                column: "SalaryBillId",
                principalTable: "Bills",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentLeases_Bills_BillId",
                table: "EquipmentLeases",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FinishedProducts_Bills_BillId",
                table: "FinishedProducts",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FundingApplications_Bills_BillId",
                table: "FundingApplications",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoEquipments_Bills_BillId",
                table: "PhotoEquipments",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Bills_BillId",
                table: "Projects",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Bills_SalaryBillId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentLeases_Bills_BillId",
                table: "EquipmentLeases");

            migrationBuilder.DropForeignKey(
                name: "FK_FinishedProducts_Bills_BillId",
                table: "FinishedProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_FundingApplications_Bills_BillId",
                table: "FundingApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_PhotoEquipments_Bills_BillId",
                table: "PhotoEquipments");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Bills_BillId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_BillId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_PhotoEquipments_BillId",
                table: "PhotoEquipments");

            migrationBuilder.DropIndex(
                name: "IX_FundingApplications_BillId",
                table: "FundingApplications");

            migrationBuilder.DropIndex(
                name: "IX_FinishedProducts_BillId",
                table: "FinishedProducts");

            migrationBuilder.DropIndex(
                name: "IX_EquipmentLeases_BillId",
                table: "EquipmentLeases");

            migrationBuilder.DropIndex(
                name: "IX_Employees_SalaryBillId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "BillId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "BillId",
                table: "PhotoEquipments");

            migrationBuilder.DropColumn(
                name: "BillId",
                table: "FundingApplications");

            migrationBuilder.DropColumn(
                name: "BillId",
                table: "FinishedProducts");

            migrationBuilder.DropColumn(
                name: "BillId",
                table: "EquipmentLeases");

            migrationBuilder.DropColumn(
                name: "SalaryBillId",
                table: "Employees");
        }
    }
}
