using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmCompanyManagement.Server.Migrations
{
    /// <inheritdoc />
    public partial class update1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Bills",
                type: "NUMBER(1)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "NUMBER(10)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckOutTime",
                table: "Attendance",
                type: "TIMESTAMP(7)",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "INTERVAL DAY(8) TO SECOND(7)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckInTime",
                table: "Attendance",
                type: "TIMESTAMP(7)",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "INTERVAL DAY(8) TO SECOND(7)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Bills",
                type: "NUMBER(10)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "NUMBER(1)");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "CheckOutTime",
                table: "Attendance",
                type: "INTERVAL DAY(8) TO SECOND(7)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TIMESTAMP(7)",
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "CheckInTime",
                table: "Attendance",
                type: "INTERVAL DAY(8) TO SECOND(7)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TIMESTAMP(7)",
                oldNullable: true);
        }
    }
}
