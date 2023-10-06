using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ServerA.Data.Models;
using ServerA.Data.ViewModels;

namespace ServerA.Data.Interfaces
{
    public interface IProgressNoteService
    {
        Task AddProgressNoteAsync(ProgressNoteVM progressNoteVM);
        Task<List<ProgressNote>> GetAllProgressNoteAsync();
        Task<ProgressNote> GetProgressNoteByIdAsync(int id);
        Task<ProgressNote> UpdateProgressNoteAsync(int id, ProgressNoteVM progressNoteVM);
        Task DeleteProgressNoteAsync(int id);
    }
}

