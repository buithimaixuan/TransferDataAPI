using Microsoft.EntityFrameworkCore;
using ServerB.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServerB.Data.Interfaces;

namespace ServerB.Data.Services
{
    public class ResidentService : IResidentService
    {
        private readonly AppDbContext _context;

        public ResidentService(AppDbContext contextParam)
        {
            _context = contextParam;
        }

        //----
        public async Task<List<Resident>> GetDataResidentsServerA()
        {
            HttpClient httpClient = new HttpClient();

            //get data from ServerA
            var residents = await httpClient.GetFromJsonAsync<List<Resident>>("https://localhost:7296/api/Resident/get-all-resident");

            return residents;
        }

        public async Task<List<Resident>> GetDataResidentsServerB()
        {
            var residents = await _context.Residents.ToListAsync();
            return residents;
        }

        public async Task SyncData()
        {
            var dataServerA = await GetDataResidentsServerA();
            var dataServerB = await GetDataResidentsServerB();

            var addResidents = new List<Resident>();
            var updateResidents = new List<Resident>();
            var deleteResidents = new List<Resident>();


            foreach (var resident in dataServerA)
            {
                var isExisted = dataServerB.Any(c => c.Id == resident.Id);
                if (isExisted)
                {
                    updateResidents.Add(resident);
                    continue;
                }
                addResidents.Add(resident);
            }

            foreach (var resident in dataServerB)
            {
                var isExisted = dataServerA.Any(c => c.Id == resident.Id);
                if (!isExisted)
                {
                    deleteResidents.Add(resident);
                }
            }

            if (addResidents.Any())
            {
                _context.Residents.AddRange(addResidents);
            }

            if (updateResidents.Any())
            {
                _context.Residents.UpdateRange(updateResidents);
            }

            if (deleteResidents.Any())
            {
                _context.Residents.RemoveRange(deleteResidents);
            }

            await _context.SaveChangesAsync();
        }

        //----
    }
}
