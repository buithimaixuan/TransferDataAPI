using Microsoft.AspNetCore.Mvc;
using ServerA.CustomExceptions;
using ServerA.Data.Services;
using ServerA.Data.ViewModels;
using System;
using System.Threading.Tasks;
using ServerA.Data.Interfaces;

namespace ServerA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgressNoteController : Controller
    {
        private readonly IProgressNoteService _progressNoteService;
        public ProgressNoteController(IProgressNoteService progressNoteService)
        {
            _progressNoteService = progressNoteService ?? throw new ArgumentNullException(nameof(progressNoteService));
        }

        [HttpGet("get-all-progressNote")]
        public async Task<IActionResult> GetAllProgressNoteAsync()
        {
            try
            {
                var progressNotes = await _progressNoteService.GetAllProgressNoteAsync();
                return Ok(progressNotes);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while getting all the progressNote: " + ex.Message);
            }
        }

        [HttpGet("get-progressNote-by-id/{id}")]
        public async Task<IActionResult> GetProgressNoteByIdAsync(int id)
        {
            try
            {
                var progressNote = await _progressNoteService.GetProgressNoteByIdAsync(id);
                if (progressNote == null)
                {
                    return NotFound(); // Return a 404 response if the resident is not found.
                }
                return Ok(progressNote);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while getting the progressNote: " + ex.Message);
            }
        }

        [HttpPost("add-progressNote")]
        public async Task<IActionResult> AddProgressNoteAsync([FromBody] ProgressNoteVM progressNote)
        {
            try
            {
                await _progressNoteService.AddProgressNoteAsync(progressNote);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while adding the progressNote: " + ex.Message);
            }
        }

        [HttpPut("update-progressNote-by-id/{id}")]
        public async Task<IActionResult> UpdateProgressNoteAsync(int id, [FromBody] ProgressNoteVM progressNote)
        {
            try
            {
                var updatedProgressNote = await _progressNoteService.UpdateProgressNoteAsync(id, progressNote);
                return Ok(updatedProgressNote);
            }
            catch (NotFoundRecordsException e)
            {
                Console.WriteLine(e.Message);
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while updating the progressNote: " + ex.Message);
            }
        }

        [HttpDelete("delete-progressNote-by-id/{id}")]
        public async Task<IActionResult> DeleteProgressNoteAsync(int id)
        {
            try
            {
                await _progressNoteService.DeleteProgressNoteAsync(id);
                return Ok();
            }
            catch (NotFoundRecordsException e)
            {
                Console.WriteLine(e.Message);
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while deleting the progressNote: " + ex.Message);
            }
        }
    }
}

