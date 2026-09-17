using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movies.Domain.Entities;

namespace Movies.Infrastructure.Persistence.Configuration
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.ToTable("Genres");

            builder.HasKey(x => x.Id);
        
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(40);
        
            builder.HasIndex(x => x.Name)
                .IsUnique();
        }
    }
}