using Microsoft.AspNetCore.Mvc;
using ServerB.Data.Services;
using ServerB.Data.Models;
using System;
using System.Threading.Tasks;
using ServerB.Data.Repository;

namespace ServerB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgressNoteController : Controller
    {
        private readonly IProgressNoteService _progressNoteService;
        public ProgressNoteController(ProgressNoteService progressNoteService)
        {
            _progressNoteService = progressNoteService ?? throw new ArgumentNullException(nameof(progressNoteService)); ;
        }

        [HttpGet("sync-data-progressNote")]
        public async Task<IActionResult> SyncDataProgressNote()
        {
            try
            {
                await _progressNoteService.SyncData();
                var progressNotes = await _progressNoteService.GetDataProgressNotesServerB();
                return Ok(progressNotes);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while getting all the progressNote: " + ex.Message);
            }
        }
    }
}

