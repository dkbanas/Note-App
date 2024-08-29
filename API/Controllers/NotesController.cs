using API.Errors;
using API.Extensions;
using API.Helpers;
using Core.Commands.NoteCommands;
using Core.DTO;
using Core.Entities;
using Core.Interfaces;
using Core.Queries.NoteQueries;
using Infrastructure.Data;
using Infrastructure.Handlers.NoteHandlers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NotesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<Note>>> GetNotes(int pageIndex = 1, int pageSize = 10, string sort = null, string search = null)
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();
            var notes = await _mediator.Send(new GetAllNotesQuery { pageIndex = pageIndex, pageSize = pageSize,userEmail = email ,sort = sort, search = search });
            var totalItems = await _mediator.Send(new GetNumOfNotesQuery {userEmail = email});
            
            var paginationData = new Pagination<Core.Entities.Note>(
                pageIndex,
                pageSize,
                totalItems,
                notes ?? new List<Core.Entities.Note>()
            );
            return Ok(paginationData);

        }
    
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Core.Entities.Note>> GetNoteById(int id)
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();
            var note = await _mediator.Send(new GetNoteByIdQuery{id = id, userEmail = email});
            if (note == null) return NotFound(new ApiResponse(404));
            return Ok(note);
            
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Core.Entities.Note>> CreateNote(NoteForCreation noteDto)
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();
            var note = new Core.Entities.Note
            {
                Title = noteDto.Title,
                Description = noteDto.Description,
                CreatedDate = DateTime.UtcNow,
                UserEmail = email
            };
            var createdNote = await _mediator.Send(new CreateNoteCommand { note = note });
            if (createdNote == null) return BadRequest(new ApiResponse(400, "Problem creating note"));
            return Ok(createdNote);
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteNoteById(int id)
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();
            var deleted = await _mediator.Send(new DeleteNoteCommand { id = id, userEmail = email });
            return Ok(deleted);
           
        }
        
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateNoteById(int id, NoteForCreation noteDto)
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();
            var existingNote = await _mediator.Send(new GetNoteByIdQuery { id = id, userEmail = email });
            if (existingNote == null) return NotFound(new ApiResponse(404, "Note not found"));
            
            existingNote.Title = noteDto.Title;
            existingNote.Description = noteDto.Description;
            
            var updatedNote = await _mediator.Send(new UpdateNoteCommand { note = existingNote });
            if (updatedNote == null) return BadRequest(new ApiResponse(400, "Problem updating note"));
            return Ok(updatedNote);
            
        }
    }
}

