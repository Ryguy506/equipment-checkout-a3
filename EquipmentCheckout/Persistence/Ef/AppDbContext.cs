using EquipmentCheckout.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EquipmentCheckout.Persistence.Ef;

public class AppDbContext : DbContext
{
	public DbSet<EquipmentItem> EquipmentItems => Set<EquipmentItem>();
	public DbSet<Borrower> Borrowers => Set<Borrower>();
	public DbSet<Loan> Loans => Set<Loan>();
    public DbSet<Hold> Holds => Set<Hold>();
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {

    }

    // TODO (students): add DbSet<T> properties for your entities here.
    // Example:
    // public DbSet<Domain.Entities.EquipmentItem> EquipmentItems => Set<Domain.Entities.EquipmentItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
		modelBuilder.Entity<Borrower>().ToTable("Borrowers");
		modelBuilder.Entity<EquipmentItem>().ToTable("EquipmentItems");
		modelBuilder.Entity<Loan>().ToTable("Loans");
        modelBuilder.Entity<Hold>().ToTable("Holds");

        modelBuilder.Entity<Borrower>()
			.HasIndex(b => b.StudentNumber)
			.IsUnique();
		// TODO (students): configure mappings and constraints here (or use EF conventions).
	}
}
