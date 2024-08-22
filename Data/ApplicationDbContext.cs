namespace DekaNews.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext(options)
{
    public DbSet<Tag> Tags { get; set; } = default!;    
    public DbSet<Noticia> Noticias { get; set; } = default!;
}