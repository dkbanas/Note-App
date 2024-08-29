using Core.Queries.NoteQueries;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Handlers.NoteHandlers;
public class GetNumOfNotesHandler : IRequestHandler<GetNumOfNotesQuery, int>
{
    private readonly NoteContext _noteContext;

    public GetNumOfNotesHandler(NoteContext noteContext)
    {
        _noteContext = noteContext;
    }
    public async Task<int> Handle(GetNumOfNotesQuery request, CancellationToken cancellationToken)
    {
        return await _noteContext.Notes.CountAsync(n => n.UserEmail == request.userEmail);
    }
}