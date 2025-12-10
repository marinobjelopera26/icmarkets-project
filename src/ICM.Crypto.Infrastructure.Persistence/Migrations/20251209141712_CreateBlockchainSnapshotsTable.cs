using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ICM.Crypto.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateBlockchainSnapshotsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "blockchain_snapshots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Blockchain = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Source = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    RawJson = table.Column<string>(type: "jsonb", nullable: false),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blockchain_snapshots", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_snapshots_blockchain_createdat",
                table: "blockchain_snapshots",
                columns: new[] { "Blockchain", "CreatedAtUtc" });

            migrationBuilder.CreateIndex(
                name: "ix_snapshots_rawjson_gin",
                table: "blockchain_snapshots",
                column: "RawJson")
                .Annotation("Npgsql:IndexMethod", "gin");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "blockchain_snapshots");
        }
    }
}
