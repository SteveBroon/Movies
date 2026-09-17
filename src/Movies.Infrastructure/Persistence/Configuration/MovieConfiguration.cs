using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movies.Domain.Entities;

namespace Movies.Infrastructure.Persistence.Configuration
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.ToTable("Movies");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.Property(x => x.ReleaseDate)
                .HasColumnType("date");

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.Overview)
                .HasColumnType("nvarchar(max)");

            builder.Property(x => x.Popularity)
                .HasPrecision(12, 3);

            builder.Property(x => x.VoteCount);

            builder.Property(x => x.VoteAverage)
                .HasPrecision(3, 1);

            builder.Property(x => x.OriginalLanguage)
                .IsFixedLength()
                .HasMaxLength(2);

            builder.Property(x => x.PosterUrl)
                .HasMaxLength(500);  

            builder.HasIndex(x => x.Title)
                .HasDatabaseName("IX_Movies_Title");

            builder.HasIndex(x => x.ReleaseDate)
                .HasDatabaseName("IX_Movies_ReleaseDate");
        }
    }
}