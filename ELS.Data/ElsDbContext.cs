using ElsModels.SQL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;



namespace ELS.Data
{
    public class ElsDbContext: IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        public ElsDbContext(DbContextOptions<ElsDbContext> options)
         : base(options)
        {
        }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Technician> Technicians { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Ticket>().HasOne(t=>t.Technician).WithMany(ti=>ti.Tickets).HasForeignKey(f=>f.TechnicianId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                        .HasOne(t => t.Equipment)
                        .WithMany(e => e.Tickets)
                        .HasForeignKey(t => t.EquipmentId)
                        .OnDelete(DeleteBehavior.Cascade);
                
            modelBuilder.Entity<Equipment>().HasOne(x => x.Category).WithMany(c => c.Equipments).HasForeignKey(f => f.CategoryId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Report>().HasOne(r => r.Technician)
           .WithMany(t => t.Reports)               
           .HasForeignKey(r => r.TechnicianId)   
           .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
