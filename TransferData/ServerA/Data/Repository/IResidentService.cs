using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ServerA.Data.Models;
using ServerA.Data.ViewModels;

namespace ServerA.Data.Repository
{
    public interface IResidentService
    {
        Task AddResidentAsync(ResidentVM residentVM);
        Task<List<Resident>> GetAllResidentAsync();
        Task<Resident> GetResidentByIdAsync(int id);
        Task<Resident> UpdateResidentAsync(int id, ResidentVM residentVM);
        Task DeleteResidentAsync(int id);
    }
}

