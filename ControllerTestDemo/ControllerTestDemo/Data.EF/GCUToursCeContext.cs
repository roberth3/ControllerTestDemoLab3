using System.Data.Entity;
using ControllerTestDemo.Domain;

namespace ControllerTestDemo.Data.EF
{
    public class GCUToursCeContext : DbContext
    {
        public GCUToursCeContext()
            : base("name = gcutourswmEntities")
        { 
            this.Configuration.LazyLoadingEnabled = false; 
        }

        public DbSet<User> Users { get; set; }
    }
}