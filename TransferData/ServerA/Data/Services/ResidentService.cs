using Microsoft.EntityFrameworkCore;
using ServerA.Data.Models;
using ServerA.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServerA.CustomExceptions;
using ServerA.Data.Interfaces;

namespace ServerA.Data.Services
{
    public class ResidentService : IResidentService
    {
        private readonly AppDbContext _context;

        public ResidentService(AppDbContext contextParam)
        {
            _context = contextParam;
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
            _context.Residents.Add(resident);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Resident>> GetAllResidentAsync() => await _context.Residents.ToListAsync();

        public async Task<Resident> GetResidentByIdAsync(int id) => await _context.Residents.FirstOrDefaultAsync(f => f.Id == id);
        

        public async Task<Resident> UpdateResidentAsync(int id, ResidentVM residentVM)
        {
            var resident = await _context.Residents.FirstOrDefaultAsync(f => f.Id == id);
            if (resident == null) throw new NotFoundRecordsException($"Resident with ID {id} not found!");

            resident.FirstName = residentVM.FirstName;
            resident.LastName = residentVM.LastName;
            resident.DoB = residentVM.DoB;
            resident.FacilityId = residentVM.FacilityId;
            _context.Residents.Update(resident);
            _context.SaveChanges();
            
            return resident;
        }

        public async Task DeleteResidentAsync(int id)
        {
            var resident = await _context.Residents.FirstOrDefaultAsync(f => f.Id == id);
            if (resident == null) throw new NotFoundRecordsException($"Resident with ID {id} not found!");

            _context.Residents.Remove(resident);
            _context.SaveChanges();
        }
    }
}
