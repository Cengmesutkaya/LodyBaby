using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntities.Models
{
    public class Db:DbContext
    {
        public Db()
        //:Data Source = 93.89.230.2; Initial Catalog = pulusmG8_herboy; Integrated Security = False; User ID = pulusmG8_herbot; Connect Timeout = 30; Encrypt=False;Packet Size = 8192
        : base(@"Data Source=.;Initial Catalog=LodyBaby;Integrated Security =False; User ID=sa;Password=sebboy")
        //: base(@"Data Source=93.89.230.2;Initial Catalog = pulusmG8_herboy;Integrated Security=False;User ID=pulusmG8_herbot;Password=Herboy123654;")
        {

        } 
        public DbSet<Category> Category { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<Email> Email { get; set; }
        public DbSet<Product> Product { get; set; } 
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<Member> Member { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Town> Town { get; set; }
        public DbSet<Offer> Offer { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
