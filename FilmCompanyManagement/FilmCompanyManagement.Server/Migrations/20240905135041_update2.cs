using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmCompanyManagement.Server.Migrations
{
    /// <inheritdoc />
    public partial class update2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentRepairs_PhotoEquipments_PhoteEquipmentId",
                table: "EquipmentRepairs");

            migrationBuilder.RenameColumn(
                name: "Count",
                table: "PhotoEquipments",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "PhoteEquipmentId",
                table: "EquipmentRepairs",
                newName: "PhotoEquipmentId");

            migrationBuilder.RenameIndex(
                name: "IX_EquipmentRepairs_PhoteEquipmentId",
                table: "EquipmentRepairs",
                newName: "IX_EquipmentRepairs_PhotoEquipmentId");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "PhotoEquipments",
                type: "NVARCHAR2(12)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "PhotoEquipments",
                type: "NUMBER(1)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SalaryStatus",
                table: "Employees",
                type: "NVARCHAR2(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "unprocessed");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoEquipments_EmployeeId",
                table: "PhotoEquipments",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentRepairs_PhotoEquipments_PhotoEquipmentId",
                table: "EquipmentRepairs",
                column: "PhotoEquipmentId",
                principalTable: "PhotoEquipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoEquipments_Employees_EmployeeId",
                table: "PhotoEquipments",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentRepairs_PhotoEquipments_PhotoEquipmentId",
                table: "EquipmentRepairs");

            migrationBuilder.DropForeignKey(
                name: "FK_PhotoEquipments_Employees_EmployeeId",
                table: "PhotoEquipments");

            migrationBuilder.DropIndex(
                name: "IX_PhotoEquipments_EmployeeId",
                table: "PhotoEquipments");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "PhotoEquipments");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "PhotoEquipments");

            migrationBuilder.DropColumn(
                name: "SalaryStatus",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "PhotoEquipments",
                newName: "Count");

            migrationBuilder.RenameColumn(
                name: "PhotoEquipmentId",
                table: "EquipmentRepairs",
                newName: "PhoteEquipmentId");

            migrationBuilder.RenameIndex(
                name: "IX_EquipmentRepairs_PhotoEquipmentId",
                table: "EquipmentRepairs",
                newName: "IX_EquipmentRepairs_PhoteEquipmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentRepairs_PhotoEquipments_PhoteEquipmentId",
                table: "EquipmentRepairs",
                column: "PhoteEquipmentId",
                principalTable: "PhotoEquipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
