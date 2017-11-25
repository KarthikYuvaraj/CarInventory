using CarInventory.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace CarInventory.Data.Configurations
{
    public class CarConfiguration : EntityTypeConfiguration<Car>
    {
        public CarConfiguration()
        {
            Property(c => c.Brand).IsRequired().HasMaxLength(100);
            Property(c => c.Model).IsRequired().HasMaxLength(100);
        }
    }
}
