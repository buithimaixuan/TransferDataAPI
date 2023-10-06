using Microsoft.EntityFrameworkCore;
using ServerA.Data.Models;
using ServerA.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServerA.CustomExceptions;

namespace ServerA.Data.Services
{
    public class FacilityService
    {
        private AppDbContext context;

        public FacilityService(AppDbContext contextParam)
        {
            context = contextParam;
        }

        public async Task AddFacilityAsync(FacilityVM facilityVM)
        {
            var facility = new Facility()
            {
                Name = facilityVM.Name,
                Address = facilityVM.Address
            };
            await context.Facilities.AddAsync(facility);
            await context.SaveChangesAsync();
        }

        public async Task<List<Facility>> GetAllFacilityAsync()
        {
            var facilities = await context.Facilities.ToListAsync();
            return facilities;
        }

        public async Task<Facility> GetFacilityByIdAsync(int id)
        {
            var facility = await context.Facilities.FirstOrDefaultAsync(r => r.Id == id);
            return facility;
        }

        public async Task<Facility> UpdateFacilityAsync(int id, FacilityVM facilityVM)
        {
            var facility = await context.Facilities.FirstOrDefaultAsync(f => f.Id == id);
            if (facility == null) throw new NotFoundRecordsException($"Facility with ID {id} not found!");

            facility.Name = facilityVM.Name;
            facility.Address = facilityVM.Address;
            context.Facilities.Update(facility);
            context.SaveChanges();
            
            return facility;
        }

        public async Task DeleteFacilityAsync(int id)
        {
            var facility = await context.Facilities.FirstOrDefaultAsync(f => f.Id == id);
            if (facility == null) throw new NotFoundRecordsException($"Facility with ID {id} not found!");

            context.Facilities.Remove(facility);
            context.SaveChanges();
        }
    }
}
