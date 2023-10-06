using Microsoft.EntityFrameworkCore;
using ServerA.Data.Models;
using ServerA.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServerA.CustomExceptions;
using ServerA.Data.Repository;

namespace ServerA.Data.Services
{
    public class FacilityService : IFacilityService
    {
        private readonly AppDbContext _context;

        public FacilityService(AppDbContext contextParam)
        {
            _context = contextParam;
        }

        public async Task AddFacilityAsync(FacilityVM facilityVM)
        {
            var facility = new Facility()
            {
                Name = facilityVM.Name,
                Address = facilityVM.Address
            };
            await _context.Facilities.AddAsync(facility);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Facility>> GetAllFacilityAsync()
        {
            var facilities = await _context.Facilities.ToListAsync();
            return facilities;
        }

        public async Task<Facility> GetFacilityByIdAsync(int id)
        {
            var facility = await _context.Facilities.FirstOrDefaultAsync(r => r.Id == id);
            return facility;
        }

        public async Task<Facility> UpdateFacilityAsync(int id, FacilityVM facilityVM)
        {
            var facility = await _context.Facilities.FirstOrDefaultAsync(f => f.Id == id);
            if (facility == null) throw new NotFoundRecordsException($"Facility with ID {id} not found!");

            facility.Name = facilityVM.Name;
            facility.Address = facilityVM.Address;
            _context.Facilities.Update(facility);
            _context.SaveChanges();
            
            return facility;
        }

        public async Task DeleteFacilityAsync(int id)
        {
            var facility = await _context.Facilities.FirstOrDefaultAsync(f => f.Id == id);
            if (facility == null) throw new NotFoundRecordsException($"Facility with ID {id} not found!");

            _context.Facilities.Remove(facility);
            _context.SaveChanges();
        }
    }
}
