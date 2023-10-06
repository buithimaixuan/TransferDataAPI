using Microsoft.EntityFrameworkCore;
using ServerB.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerB.Data.Services
{
	public class ProgressNoteService
	{
        private AppDbContext context;

        public ProgressNoteService(AppDbContext contextParam)
        {
            context = contextParam;
        }

        //----
        public async Task<List<ProgressNote>> GetDataProgressNotesServerA()
        {
            HttpClient httpClient = new HttpClient();

            //get data from ServerA
            var progressNotes = await httpClient.GetFromJsonAsync<List<ProgressNote>>("https://localhost:7296/api/ProgressNote/get-all-progressNote");

            return progressNotes;
        }

        public async Task<List<ProgressNote>> GetDataProgressNotesServerB()
        {
            var progressNotes = await context.ProgressNotes.ToListAsync();
            return progressNotes;
        }

        public async Task SyncData()
        {
            var dataServerA = await GetDataProgressNotesServerA();
            var dataServerB = await GetDataProgressNotesServerB();

            var addProgressNotes = new List<ProgressNote>();
            var updateProgressNotes = new List<ProgressNote>();
            var deleteProgressNotes = new List<ProgressNote>();


            foreach (var progressNote in dataServerA)
            {
                var isExisted = dataServerB.Any(c => c.Id == progressNote.Id);
                if (isExisted)
                {
                    updateProgressNotes.Add(progressNote);
                    continue;
                }
                addProgressNotes.Add(progressNote);
            }

            foreach (var progressNote in dataServerB)
            {
                var isExisted = dataServerA.Any(c => c.Id == progressNote.Id);
                if (!isExisted)
                {
                    deleteProgressNotes.Add(progressNote);
                }
            }

            if (addProgressNotes.Any())
            {
                context.ProgressNotes.AddRange(addProgressNotes);
            }

            if (updateProgressNotes.Any())
            {
                context.ProgressNotes.UpdateRange(updateProgressNotes);
            }

            if (deleteProgressNotes.Any())
            {
                context.ProgressNotes.RemoveRange(deleteProgressNotes);
            }

            await context.SaveChangesAsync();
        }

        //----
    }
}

