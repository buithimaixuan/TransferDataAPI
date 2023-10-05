using Microsoft.AspNetCore.Mvc;
using ServerB.Data.Services;
using ServerB.Data.Models;


namespace ServerB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacilityController : Controller
    {
        private readonly FacilityService _facilityService;
        public FacilityController(FacilityService facilityService)
        {
            _facilityService = facilityService;
        }

        [HttpGet("sync-data-facility")]
        public async Task<IActionResult> SyncDataFacility()
        {
            try
            {
                await _facilityService.SyncData();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while getting all the facility: " + ex.Message);
            }
        }
    }
}

