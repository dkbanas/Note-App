using Core.Entities;
using Core.Queries.NoteQueries;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Handlers.NoteHandlers;
public class GetNoteByIdHandler : IRequestHandler<GetNoteByIdQuery, Note>
{
    private readonly NoteContext _noteContext;

    public GetNoteByIdHandler(NoteContext noteContext)
    {
        _noteContext = noteContext;
    }


    public async Task<Note> Handle(GetNoteByIdQuery request, CancellationToken cancellationToken)
    {
        var note = await _noteContext.Notes.FirstOrDefaultAsync(n => n.Id == request.id && n.UserEmail == request.userEmail);
        return note;
    }
}