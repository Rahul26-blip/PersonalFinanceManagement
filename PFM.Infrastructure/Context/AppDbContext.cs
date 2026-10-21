using Microsoft.EntityFrameworkCore;
using PFM.Domain.Entites;

namespace PFM.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Debts> Debts { get; set; }
        public DbSet<Investment> Investments { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<SavingGole> SavingGoles { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }
        
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>()
                .ToTable("Accounts");

            modelBuilder.Entity<Budget>()
                .ToTable("Budgets");

            modelBuilder.Entity<Categories>()
                .ToTable("Categories");

            modelBuilder.Entity<Debts>()
                .ToTable("Debts");

            modelBuilder.Entity<Investment>()
                .ToTable("Investments");

            modelBuilder.Entity<Permission>()
                .ToTable("Permission");

            modelBuilder.Entity<Role>()
                .ToTable("Roles");

            modelBuilder.Entity<RolePermission>()
                .ToTable("RolePermissions");

            modelBuilder.Entity<SavingGole>()
                .ToTable("SavingsGoles");



            modelBuilder.Entity<Transaction>()
                .ToTable("Transactions");

            modelBuilder.Entity<User>()
                .ToTable("Users");

            modelBuilder.Entity<UserRole>()
                .ToTable("UserRoles");
        }

    }
}
