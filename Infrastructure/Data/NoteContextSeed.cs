using System.Text.Json;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data;
public class NoteContextSeed
{
    public static async Task SeedAsync(NoteContext noteContext, ILoggerFactory loggerFactory)
    {
        try
        {
            if (!noteContext.Notes.Any())
            {
                var notesData = File.ReadAllText("../Infrastructure/Data/SeedData/notes.json");
                if (string.IsNullOrWhiteSpace(notesData))
                {
                    throw new InvalidOperationException("The notes.json file is empty or not found.");
                }
                
                var notes = JsonSerializer.Deserialize<List<Note>>(notesData);
                if (notes == null)
                {
                    throw new InvalidOperationException("Failed to deserialize the notes data.");
                }
                
                foreach (var item in notes)
                {
                    noteContext.Notes.Add(item);
                }

                await noteContext.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<NoteContextSeed>();
            logger.LogError(ex.Message);
        }
    }
}