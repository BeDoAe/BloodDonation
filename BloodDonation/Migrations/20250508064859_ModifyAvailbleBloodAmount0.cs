using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodDonation.Migrations
{
    /// <inheritdoc />
    public partial class ModifyAvailbleBloodAmount0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TotalBloodAmount",
                table: "Hospitals",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TotalBloodAmount",
                table: "Hospitals",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
