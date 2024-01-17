using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthCare.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class fixedRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_AspNetUsers_AccountId",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_Staff_AspNetUsers_AccountId",
                table: "Staff");

            migrationBuilder.DropIndex(
                name: "IX_Staff_AccountId",
                table: "Staff");

            migrationBuilder.DropIndex(
                name: "IX_Patients_AccountId",
                table: "Patients");

            migrationBuilder.AlterColumn<string>(
                name: "AccountId",
                table: "Staff",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AccountId",
                table: "Patients",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Staff_AccountId",
                table: "Staff",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_AccountId",
                table: "Patients",
                column: "AccountId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_AspNetUsers_AccountId",
                table: "Patients",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Staff_AspNetUsers_AccountId",
                table: "Staff",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_AspNetUsers_AccountId",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_Staff_AspNetUsers_AccountId",
                table: "Staff");

            migrationBuilder.DropIndex(
                name: "IX_Staff_AccountId",
                table: "Staff");

            migrationBuilder.DropIndex(
                name: "IX_Patients_AccountId",
                table: "Patients");

            migrationBuilder.AlterColumn<string>(
                name: "AccountId",
                table: "Staff",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "AccountId",
                table: "Patients",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_AccountId",
                table: "Staff",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_AccountId",
                table: "Patients",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_AspNetUsers_AccountId",
                table: "Patients",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Staff_AspNetUsers_AccountId",
                table: "Staff",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
