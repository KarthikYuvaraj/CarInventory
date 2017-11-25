using System;

namespace CarInventory.Core.Entities
{
    public class Car : BaseEntity
    {
        public int CarId { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public Decimal Price { get; set; }

        public Boolean NewCar { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
