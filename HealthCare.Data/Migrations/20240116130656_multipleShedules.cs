using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthCare.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class multipleShedules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staff_Schedule_ScheduleId",
                table: "Staff");

            migrationBuilder.DropIndex(
                name: "IX_Staff_ScheduleId",
                table: "Staff");

            migrationBuilder.DropColumn(
                name: "ScheduleId",
                table: "Staff");

            migrationBuilder.AddColumn<int>(
                name: "StaffId",
                table: "Schedule",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_StaffId",
                table: "Schedule",
                column: "StaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_Staff_StaffId",
                table: "Schedule",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_Staff_StaffId",
                table: "Schedule");

            migrationBuilder.DropIndex(
                name: "IX_Schedule_StaffId",
                table: "Schedule");

            migrationBuilder.DropColumn(
                name: "StaffId",
                table: "Schedule");

            migrationBuilder.AddColumn<int>(
                name: "ScheduleId",
                table: "Staff",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Staff_ScheduleId",
                table: "Staff",
                column: "ScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Staff_Schedule_ScheduleId",
                table: "Staff",
                column: "ScheduleId",
                principalTable: "Schedule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
