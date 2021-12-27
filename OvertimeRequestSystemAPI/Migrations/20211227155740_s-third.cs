using Microsoft.EntityFrameworkCore.Migrations;

namespace OvertimeRequestSystemAPI.Migrations
{
    public partial class sthird : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_Response_TB_T_Employee_ManagerOfFinanceId",
                table: "TB_M_Response");

            migrationBuilder.DropIndex(
                name: "IX_TB_M_Response_ManagerOfFinanceId",
                table: "TB_M_Response");

            migrationBuilder.DropColumn(
                name: "ManagerOfFinanceId",
                table: "TB_M_Response");

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Response_ManagerOrFinanceId",
                table: "TB_M_Response",
                column: "ManagerOrFinanceId");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_Response_TB_T_Employee_ManagerOrFinanceId",
                table: "TB_M_Response",
                column: "ManagerOrFinanceId",
                principalTable: "TB_T_Employee",
                principalColumn: "NIP",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_Response_TB_T_Employee_ManagerOrFinanceId",
                table: "TB_M_Response");

            migrationBuilder.DropIndex(
                name: "IX_TB_M_Response_ManagerOrFinanceId",
                table: "TB_M_Response");

            migrationBuilder.AddColumn<int>(
                name: "ManagerOfFinanceId",
                table: "TB_M_Response",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Response_ManagerOfFinanceId",
                table: "TB_M_Response",
                column: "ManagerOfFinanceId");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_Response_TB_T_Employee_ManagerOfFinanceId",
                table: "TB_M_Response",
                column: "ManagerOfFinanceId",
                principalTable: "TB_T_Employee",
                principalColumn: "NIP",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
