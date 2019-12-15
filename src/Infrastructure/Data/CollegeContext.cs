using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;
using System.Reflection;

namespace Infrastructure.Data
{
    public class CollegeContext : DbContext
    {
        public CollegeContext(DbContextOptions<CollegeContext> options) : base(options)
        {
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<College> Colleges { get; set; }
        public DbSet<CollegeAppUser> CollegeAppUsers { get; set; }
        public DbSet<HomeWorkAssignment> HomeWorkAssignments { get; set; }
        public DbSet<HomeWorkAssignmentItem> HomeWorkAssignmentItems { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleClaim> RoleClaims { get; set; }
        public DbSet<SubmittedHomeWork> SubmittedHomeWorks { get; set; }
        public DbSet<YearClass> YearClasses { get; set; }

        public DbSet<GoogleSpeechApiRequest> GoogleSpeechApiRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            foreach (IMutableEntityType entityType in builder.Model.GetEntityTypes())
            {
                entityType.SetTableName(entityType.DisplayName());
            }

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
