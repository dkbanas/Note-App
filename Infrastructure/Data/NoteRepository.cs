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

    public async Task<IReadOnlyList<Note>> GetAllNotesAsync(int pageIndex, int pageSize, string sort,string search = null)
    {
        var query = _noteContext.Notes.AsQueryable();
        
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(n => n.Title.Contains(search));
        }

        switch (sort?.ToLower())
        {
            case "alphabetically":
                query = query.OrderBy(n => n.Title);
                break;
            case "alphabetically-reverse":
                query = query.OrderByDescending(n => n.Title);
                break;
            case "date-reverse":
                query = query.OrderByDescending(n => n.CreatedDate);
                break;
            default:
                query = query.OrderBy(n => n.CreatedDate);
                break;
        }

        query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

        return await query.ToListAsync();
    }

    public async Task<int> CountAsync()
    {
        return await _noteContext.Notes.CountAsync();
    }
}