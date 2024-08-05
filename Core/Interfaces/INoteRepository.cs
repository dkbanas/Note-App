using Core.Entities;

namespace Core.Interfaces;

public interface INoteRepository
{
    Task<Note> CreateNoteAsync(Note note);
    Task<Note> GetNoteByIdAsync(int id, string userEmail);
    Task<IReadOnlyList<Note>> GetAllNotesAsync(int pageIndex, int pageSize, string userEmail, string sort, string search = null);
    Task<bool> DeleteNoteAsync(int id, string userEmail);
    Task<Note> UpdateNoteAsync(Note note, string userEmail);
    Task<int> CountAsync(string userEmail);
}