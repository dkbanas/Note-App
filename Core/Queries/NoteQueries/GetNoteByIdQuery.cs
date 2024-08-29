using Core.Entities;
using MediatR;

namespace Core.Queries.NoteQueries;
public class GetNoteByIdQuery : IRequest<Note>
{
    public int id { get; set; }
    public string userEmail { get; set; }
}