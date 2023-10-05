using Microsoft.AspNetCore.Mvc;
using ServerB.Data.Services;
using ServerB.Data.Models;
using System;
using System.Threading.Tasks;

namespace ServerB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgressNoteController : Controller
    {
        private readonly ProgressNoteService _progressNoteService;
        public ProgressNoteController(ProgressNoteService progressNoteService)
        {
            _progressNoteService = progressNoteService;
        }

        [HttpGet("sync-data-progressNote")]
        public async Task<IActionResult> SyncDataProgressNote()
        {
            try
            {
                await _progressNoteService.SyncData();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while getting all the progressNote: " + ex.Message);
            }
        }
    }
}

