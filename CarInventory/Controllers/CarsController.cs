using AutoMapper;
using CarInventory.Core.Entities;
using CarInventory.Core.Services;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using System.Web.Http;
using CarInventory.Dto.Dtos;

namespace CarInventory.Controllers
{
    [Authorize]
    [RoutePrefix("api/Cars")]
    public class CarsController : ApiController
    {
        private readonly ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetCars()
        {
            List<Car> cars = await _carService.GetAllAsync();

            List<CarDto> carDtos = new List<CarDto>();
            
            Mapper.Map(cars, carDtos);
            
            return Ok(carDtos);
        }

        [Route("ById/{id:int}")]
        public async Task<IHttpActionResult> GetCar(int id)
        {
            Car car = await _carService.GetByIdAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            CarDto carDto = new CarDto();

            Mapper.Map(car, carDto);

            return Ok(carDto);
        }

        [HttpPut]
        public async Task<IHttpActionResult> PutCar(int id, CarDto carDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != carDto.CarId)
            {
                return BadRequest();
            }

            try
            {
                carDto.CarId = id;

                Car car = await _carService.GetByIdAsync(id);

                Mapper.Map(carDto, car);

                await _carService.UpdateAsync(car);
              
                return Ok(carDto);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> PostCar(CarDto carDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Car car = new Car();
            
            Mapper.Map(carDto, car);

            car.ApplicationUserId = User.Identity.GetUserId();

            car = await _carService.AddAsync(car);
            carDto.CarId = car.Id;

            return CreatedAtRoute("ApiRoute", new { id = carDto.CarId }, carDto);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeleteCar(int id)
        {
            Car car = await _carService.GetByIdAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            await _carService.DeleteAsync(car);

            return Ok(car);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _carService.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CarExists(int id)
        {
            return _carService.GetByIdAsync(id) != null;
        }
    }
}