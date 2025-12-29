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
        public DbSet<UserToken> UserTokens { get; set; }
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

    public static class JDatabaseExtensions
    {
        public static IQueryable<T> ListByPageNumber<T>(this IQueryable<T> data, int pageNo = 1, int pageSize = 12)
        {
            return data.OrderByDescending(p => (p as JDBBase).Id).Skip((pageNo - 1) * pageSize).Take(pageSize);
        }
    }
}
