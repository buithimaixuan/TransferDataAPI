using Microsoft.EntityFrameworkCore;
using ServerB.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServerB.Data.Interfaces;

namespace ServerB.Data.Services
{
    public class FacilityService : IFacilityService
    {
        private readonly AppDbContext _context;

        public FacilityService(AppDbContext contextParam)
        {
            _context = contextParam;
        }

        //----
        public async Task<List<Facility>> GetDataFacilitiesServerA()
        {
            HttpClient httpClient = new HttpClient();

            //get data from ServerA
            var facilities = await httpClient.GetFromJsonAsync<List<Facility>>("https://localhost:7296/api/Facility/get-all-facility");

            return facilities;
        }

        public async Task<List<Facility>> GetDataFacilitiesServerB()
        {
            var facilities = await _context.Facilities.ToListAsync();
            return facilities;
        }

        public async Task SyncData()
        {
            var dataServerA = await GetDataFacilitiesServerA();
            var dataServerB = await GetDataFacilitiesServerB();

            var addFacilities = new List<Facility>();
            var updateFacilities = new List<Facility>();
            var deleteFacilities = new List<Facility>();


            foreach (var facility in dataServerA)
            {
                var isExisted = dataServerB.Any(c => c.Id == facility.Id);
                if (isExisted)
                {
                    updateFacilities.Add(facility);
                    continue;
                }
                addFacilities.Add(facility);
            }

            foreach (var facility in dataServerB)
            {
                var isExisted = dataServerA.Any(c => c.Id == facility.Id);
                if (!isExisted)
                {
                    deleteFacilities.Add(facility);
                }
            }

            if (addFacilities.Any())
            {
                _context.Facilities.AddRange(addFacilities);
            }

            if (updateFacilities.Any())
            {
                _context.Facilities.UpdateRange(updateFacilities);
            }

            if (deleteFacilities.Any())
            {
                _context.Facilities.RemoveRange(deleteFacilities);
            }

            await _context.SaveChangesAsync();
        }

        //----
    }
}
