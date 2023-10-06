using Microsoft.AspNetCore.Mvc;
using ServerB.Data.Services;
using ServerB.Data.Models;
using System;
using System.Threading.Tasks;
using ServerB.Data.Interfaces;

namespace ServerB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResidentController : Controller
    {
        private readonly IResidentService _residentService;
        public ResidentController(IResidentService residentService)
        {
            _residentService = residentService ?? throw new ArgumentNullException(nameof(residentService));
        }

        [HttpGet("sync-data-resident")]
        public async Task<IActionResult> SyncDataResident()
        {
            try
            {
                await _residentService.SyncData();
                var residents = await _residentService.GetDataResidentsServerB();
                return Ok(residents);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while getting all the resident: " + ex.Message);
            }
        }
    }
}
