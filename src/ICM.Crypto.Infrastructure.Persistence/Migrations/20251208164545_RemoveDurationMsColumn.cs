using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ICM.Crypto.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDurationMsColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationMs",
                table: "blockchain_snapshots");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DurationMs",
                table: "blockchain_snapshots",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
