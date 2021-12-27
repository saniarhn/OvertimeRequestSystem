using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OvertimeRequestSystemAPI.Migrations
{
    public partial class ssecond : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "TB_T_Overtime");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "TB_T_Overtime",
                newName: "Date");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "TB_T_Overtime",
                newName: "StartDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "TB_T_Overtime",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
