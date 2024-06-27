using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TesteICI.Domain.Entities;
using TesteICI.Infra.Data.Mappings;

namespace TesteICI.Infra.Data.Context
{
    public class MainContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public MainContext(DbContextOptions<MainContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Tag> Tags { get; set; }
        public DbSet<Noticia> Noticias { get; set; }
        public DbSet<NoticiaTag> NoticiaTags { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TagMap());
            modelBuilder.ApplyConfiguration(new NoticiaMap());
            modelBuilder.ApplyConfiguration(new NoticiaTagMap());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"),
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null);
                }
            );
            optionsBuilder.LogTo(Console.WriteLine);
        }
    }
}
