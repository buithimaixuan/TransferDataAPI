using Microsoft.AspNetCore.Mvc;
using ServerB.Data.Services;
using ServerB.Data.Models;
using ServerB.Data.Interfaces;


namespace ServerB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacilityController : Controller
    {
        private readonly IFacilityService _facilityService;
        public FacilityController(IFacilityService facilityService)
        {
            _facilityService = facilityService ?? throw new ArgumentNullException(nameof(facilityService));
        }

        [HttpGet("sync-data-facility")]
        public async Task<IActionResult> SyncDataFacility()
        {
            try
            {
                await _facilityService.SyncData();
                var facilities = await _facilityService.GetDataFacilitiesServerB();
                return Ok(facilities);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while getting all the facility: " + ex.Message);
            }
        }
    }
}

