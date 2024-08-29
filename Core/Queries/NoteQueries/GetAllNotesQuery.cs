using Core.Entities;
using MediatR;

namespace Core.Queries.NoteQueries;
public class GetAllNotesQuery : IRequest<IReadOnlyList<Note>>
{
    public int pageIndex { get; set; }
    public int pageSize { get; set; }
    public string userEmail { get; set; }
    public string sort { get; set; }
    public string? search {get; set; }
}