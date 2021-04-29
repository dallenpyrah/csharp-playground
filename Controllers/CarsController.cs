using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using csharp_playground.Models;
using csharp_playground.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace csharp_playground.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {

        private readonly CarsService _cservice;

        public CarsController(CarsService cservice)
        {
            _cservice = cservice;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Car>> GetAllCars()
        {
            try
            {
                return Ok(_cservice.GetAllCars());
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Car> GetOneCar(int id)
        {
            try
            {
                return Ok(_cservice.GetOneCar(id));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Car>> CreateOneCar([FromBody] Car newCar)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                newCar.CreatorId = userInfo.Id;
                newCar.Creator = userInfo;
                return Ok(_cservice.CreateOneCar(newCar));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]

        public async Task<ActionResult<Car>> EditOneCar(int id, [FromBody] Car editedCar)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                editedCar.CreatorId = userInfo.Id;
                editedCar.Id = id;
                editedCar.Creator = userInfo;
                return Ok(_cservice.EditOneCar(editedCar));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]

        public async Task<ActionResult<Car>> DeleteOneCar(int id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                _cservice.DeleteOneCar(id, userInfo.Id);
                return Ok("Car Deleted");
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);                
            }
        }

    }
}