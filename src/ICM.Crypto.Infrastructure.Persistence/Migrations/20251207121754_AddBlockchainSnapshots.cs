using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ICM.Crypto.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddBlockchainSnapshots : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "blockchain_snapshots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ChainKey = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Blockchain = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Network = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    SourceUrl = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    RawJson = table.Column<string>(type: "jsonb", nullable: false),
                    HttpStatus = table.Column<int>(type: "integer", nullable: false),
                    DurationMs = table.Column<int>(type: "integer", nullable: false),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blockchain_snapshots", x => x.Id);
                    table.CheckConstraint("chk_snapshots_duration", "\"DurationMs\" >= 0");
                    table.CheckConstraint("chk_snapshots_httpstatus", "\"HttpStatus\" >= 100 AND \"HttpStatus\" <= 599");
                });

            migrationBuilder.CreateIndex(
                name: "ix_snapshots_chain_createdat",
                table: "blockchain_snapshots",
                columns: new[] { "ChainKey", "CreatedAtUtc" });

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
