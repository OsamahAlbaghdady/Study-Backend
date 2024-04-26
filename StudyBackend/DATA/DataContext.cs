using BackEndStructuer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackEndStructuer.DATA
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }


        public DbSet<AppUser> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

 
        public DbSet<Article> Articles { get; set; }
        
        public DbSet<Country> Countries { get; set; }
        public DbSet<Degree> Degrees { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<DegreeField> DegreeFields { get; set; }
        public DbSet<UniversityDegree> UniversityDegrees { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<Setting> Settings { get; set; }
        

        
        // here to add

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
            modelBuilder.Entity<RolePermission>().HasKey(rp => new { rp.RoleId, rp.PermissionId });

            
            // new DbInitializer(modelBuilder).Seed();

        }
        
    }
}