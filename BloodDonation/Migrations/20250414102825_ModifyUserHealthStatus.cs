using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodDonation.Migrations
{
    /// <inheritdoc />
    public partial class ModifyUserHealthStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserHealthStatuses_AspNetUsers_ApplicationUserId",
                table: "UserHealthStatuses");

            migrationBuilder.DropIndex(
                name: "IX_UserHealthStatuses_ApplicationUserId",
                table: "UserHealthStatuses");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "UserHealthStatuses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "UserHealthStatuses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserHealthStatuses_ApplicationUserId",
                table: "UserHealthStatuses",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserHealthStatuses_AspNetUsers_ApplicationUserId",
                table: "UserHealthStatuses",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
