using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteICI.Domain.Entities;

namespace TesteICI.Infra.Data.Mappings;

public class TagMap : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.HasKey(c => c.TagId);
        builder.Property(c => c.TagId)
            .HasColumnName("Id");

        builder.Property(c => c.Descricao)
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.HasMany(c => c.Tags)
            .WithOne(c => c.Tag)
            .HasForeignKey(c => c.TagId);
    }
}
