using TesteICI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TesteICI.Infra.Data.Mappings
{
    public class NoticiaMap : IEntityTypeConfiguration<Noticia>
    {
        public void Configure(EntityTypeBuilder<Noticia> builder)
        {
            builder.HasKey(c => c.NoticiaId);
            builder.Property(c => c.NoticiaId)
                .HasColumnName("Id");
        }
    }
}
