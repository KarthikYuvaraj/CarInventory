using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarInventory.Dto.Dtos
{
    class CarDto
    {
        public CarDto()
        {

        }

        public int CarId { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public Decimal Price { get; set; }

        public Boolean NewCar { get; set; }
    }
}
