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
	public class ProgressNoteService : IProgressNoteService
	{
        private readonly AppDbContext _context;

        public ProgressNoteService(AppDbContext contextParam)
        {
            _context = contextParam;
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
            _context.ProgressNotes.Add(progressNote);
           await _context.SaveChangesAsync();
        }

        public async Task<List<ProgressNote>> GetAllProgressNoteAsync() => await _context.ProgressNotes.ToListAsync();

        public async Task<ProgressNote> GetProgressNoteByIdAsync(int id) => await _context.ProgressNotes.FirstOrDefaultAsync(p => p.Id == id);

        public async Task<ProgressNote> UpdateProgressNoteAsync(int id, ProgressNoteVM progressNoteVM)
        {
            var progressNote = await _context.ProgressNotes.FirstOrDefaultAsync(p => p.Id == id);
            if (progressNote == null) throw new NotFoundRecordsException($"ProgressNote with ID {id} not found!");

            progressNote.Content = progressNoteVM.Content;
            progressNote.Type = progressNoteVM.Type;
            progressNote.CreatedDate = progressNoteVM.CreatedDate;
            progressNote.ResidentId = progressNoteVM.ResidentId;
            _context.ProgressNotes.Update(progressNote);
            _context.SaveChanges();
            
            return progressNote;
        }

        public async Task DeleteProgressNoteAsync(int id)
        {
            var progressNote = await _context.ProgressNotes.FirstOrDefaultAsync(f => f.Id == id);
            if (progressNote == null) throw new NotFoundRecordsException($"ProgressNote with ID {id} not found!");

            _context.ProgressNotes.Remove(progressNote);
            _context.SaveChanges();
        }
    }
}

