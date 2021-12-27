using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OvertimeRequestSystemAPI.Migrations
{
    public partial class sfirst : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_M_Parameter",
                columns: table => new
                {
                    ParameterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParameterName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Parameter", x => x.ParameterId);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_Role",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Role", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "TB_T_Employee",
                columns: table => new
                {
                    NIP = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BasicSalary = table.Column<float>(type: "real", nullable: false),
                    ManagerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_T_Employee", x => x.NIP);
                });

            migrationBuilder.CreateTable(
                name: "TB_T_Account",
                columns: table => new
                {
                    NIP = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountStatus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_T_Account", x => x.NIP);
                    table.ForeignKey(
                        name: "FK_TB_T_Account_TB_T_Employee_NIP",
                        column: x => x.NIP,
                        principalTable: "TB_T_Employee",
                        principalColumn: "NIP",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_T_Overtime",
                columns: table => new
                {
                    OvertimeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SumOvertimeHour = table.Column<int>(type: "int", nullable: false),
                    OvertimeSalary = table.Column<float>(type: "real", nullable: false),
                    StatusByManager = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusByFinance = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NIP = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_T_Overtime", x => x.OvertimeId);
                    table.ForeignKey(
                        name: "FK_TB_T_Overtime_TB_T_Employee_NIP",
                        column: x => x.NIP,
                        principalTable: "TB_T_Employee",
                        principalColumn: "NIP",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_T_AccountRole",
                columns: table => new
                {
                    AccountRoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NIP = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_T_AccountRole", x => x.AccountRoleId);
                    table.ForeignKey(
                        name: "FK_TB_T_AccountRole_TB_M_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "TB_M_Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_T_AccountRole_TB_T_Account_NIP",
                        column: x => x.NIP,
                        principalTable: "TB_T_Account",
                        principalColumn: "NIP",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_Response",
                columns: table => new
                {
                    ResponseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResponseDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManagerOrFinanceId = table.Column<int>(type: "int", nullable: false),
                    OvertimeId = table.Column<int>(type: "int", nullable: false),
                    ManagerOfFinanceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Response", x => x.ResponseId);
                    table.ForeignKey(
                        name: "FK_TB_M_Response_TB_T_Employee_ManagerOfFinanceId",
                        column: x => x.ManagerOfFinanceId,
                        principalTable: "TB_T_Employee",
                        principalColumn: "NIP",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_M_Response_TB_T_Overtime_OvertimeId",
                        column: x => x.OvertimeId,
                        principalTable: "TB_T_Overtime",
                        principalColumn: "OvertimeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_T_OvertimeDetail",
                columns: table => new
                {
                    OvertimeDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartHour = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndHour = table.Column<TimeSpan>(type: "time", nullable: false),
                    TaskName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OvertimeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_T_OvertimeDetail", x => x.OvertimeDetailId);
                    table.ForeignKey(
                        name: "FK_TB_T_OvertimeDetail_TB_T_Overtime_OvertimeId",
                        column: x => x.OvertimeId,
                        principalTable: "TB_T_Overtime",
                        principalColumn: "OvertimeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Response_ManagerOfFinanceId",
                table: "TB_M_Response",
                column: "ManagerOfFinanceId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Response_OvertimeId",
                table: "TB_M_Response",
                column: "OvertimeId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_T_AccountRole_NIP",
                table: "TB_T_AccountRole",
                column: "NIP");

            migrationBuilder.CreateIndex(
                name: "IX_TB_T_AccountRole_RoleId",
                table: "TB_T_AccountRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_T_Employee_Email",
                table: "TB_T_Employee",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TB_T_Overtime_NIP",
                table: "TB_T_Overtime",
                column: "NIP");

            migrationBuilder.CreateIndex(
                name: "IX_TB_T_OvertimeDetail_OvertimeId",
                table: "TB_T_OvertimeDetail",
                column: "OvertimeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_M_Parameter");

            migrationBuilder.DropTable(
                name: "TB_M_Response");

            migrationBuilder.DropTable(
                name: "TB_T_AccountRole");

            migrationBuilder.DropTable(
                name: "TB_T_OvertimeDetail");

            migrationBuilder.DropTable(
                name: "TB_M_Role");

            migrationBuilder.DropTable(
                name: "TB_T_Account");

            migrationBuilder.DropTable(
                name: "TB_T_Overtime");

            migrationBuilder.DropTable(
                name: "TB_T_Employee");
        }
    }
}
