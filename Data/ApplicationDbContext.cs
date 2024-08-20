namespace DekaNews.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext(options)
{
    private static readonly ILoggerFactory _logger = LoggerFactory.Create(p => p.AddJsonConsole());

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
                    .MigrationsHistoryTable("DekaNews")
            );
    }
}