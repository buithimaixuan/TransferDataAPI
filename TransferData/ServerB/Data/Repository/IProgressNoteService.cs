using System.Collections.Generic;
using System.Threading.Tasks;
using ServerB.Data.Models;

namespace ServerB.Data.Repository
{
	public interface IProgressNoteService
	{
        Task<List<ProgressNote>> GetDataProgressNotesServerA();
        Task<List<ProgressNote>> GetDataProgressNotesServerB();
        Task SyncData();
    }
}

