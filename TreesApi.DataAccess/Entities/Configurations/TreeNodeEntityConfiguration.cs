using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TreesApi.DataAccess.Entities.Configurations;

internal class TreeNodeEntityConfiguration : BaseEntityConfiguration<TreeNodeEntity>
{
    public override void Configure(EntityTypeBuilder<TreeNodeEntity> builder)
    {
        base.Configure(builder);
        
        builder.Property(e => e.Name)
            .IsRequired();

        builder.HasIndex(e => new { e.Name, e.ParentId })
            .IsUnique()
            .AreNullsDistinct(false);

        builder.HasIndex(e => new { e.ParentId });

        builder.HasOne(e => e.Parent)
            .WithMany(e => e.Children)
            .HasForeignKey(e => e.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Root)
            .WithMany()
            .HasForeignKey(e => e.RootId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
