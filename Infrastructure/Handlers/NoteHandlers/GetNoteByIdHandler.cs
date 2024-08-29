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
        if (note == null)
        {
            throw new KeyNotFoundException($"Note with id {request.id} was not found for user {request.userEmail}.");
        }
        return note;
    }
}