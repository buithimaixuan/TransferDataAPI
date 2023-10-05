using Microsoft.EntityFrameworkCore;
using ServerA.Data.Models;
using ServerA.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerA.Data.Services
{
	public class ProgressNoteService
	{
        private AppDbContext context;

        public ProgressNoteService(AppDbContext contextParam)
        {
            context = contextParam;
        }

        public async Task AddProgressNoteAsync(ProgressNoteVM progressNoteVM)
        {
            var progressNote = new ProgressNote()
            {
                Content = progressNoteVM.Content,
                Type = progressNoteVM.Type,
                CreatedDate = progressNoteVM.CreatedDate,
                ResidentId = progressNoteVM.ResidentId
            };
            context.ProgressNotes.Add(progressNote);
           await context.SaveChangesAsync();
        }

        public async Task<List<ProgressNote>> GetAllProgressNoteAsync() => await context.ProgressNotes.ToListAsync();

        public async Task<ProgressNote> GetProgressNoteByIdAsync(int id) => await context.ProgressNotes.FirstOrDefaultAsync(p => p.Id == id);

        public async Task<ProgressNote> UpdateProgressNoteAsync(int id, ProgressNoteVM progressNoteVM)
        {
            var progressNote = await context.ProgressNotes.FirstOrDefaultAsync(p => p.Id == id);
            if (progressNote == null) throw new Exception($"ProgressNote with ID {id} not found!");

            progressNote.Content = progressNoteVM.Content;
            progressNote.Type = progressNoteVM.Type;
            progressNote.CreatedDate = progressNoteVM.CreatedDate;
            progressNote.ResidentId = progressNoteVM.ResidentId;
            context.ProgressNotes.Update(progressNote);
            context.SaveChanges();
            
            return progressNote;
        }

        public async Task DeleteProgressNoteAsync(int id)
        {
            var progressNote = await context.ProgressNotes.FirstOrDefaultAsync(f => f.Id == id);
            if (progressNote == null) throw new Exception($"ProgressNote with ID {id} not found!");

            context.ProgressNotes.Remove(progressNote);
            context.SaveChanges();
        }
    }
}

