using IMDbWebAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace IMDbWebAPI.Data;

public class ApplicationDbContext : DbContext
{
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
     : base(options)
  { }

  public DbSet<Movie> Movie { get; set; }
}