using Microsoft.AspNetCore.Mvc;
using ServerA.Data.Services;
using ServerA.Data.ViewModels;
using ServerA.CustomExceptions;

namespace ServerA.Controllers
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

        [HttpPost("add-facility")]
        public async Task<IActionResult> AddFacilityAsync([FromBody] FacilityVM facility)
        {
            try
            {
                await _facilityService.AddFacilityAsync(facility);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while adding the facility: " + ex.Message);
            }
        }

        [HttpGet("get-all-facility")]
        public async Task<IActionResult> GetAllFacilityAsync()
        {
            try
            {
                var facilities = await _facilityService.GetAllFacilityAsync();
                return Ok(facilities);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while getting all the facility: " + ex.Message);
            }
        }

        [HttpGet("get-facility-by-id/{id}")]
        public async Task<IActionResult> GetFacilityByIdAsync(int id)
        {
            try
            {
                var facility = await _facilityService.GetFacilityByIdAsync(id);
                if (facility == null)
                {
                    return NotFound(); 
                }
                return Ok(facility);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while getting the facility: " + ex.Message);
            }
        }

        [HttpPut("update-facility-by-id/{id}")]
        public async Task<IActionResult> UpdateFacilityAsync(int id, [FromBody] FacilityVM facility)
        {
            try
            {
                var updatedFacility = await _facilityService.UpdateFacilityAsync(id, facility);
                return Ok(updatedFacility);
            }
            catch (NotFoundRecordsException e)
            {
                Console.WriteLine(e.Message);
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while updating the facility: " + ex.Message);
            }
        }

        [HttpDelete("delete-facility-by-id/{id}")]
        public async Task<IActionResult> DeleteFacilityAsync(int id)
        {
            try
            {
                await _facilityService.DeleteFacilityAsync(id);
                return Ok();
            }
            catch (NotFoundRecordsException e)
            {
                Console.WriteLine(e.Message);
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while deleting the facility: " + ex.Message);
            }
        }

    }
}

