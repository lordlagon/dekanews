namespace DekaNews.Data.Configurations;

public class NoticiaConfiguration : IEntityTypeConfiguration<Noticia>
{
    public void Configure(EntityTypeBuilder<Noticia> builder)
    {
        builder.ToTable("Noticias");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Texto);
        builder.Property(c => c.Titulo).HasColumnType("VARCHAR(250)");
    }
}