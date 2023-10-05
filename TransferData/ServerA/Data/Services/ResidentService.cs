using Microsoft.EntityFrameworkCore;
using ServerA.Data.Models;
using ServerA.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerA.Data.Services
{
    public class ResidentService
    {
        private AppDbContext _context;

        public ResidentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddResidentAsync(ResidentVM resident)
        {
            var _resident = new Resident()
            {
                FirstName = resident.FirstName,
                LastName = resident.LastName,
                DoB = resident.DoB,
                FacilityId = resident.FacilityId
            };
            _context.Residents.Add(_resident);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Resident>> GetAllResidentAsync() => await _context.Residents.Include(x => x.Facility).Include(y => y.ProgressNotes).ToListAsync();

        public async Task<Resident> GetResidentByIdAsync(int id) => await _context.Residents.Include(x => x.Facility).Include(y => y.ProgressNotes).FirstOrDefaultAsync(f => f.Id == id);
        

        public async Task<Resident> UpdateResidentAsync(int id, ResidentVM resident)
        {
            var _resident = await _context.Residents.FirstOrDefaultAsync(f => f.Id == id);
            if (_resident != null)
            {
                _resident.FirstName = resident.FirstName;
                _resident.LastName = resident.LastName;
                _resident.DoB = resident.DoB;
                _resident.FacilityId = resident.FacilityId;
                _context.Residents.Update(_resident);
                _context.SaveChanges();
            }
            return _resident;
        }

        public async Task<bool> DeleteResidentAsync(int id)
        {
            var _resident = await _context.Residents.FirstOrDefaultAsync(f => f.Id == id);
            if (_resident != null)
            {
                _context.Residents.Remove(_resident);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
