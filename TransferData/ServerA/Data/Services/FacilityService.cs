using Microsoft.EntityFrameworkCore;
using ServerA.Data.Models;
using ServerA.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; // Add this namespace for asynchronous operations

namespace ServerA.Data.Services
{
    public class FacilityService
    {
        private AppDbContext _context;

        public FacilityService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddFacilityAsync(FacilityVM facility)
        {
            var _facility = new Facility()
            {
                Name = facility.Name,
                Address = facility.Address
            };
            await _context.Facilities.AddAsync(_facility);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Facility>> GetAllFacilityAsync()
        {
            var _listFacility = await _context.Facilities.Include(x => x.Residents).ToListAsync();
            return _listFacility;
        }

        public async Task<Facility> GetFacilityByIdAsync(int id)
        {
            var _facility = await _context.Facilities.Include(x => x.Residents).FirstOrDefaultAsync(r => r.Id == id);
            return _facility;
        }

        public async Task<Facility> UpdateFacilityAsync(int id, FacilityVM facility)
        {
            var _facility = await _context.Facilities.FirstOrDefaultAsync(f => f.Id == id);
            if (_facility != null)
            {
                _facility.Name = facility.Name;
                _facility.Address = facility.Address;
                _context.Facilities.Update(_facility);
                _context.SaveChanges();
            }
            return _facility;
        }

        public async Task<bool> DeleteFacilityAsync(int id)
        {
            var _facility = await _context.Facilities.FirstOrDefaultAsync(f => f.Id == id);
            if (_facility != null)
            {
                _context.Facilities.Remove(_facility);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
