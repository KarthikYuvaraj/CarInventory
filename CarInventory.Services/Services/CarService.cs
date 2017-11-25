using CarInventory.Core.Data;
using CarInventory.Core.Entities;
using CarInventory.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
