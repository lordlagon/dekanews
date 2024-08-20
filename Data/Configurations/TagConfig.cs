namespace DekaNews.Data.Configurations;

public class TagConfig : IEntityTypeConfiguration<Tag>
{ 
    public void Configure(EntityTypeBuilder<Tag> builder) 
    { 
        builder.ToTable("Tags"); 
        builder.HasKey(c => c.Id); 
        builder.Property(c => c.Descricao).HasColumnType("VARCHAR(100)").IsRequired();
    }
}