using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movies.Domain.Entities;

namespace Movies.Infrastructure.Persistence.Configuration
{
    public class MovieGenreConfiguration : IEntityTypeConfiguration<MovieGenre>
    {
        public void Configure(EntityTypeBuilder<MovieGenre> builder)
        {
            builder.HasKey(x => new
            {
                x.MovieId,
                x.GenreId
            });

            builder.HasOne(x => x.Movie)
                .WithMany(x => x.MovieGenres)
                .HasForeignKey(x => x.MovieId);

            builder.HasOne(x => x.Genre)
                .WithMany(x => x.MovieGenres)
                .HasForeignKey(x => x.GenreId);
        }
    }
}