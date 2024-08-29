using Core.Entities;
using MediatR;

namespace Core.Commands.NoteCommands;
public class UpdateNoteCommand : IRequest<ServiceResponse>
{
    public Note note { get; set; }
    public string userEmail { get; set; }
}