using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class TestMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsInMaintenance",
                table: "Rooms",
                newName: "InMaintenance");

            migrationBuilder.AddColumn<bool>(
                name: "HasGuest",
                table: "Rooms",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasGuest",
                table: "Rooms");

            migrationBuilder.RenameColumn(
                name: "InMaintenance",
                table: "Rooms",
                newName: "IsInMaintenance");
        }
    }
}
