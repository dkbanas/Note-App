using Core.Entities;
using MediatR;

namespace Core.Commands.NoteCommands;
public class CreateNoteCommand : IRequest<Note>, IRequest<ServiceResponse>
{
    public Note note { get; set; }
}
