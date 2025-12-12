using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ICM.Crypto.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedAtUtc",
                table: "blockchain_snapshots",
                newName: "CreatedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "blockchain_snapshots",
                newName: "CreatedAtUtc");
        }
    }
}
