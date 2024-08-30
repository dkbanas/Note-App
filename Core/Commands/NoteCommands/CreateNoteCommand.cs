using Core.Entities;
using MediatR;

namespace Core.Commands.NoteCommands;
public class CreateNoteCommand : IRequest<ServiceResponse>
{
    public Note note { get; set; }
}
