using CarInventory.Core.Data.Repositories;
using CarInventory.Core.Entities;

namespace CarInventory.Data.Repositories
{
    public class CarRepository : BaseRepository<Car>, ICarRepository
    {
        public CarRepository(IDbContext context) : base(context)
        {
        }
    }
}
