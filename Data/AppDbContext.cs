using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Contact> Contacts => Set<Contact>();
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Incident> Incidents => Set<Incident>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>()
            .HasIndex(c => c.Email)
            .IsUnique();

        modelBuilder.Entity<Account>()
            .HasIndex(a => a.Name)
            .IsUnique();

        modelBuilder.Entity<Incident>()
            .HasKey(i => i.IncidentName);
    }
}
