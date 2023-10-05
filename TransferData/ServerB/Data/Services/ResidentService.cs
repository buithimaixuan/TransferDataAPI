using Microsoft.EntityFrameworkCore;
using ServerB.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerB.Data.Services
{
    public class ResidentService
    {
        private AppDbContext context;

        public ResidentService(AppDbContext contextParam)
        {
            context = contextParam;
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
            var residents = await context.Residents.ToListAsync();
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
            if (addResidents.Count > 0)
            {
                context.Residents.AddRange(addResidents);
            }
            if (updateResidents.Count > 0)
            {
                context.Residents.UpdateRange(updateResidents);
            }
            if (deleteResidents.Count > 0)
            {
                context.Residents.RemoveRange(deleteResidents);
            }
            await context.SaveChangesAsync();
        }

        //----
    }
}
