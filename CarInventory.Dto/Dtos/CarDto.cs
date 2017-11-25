
using System;

namespace CarInventory.Dto.Dtos
{
    public class CarDto
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
