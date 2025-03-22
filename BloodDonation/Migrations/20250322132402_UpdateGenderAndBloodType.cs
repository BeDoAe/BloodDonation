using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodDonation.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGenderAndBloodType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastDonationDateTime",
                table: "Reciepents",
                newName: "LastRecieveDateTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastRecieveDateTime",
                table: "Reciepents",
                newName: "LastDonationDateTime");
        }
    }
}
