using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Voyager.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_06 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReviewCount",
                table: "Hotels",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReviewCount",
                table: "Hotels");
        }
    }
}
