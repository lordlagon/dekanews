namespace DekaNews.Data.Configurations;

public class NoticiaTagConfiguration : IEntityTypeConfiguration<NoticiaTag>
{
    public void Configure(EntityTypeBuilder<NoticiaTag> builder)
    {
        builder.ToTable("NoticiaTag");
        builder.HasKey(c => c.Id);
    }
}