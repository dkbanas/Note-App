using Core;
using Core.Commands.NoteCommands;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Handlers.NoteHandlers;
public class DeleteNoteHandler : IRequestHandler<DeleteNoteCommand, ServiceResponse>
{
    private readonly NoteContext _noteContext;

    public DeleteNoteHandler(NoteContext noteContext)
    {
        _noteContext = noteContext;
    }
    public async Task<ServiceResponse> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {
        var note = await _noteContext.Notes.FirstOrDefaultAsync(n => n.Id == request.id && n.UserEmail == request.userEmail);
        if (note == null)
        {
            throw new KeyNotFoundException($"Note with id {request.id} for user {request.userEmail} not found.");
        }

        _noteContext.Notes.Remove(note);
        await _noteContext.SaveChangesAsync();
        return new ServiceResponse(true,"Note deleted");
    }
}