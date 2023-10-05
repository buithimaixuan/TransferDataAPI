﻿using Microsoft.AspNetCore.Mvc;
using ServerA.Data.Services;
using ServerA.Data.ViewModels;
using System;
using System.Threading.Tasks;

namespace ServerA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResidentController : Controller
    {
        public ResidentService _residentService;
        public ResidentController(ResidentService residentService)
        {
            _residentService = residentService;
        }

        [HttpGet("get-all-resident")]
        public async Task<IActionResult> GetAllResidentAsync()
        {
            try
            {
                var residents = await _residentService.GetAllResidentAsync();
                return Ok(residents);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while getting all the resident: " + ex.Message);
            }
        }

        [HttpGet("get-resident-by-id/{id}")]
        public async Task<IActionResult> GetResidentByIdAsync(int id)
        {
            try
            {
                var resident = await _residentService.GetResidentByIdAsync(id);
                if (resident == null)
                {
                    return NotFound(); // Return a 404 response if the resident is not found.
                }
                return Ok(resident);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while getting the resident: " + ex.Message);
            }
        }

        [HttpPost("add-resident")]
        public async Task<IActionResult> AddResidentAsync([FromBody] ResidentVM resident)
        {
            try
            {
                await _residentService.AddResidentAsync(resident);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while adding the resident: " + ex.Message);
            }
        }

        [HttpPut("update-resident-by-id/{id}")]
        public async Task<IActionResult> UpdateResidentAsync(int id, [FromBody] ResidentVM resident)
        {
            try
            {
                var updatedResident = await _residentService.UpdateResidentAsync(id, resident);
                if (updatedResident == null)
                {
                    return NotFound(); // Return a 404 response if the resident is not found.
                }
                return Ok(updatedResident);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while updating the resident: " + ex.Message);
            }
        }

        [HttpDelete("delete-resident-by-id/{id}")]
        public async Task<IActionResult> DeleteResidentAsync(int id)
        {
            try
            {
                var isDeletedSuccess = await _residentService.DeleteResidentAsync(id);
                if (isDeletedSuccess) {
                    return Ok();
                }
                return NotFound(); // Return a 404 response if the resident is not found.
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while deleting the resident: " + ex.Message);
            }
        }
    }
}
