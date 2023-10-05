using Microsoft.AspNetCore.Mvc;
using ServerB.Data.Services;
using ServerB.Data.ViewModels;
using ServerB.Data.Models;
using System;
using System.Threading.Tasks;

namespace ServerB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgressNoteController : Controller
    {
        public ProgressNoteService _progressNoteService;
        public ProgressNoteController(ProgressNoteService progressNoteService)
        {
            _progressNoteService = progressNoteService;
        }

        [HttpGet("get-all-progressNote")]
        public async Task<IActionResult> GetAllProgressNoteAsync()
        {
            try
            {
                var progressNote = await _progressNoteService.GetAllProgressNoteAsync();
                return Ok(progressNote);
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
        public async Task<IActionResult> AddProgressNoteAsync([FromBody] ProgressNote progressNote)
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

        [HttpPost("add-list-progressNote")]
        public async Task<IActionResult> AddListProgressNoteAsync([FromBody] List<ProgressNote> progressNotes)
        {
            try
            {
                await _progressNoteService.AddListProgressNoteAsync(progressNotes);
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
                if (updatedProgressNote == null)
                {
                    return NotFound(); // Return a 404 response if the resident is not found.
                }
                return Ok(updatedProgressNote);
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
                var isDeletedSuccess = await _progressNoteService.DeleteProgressNoteAsync(id);
                if (isDeletedSuccess)
                {
                    return Ok();
                }
                return NotFound(); // Return a 404 response if the resident is not found.
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while deleting the progressNote: " + ex.Message);
            }
        }


        [HttpDelete("delete-all-progressNote")]
        public IActionResult DeleteAllProgressNote()
        {
            try
            {
                _progressNoteService.DeleteAllProgressNote();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while deleting all the progressNote: " + ex.Message);
            }
        }
    }
}

