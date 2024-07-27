using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class NoteRepository : INoteRepository
{
    private readonly NoteContext _noteContext;

    public NoteRepository(NoteContext noteContext)
    {
        _noteContext = noteContext;
    }

    public async Task<Note> GetNotebyIdAsync(int id)
    {
        var note = await _noteContext.Notes.FindAsync(id);
        if (note == null)
        {
            throw new KeyNotFoundException($"Note with id {id} was not found.");
        }
        return note;
    }

    public async Task<IReadOnlyList<Note>> GetAllNotesAsync(string sort)
    {
        var query = _noteContext.Notes.AsQueryable();

        if (!string.IsNullOrEmpty(sort))
        {
            switch (sort?.ToLower())
            {
                case "alphabetically":
                    query = query.OrderBy(n => n.Title);
                    break;
                case "alphabetically-reverse":
                    query = query.OrderByDescending(n => n.Title);
                    break;
                case "date":
                    query = query.OrderBy(n => n.CreatedDate);
                    break;
                case "date-reverse":
                    query = query.OrderByDescending(n => n.CreatedDate);
                    break;
                default:
                    query = query.OrderBy(n => n.CreatedDate);
                    break;
            }
        }
        else
        {
            query = query.OrderBy(n => n.CreatedDate);
        }


        return await query.ToListAsync();
        // return await _noteContext.Notes.ToListAsync();
    }
}