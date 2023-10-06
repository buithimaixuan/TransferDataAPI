using System.Collections.Generic;
using System.Threading.Tasks;
using ServerB.Data.Models;

namespace ServerB.Data.Repository
{
    public interface IFacilityService
    {
        Task<List<Facility>> GetDataFacilitiesServerA();
        Task<List<Facility>> GetDataFacilitiesServerB();
        Task SyncData();
    }
}

