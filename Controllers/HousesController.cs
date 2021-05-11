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
    public class HousesController : ControllerBase
    {
        private readonly HousesService _hservice;

        public HousesController(HousesService hservice)
        {
            _hservice = hservice;
        }

        [HttpGet]
        public ActionResult<IEnumerable<House>> GetAllHouses()
        {
            try
            {
                return Ok(_hservice.getAllHouses());
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<House> GetHouseById(int id)
        {
            try
            {
                return Ok(_hservice.getHouseById(id));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<House>> CreateHouse([FromBody] House newHouse)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                newHouse.CreatorId = userInfo.Id;
                newHouse.Creator = userInfo;
                return Ok(_hservice.createHouse(newHouse));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<House>> EditHouse(int id, [FromBody] House editHouse)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                editHouse.Id = id;
                editHouse.CreatorId = userInfo.Id;
                editHouse.Creator = userInfo;
                return Ok(_hservice.editHouse(editHouse));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<House>> DeleteHouse(int id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_hservice.deleteHouse(id, userInfo.Id));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }
    }
}