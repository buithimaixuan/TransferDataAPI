using Microsoft.EntityFrameworkCore;
using ServerB.Data.Models;
using ServerB.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerB.Data.Services
{
	public class ProgressNoteService
	{
        private AppDbContext _context;

        public ProgressNoteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddProgressNoteAsync(ProgressNote progressNote)
        {
            var _progressNote = new ProgressNote()
            {
                Id = progressNote.Id,
                Content = progressNote.Content,
                Type = progressNote.Type,
                CreatedDate = progressNote.CreatedDate,
                ResidentId = progressNote.ResidentId
            };
           await _context.ProgressNotes.AddAsync(_progressNote);
           await _context.SaveChangesAsync();
        }

        public async Task AddListProgressNoteAsync(List<ProgressNote> progressNotes)
        {
            var progressNoteEntities = progressNotes.Select(progressNote => new ProgressNote
            {
                Id = progressNote.Id,
                Content = progressNote.Content,
                Type = progressNote.Type,
                CreatedDate = progressNote.CreatedDate,
                ResidentId = progressNote.ResidentId
            }).ToList();

            await _context.ProgressNotes.AddRangeAsync(progressNoteEntities);
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
                _progressNote.Type = progressNote.Content;
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


        public void DeleteAllProgressNote()
        {
            var _progressNote = _context.ProgressNotes.ToList();
            _context.ProgressNotes.RemoveRange(_progressNote);
            _context.SaveChanges();
        }
    }
}

