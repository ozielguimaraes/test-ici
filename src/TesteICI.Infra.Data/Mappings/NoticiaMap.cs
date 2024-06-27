using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteICI.Domain.Entities;

namespace TesteICI.Infra.Data.Mappings;

public class NoticiaMap : IEntityTypeConfiguration<Noticia>
{
    public void Configure(EntityTypeBuilder<Noticia> builder)
    {
        builder.HasKey(c => c.NoticiaId);
        builder.Property(c => c.NoticiaId)
            .HasColumnName("Id");

        builder.Property(c => c.Titulo)
            .HasColumnType("varchar(250)")
            .IsRequired();

        builder.Property(c => c.Texto)
            .HasColumnType("varchar(max)")//tipo text está depreciado
            .IsRequired();

        builder.Property(c => c.UsuarioId)
            .IsRequired();

        builder.HasMany(c => c.Tags)
            .WithOne(c => c.Noticia)
            .HasForeignKey(c => c.NoticiaId);
    }
}
