using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ICM.Crypto.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ModifyBlockchainSnapshotsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_snapshots_chain_createdat",
                table: "blockchain_snapshots");

            migrationBuilder.DropColumn(
                name: "ChainKey",
                table: "blockchain_snapshots");

            migrationBuilder.DropColumn(
                name: "HttpStatus",
                table: "blockchain_snapshots");

            migrationBuilder.DropColumn(
                name: "Network",
                table: "blockchain_snapshots");

            migrationBuilder.CreateIndex(
                name: "ix_snapshots_chain_createdat",
                table: "blockchain_snapshots",
                columns: new[] { "Blockchain", "CreatedAtUtc" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_snapshots_chain_createdat",
                table: "blockchain_snapshots");

            migrationBuilder.AddColumn<string>(
                name: "ChainKey",
                table: "blockchain_snapshots",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "HttpStatus",
                table: "blockchain_snapshots",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Network",
                table: "blockchain_snapshots",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "ix_snapshots_chain_createdat",
                table: "blockchain_snapshots",
                columns: new[] { "ChainKey", "CreatedAtUtc" });
        }
    }
}
