using Core;
using Core.Commands.NoteCommands;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Handlers.NoteHandlers;
public class UpdateNoteHandler : IRequestHandler<UpdateNoteCommand, ServiceResponse>
{
    private readonly NoteContext _noteContext;

    public UpdateNoteHandler(NoteContext noteContext)
    {
        _noteContext = noteContext;
    }
    public async Task<ServiceResponse> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
        var existingNote = await _noteContext.Notes
            .FirstOrDefaultAsync(n => n.Id == request.note.Id && n.UserEmail == request.userEmail,cancellationToken);
        

        existingNote.Title = request.note.Title;
        existingNote.Description = request.note.Description;
        
        _noteContext.Notes.Update(existingNote);
        
        await _noteContext.SaveChangesAsync(cancellationToken);
        return new ServiceResponse(true, "Note updated");
    }
}