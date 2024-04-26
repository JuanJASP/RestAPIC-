using Trabalhinho.MusicasRotas;
using Microsoft.EntityFrameworkCore;

namespace Trabalhinho.MusicasRotas;

public class AppDbContext : DbContext{
    public DbSet<Artista> Artistas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(connectionString:"Data Source=music.sqlite");
        base.OnConfiguring(optionsBuilder);
    }

}