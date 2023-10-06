using System.Collections.Generic;
using System.Threading.Tasks;
using ServerB.Data.Models;

namespace ServerB.Data.Interfaces
{
    public interface IFacilityService
    {
        Task<List<Facility>> GetDataFacilitiesServerA();
        Task<List<Facility>> GetDataFacilitiesServerB();
        Task SyncData();
    }
}

