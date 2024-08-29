using MediatR;

namespace Core.Queries.NoteQueries;
public class GetNumOfNotesQuery : IRequest<int>
{
    public string userEmail { get; set; }
}