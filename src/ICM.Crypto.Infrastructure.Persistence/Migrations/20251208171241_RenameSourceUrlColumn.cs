using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ICM.Crypto.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RenameSourceUrlColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SourceUrl",
                table: "blockchain_snapshots",
                newName: "Source");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Source",
                table: "blockchain_snapshots",
                newName: "SourceUrl");
        }
    }
}
