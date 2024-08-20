namespace DekaNews.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext(options)
{
    public DbSet<Tag> Tags { get; set; }
    public DbSet<NoticiaTag> NoticiaTags { get; set; }
    public DbSet<Noticia> Noticias { get; set; }

    private static readonly ILoggerFactory _logger = LoggerFactory
        .Create(p => p
            .AddJsonConsole());

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseLoggerFactory(_logger)
            .UseSqlServer("Server=localhost,1433;Database=DekaNews;User Id=SA;Password=SU2orange;Encrypt=False",
                p=> p
                    .EnableRetryOnFailure(
                        maxRetryCount:2, 
                        maxRetryDelay: TimeSpan.FromSeconds(5), 
                        errorNumbersToAdd:null)
            );
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
    void MapearPropriedadesEsquecidas( ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            var properties = entity
                .GetProperties()
                .Where(p => p.ClrType == typeof(string));
            foreach (var property in properties)
            {
                if (string.IsNullOrEmpty(property.GetColumnType())
                    && !property.GetMaxLength().HasValue)
                {
                    property.SetColumnType("VARCHAR(100)");
                }
            }
        }
    }
}