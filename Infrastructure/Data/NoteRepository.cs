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

    public async Task<IReadOnlyList<Note>> GetAllNotesAsync()
    {
        return await _noteContext.Notes.ToListAsync();
    }
}