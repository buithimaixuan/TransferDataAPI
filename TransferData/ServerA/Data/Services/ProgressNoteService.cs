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
        private AppDbContext _context;

        public ProgressNoteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddProgressNoteAsync(ProgressNoteVM progressNote)
        {
            var _progressNote = new ProgressNote()
            {
                Content = progressNote.Content,
                Type = progressNote.Type,
                CreatedDate = progressNote.CreatedDate,
                ResidentId = progressNote.ResidentId
            };
            _context.ProgressNotes.Add(_progressNote);
           await _context.SaveChangesAsync();
        }

        public async Task<List<ProgressNote>> GetAllProgressNoteAsync() => await _context.ProgressNotes.ToListAsync();

        public async Task<ProgressNote> GetProgressNoteByIdAsync(int id) => await _context.ProgressNotes.FirstOrDefaultAsync(p => p.Id == id);

        public async Task<ProgressNote> UpdateProgressNoteAsync(int id, ProgressNoteVM progressNote)
        {
            var _progressNote = await _context.ProgressNotes.FirstOrDefaultAsync(p => p.Id == id);
            if (_progressNote != null)
            {
                _progressNote.Content = progressNote.Content;
                _progressNote.Type = progressNote.Type;
                _progressNote.CreatedDate = progressNote.CreatedDate;
                _progressNote.ResidentId = progressNote.ResidentId;
                _context.ProgressNotes.Update(_progressNote);
                _context.SaveChanges();
            }
            return _progressNote;
        }

        public async Task<bool> DeleteProgressNoteAsync(int id)
        {
            var _progressNote = await _context.ProgressNotes.FirstOrDefaultAsync(f => f.Id == id);
            if (_progressNote != null)
            {
                _context.ProgressNotes.Remove(_progressNote);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}

