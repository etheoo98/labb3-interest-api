using InterestApi.Models;
using InterestApi.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace InterestApi.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Person> People { get; set; }
    public DbSet<Interest> Interests { get; set; }
    public DbSet<PersonInterest> PersonInterests { get; set; }
    public DbSet<InterestLink> InterestLinks { get; set; }
}