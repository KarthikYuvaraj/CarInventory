using CarInventory.Core.Data;
using CarInventory.Core.Entities;
using CarInventory.Core.Services;

namespace CarInventory.Services.Services
{
    public class CarService : BaseService<Car>, ICarService
    {
        public CarService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
