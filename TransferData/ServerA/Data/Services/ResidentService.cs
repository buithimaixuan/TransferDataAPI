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
    public class ResidentService
    {
        private AppDbContext context;

        public ResidentService(AppDbContext contextParam)
        {
            context = contextParam;
        }

        public async Task AddResidentAsync(ResidentVM residentVM)
        {
            var resident = new Resident()
            {
                FirstName = residentVM.FirstName,
                LastName = residentVM.LastName,
                DoB = residentVM.DoB,
                FacilityId = residentVM.FacilityId
            };
            context.Residents.Add(resident);
            await context.SaveChangesAsync();
        }

        public async Task<List<Resident>> GetAllResidentAsync() => await context.Residents.ToListAsync();

        public async Task<Resident> GetResidentByIdAsync(int id) => await context.Residents.FirstOrDefaultAsync(f => f.Id == id);
        

        public async Task<Resident> UpdateResidentAsync(int id, ResidentVM residentVM)
        {
            var resident = await context.Residents.FirstOrDefaultAsync(f => f.Id == id);
            if (resident == null) throw new NotFoundRecordsException($"Resident with ID {id} not found!");

            resident.FirstName = residentVM.FirstName;
            resident.LastName = residentVM.LastName;
            resident.DoB = residentVM.DoB;
            resident.FacilityId = residentVM.FacilityId;
            context.Residents.Update(resident);
            context.SaveChanges();
            
            return resident;
        }

        public async Task DeleteResidentAsync(int id)
        {
            var resident = await context.Residents.FirstOrDefaultAsync(f => f.Id == id);
            if (resident == null) throw new NotFoundRecordsException($"Resident with ID {id} not found!");

            context.Residents.Remove(resident);
            context.SaveChanges();
        }
    }
}
