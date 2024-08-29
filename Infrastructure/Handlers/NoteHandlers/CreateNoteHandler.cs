using Core;
using Core.Commands.NoteCommands;
using Core.Entities;
using Infrastructure.Data;
using MediatR;

namespace Infrastructure.Handlers.NoteHandlers;
public class CreateNoteHandler : IRequestHandler<CreateNoteCommand, ServiceResponse>
{
    private readonly NoteContext _noteContext;

    public CreateNoteHandler(NoteContext noteContext)
    {
        _noteContext = noteContext;
    }
    public async Task<ServiceResponse> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        _noteContext.Notes.Add(request.note);
        await _noteContext.SaveChangesAsync();
        return new ServiceResponse(true,"Note created");
    }
}