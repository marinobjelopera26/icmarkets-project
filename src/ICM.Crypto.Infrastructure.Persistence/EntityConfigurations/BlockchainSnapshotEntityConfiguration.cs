using ICM.Crypto.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ICM.Crypto.Infrastructure.Persistence.EntityConfigurations;

internal sealed class BlockchainSnapshotEntityConfiguration
    : IEntityTypeConfiguration<BlockchainSnapshotEntity>
{
    public void Configure(EntityTypeBuilder<BlockchainSnapshotEntity> builder)
    {
        builder.ToTable("blockchain_snapshots");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever();
        
        builder.Property(x => x.Blockchain)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(x => x.Source)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(x => x.RawJson)
            .IsRequired()
            .HasColumnType("jsonb");

        builder.Property(x => x.CreatedAtUtc)
            .IsRequired();

        builder.HasIndex(x => new { x.Blockchain, x.CreatedAtUtc })
            .HasDatabaseName("ix_snapshots_blockchain_createdat");

        builder.HasIndex(x => x.RawJson)
            .HasMethod("gin")
            .HasDatabaseName("ix_snapshots_rawjson_gin");
    }
}