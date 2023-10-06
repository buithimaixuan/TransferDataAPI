using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ServerA.Data.Models;
using ServerA.Data.ViewModels;

namespace ServerA.Data.Interfaces
{
    public interface IFacilityService
    {
        Task AddFacilityAsync(FacilityVM facilityVM);
        Task<List<Facility>> GetAllFacilityAsync();
        Task<Facility> GetFacilityByIdAsync(int id);
        Task<Facility> UpdateFacilityAsync(int id, FacilityVM facilityVM);
        Task DeleteFacilityAsync(int id);
    }
}

