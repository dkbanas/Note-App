using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data;

public class NoteRepository : INoteRepository
{
    private readonly NoteContext _noteContext;
    public NoteRepository(NoteContext noteContext)
    {
        _noteContext = noteContext;
    }
    public async Task<Note> CreateNoteAsync(Note note)
    {
        _noteContext.Notes.Add(note);
        await _noteContext.SaveChangesAsync();
        return note;
    }


    public async Task<Note> GetNoteByIdAsync(int id, string userEmail)
    {
        var note = await _noteContext.Notes.FirstOrDefaultAsync(n => n.Id == id && n.UserEmail == userEmail);
        if (note == null)
        {
            throw new KeyNotFoundException($"Note with id {id} was not found for user {userEmail}.");
        }
        return note;
    }
    
    public async Task<IReadOnlyList<Note>> GetAllNotesAsync(int pageIndex, int pageSize, string userEmail, string sort, string search = null)
    {
        var query = _noteContext.Notes.Where(n => n.UserEmail == userEmail);

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
            case "date":
                query = query.OrderByDescending(n => n.CreatedDate); 
                break;
            case "date-reverse":
                query = query.OrderBy(n => n.CreatedDate); 
                break;
            default:
                query = query.OrderByDescending(n => n.CreatedDate); 
                break;
        }

        query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

        return await query.ToListAsync();
    }
    
    public async Task<bool> DeleteNoteAsync(int id, string userEmail)
    {
        var note = await _noteContext.Notes.FirstOrDefaultAsync(n => n.Id == id && n.UserEmail == userEmail);
        if (note == null)
        {
            throw new KeyNotFoundException($"Note with id {id} for user {userEmail} not found.");
        }

        _noteContext.Notes.Remove(note);
        await _noteContext.SaveChangesAsync();
        return true;
    }
    
    public async Task<Note> UpdateNoteAsync(Note note, string userEmail)
    {
        var existingNote = await _noteContext.Notes
            .FirstOrDefaultAsync(n => n.Id == note.Id && n.UserEmail == userEmail);

        if (existingNote == null)
        {
            throw new KeyNotFoundException($"Note with id {note.Id} for user {userEmail} not found.");
        }

        existingNote.Title = note.Title;
        existingNote.Description = note.Description;
        _noteContext.Notes.Update(existingNote);
        await _noteContext.SaveChangesAsync();
        return existingNote;
    }

    public async Task<int> CountAsync(string userEmail)
    {
        return await _noteContext.Notes.CountAsync(n => n.UserEmail == userEmail);
    }
}