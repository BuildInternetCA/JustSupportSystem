using Microsoft.EntityFrameworkCore;

namespace JustSupportSystem.Models
{
    public class JDBContext : DbContext
    {
        public JDBContext(DbContextOptions<JDBContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<UserAccount>? UserAccounts { get; set; }

        public DbSet<Company>? Companies { get; set; }
        public DbSet<CustomFieldMaster>? CustomFieldMasters { get; set; }
        public DbSet<SupportTicketLog>? SupportTicketLogs { get; set; }
        public DbSet<SupportTicketType>? SupportTicketTypes { get; set; }
        public DbSet<SupportTicketCustomField>? SupportTicketCustomFields { get; set; }
        public DbSet<SupportTicket>? SupportTickets { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           
            modelBuilder.Entity<UserAccount>()
                .HasOne(u => u.Company)
                .WithMany(c => c.UserAccounts)
                .HasForeignKey(u => u.CompanyId);

            modelBuilder.Entity<Company>()
               .HasOne(u => u.DefaultAgent)
                .WithMany(c => c.Companies)
               .HasForeignKey(u => u.DefaultAgentId);

        }
    }
}
