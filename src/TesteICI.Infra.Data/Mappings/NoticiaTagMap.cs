using TesteICI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TesteICI.Infra.Data.Mappings
{
    public class NoticiaTagMap : IEntityTypeConfiguration<NoticiaTag>
    {
        public void Configure(EntityTypeBuilder<NoticiaTag> builder)
        {
            builder.HasKey(c => c.NoticiaTagId);
            builder.Property(c => c.NoticiaTagId)
                .HasColumnName("Id");
        }
    }
}
