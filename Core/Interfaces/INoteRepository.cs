using Core.Entities;

namespace Core.Interfaces;

public interface INoteRepository
{
    Task<Note> GetNotebyIdAsync(int id);
    Task<IReadOnlyList<Note>> GetAllNotesAsync();
}