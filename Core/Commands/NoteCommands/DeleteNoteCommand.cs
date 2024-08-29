using MediatR;

namespace Core.Commands.NoteCommands;
public class DeleteNoteCommand : IRequest<ServiceResponse>
{
    public int id { get; set; }
    public string userEmail { get; set; }
}