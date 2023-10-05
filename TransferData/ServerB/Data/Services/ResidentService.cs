using Microsoft.EntityFrameworkCore;
using ServerB.Data.Models;
using ServerB.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerB.Data.Services
{
    public class ResidentService
    {
        private AppDbContext _context;

        public ResidentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddResidentAsync(Resident resident)
        {
            var _resident = new Resident()
            {
                Id = resident.Id,
                FirstName = resident.FirstName,
                LastName = resident.LastName,
                DoB = resident.DoB,
                FacilityId = resident.FacilityId
            };
            await _context.Residents.AddAsync(_resident);
            await _context.SaveChangesAsync();
        }

        public async Task AddListResidentAsync(List<Resident> residents)
        {
            var residentEntities = residents.Select(resident => new Resident
            {
                Id = resident.Id,
                FirstName = resident.FirstName,
                LastName = resident.LastName,
                DoB = resident.DoB,
                FacilityId = resident.FacilityId
            }).ToList();

            await _context.Residents.AddRangeAsync(residentEntities);
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


        public void DeleteAllResident()
        {
            var _resident = _context.Residents.ToList();
            _context.Residents.RemoveRange(_resident);
            _context.SaveChanges();
        }
    }
}
