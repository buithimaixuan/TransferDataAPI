using Microsoft.AspNetCore.Mvc;
using ServerA.Data.Services;
using ServerA.Data.ViewModels;

namespace ServerA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacilityController : Controller
    {
        public FacilityService _facilityService;
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
                    return NotFound(); // Return a 404 response if the resident is not found.
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
                if (updatedFacility == null)
                {
                    return NotFound(); // Return a 404 response if the resident is not found.
                }
                return Ok(updatedFacility);
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
                var isDeletedSuccess = await _facilityService.DeleteFacilityAsync(id);
                if (isDeletedSuccess)
                {
                    return Ok();
                }
                return NotFound(); // Return a 404 response if the resident is not found.
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while deleting the facility: " + ex.Message);
            }
        }

    }
}

