using Microsoft.AspNetCore.Mvc;
using ServerB.Data.Services;
using ServerB.Data.Models;
using System;
using System.Threading.Tasks;

namespace ServerB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResidentController : Controller
    {
        private readonly ResidentService _residentService;
        public ResidentController(ResidentService residentService)
        {
            _residentService = residentService;
        }

        [HttpGet("sync-data-resident")]
        public async Task<IActionResult> SyncDataResident()
        {
            try
            {
                await _residentService.SyncData();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while getting all the resident: " + ex.Message);
            }
        }
    }
}
