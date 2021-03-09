using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace airplane_ticketsystem.Models{

    public class EntityContext: DbContext
    {
        //public EntityContext(DbContextOptions<EntityContext> options):base(options){}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            optionsBuilder.UseMySQL("server=localhost;database=airplane_tickets/Ulogin;user=mateja;password=Mateja1997!");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EntityModel>(entity =>
            {
                entity.HasKey(e => e.username);
                entity.Property(e=> e.password);
                entity.Property(e =>e.type);
            });
        }
        public DbSet<EntityModel> login {get;set;}

    }
}