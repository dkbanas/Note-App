using API.DTO;
using API.Errors;
using API.Extensions;
using API.Helpers;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
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
        private readonly INoteRepository _repo;

        public NotesController(INoteRepository repo)
        {
            _repo = repo;
        }
        
        [HttpGet]
        public async Task<ActionResult<Pagination<Note>>> GetNotes(int pageIndex = 1, int pageSize = 10, string sort = null, string search = null)
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();
            
                var notes = await _repo.GetAllNotesAsync(pageIndex, pageSize, email, sort, search);
                var totalItems = await _repo.CountAsync(email);

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
            var note = await _repo.GetNoteByIdAsync(id, email);
            if (note == null) return NotFound(new ApiResponse(404));
            return Ok(note);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Core.Entities.Note>> CreateNote([FromBody] NoteForCreation noteDto)
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();

            var note = new Core.Entities.Note
            {
                Title = noteDto.Title,
                Description = noteDto.Description,
                CreatedDate = DateTime.UtcNow,
                UserEmail = email
            };
            


            var createdNote = await _repo.CreateNoteAsync(note);
            if (createdNote == null) return BadRequest(new ApiResponse(400, "Problem creating note"));
            return Ok(createdNote);
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteNoteById(int id)
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();
            var deleted = await _repo.DeleteNoteAsync(id, email);
            if (!deleted) return NotFound(new ApiResponse(404, "Note not found"));
            return Ok(deleted);
        }
        
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateNoteById(int id, [FromBody] NoteForCreation noteDto)
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();

            var existingNote = await _repo.GetNoteByIdAsync(id, email);
            if (existingNote == null) return NotFound(new ApiResponse(404, "Note not found"));

            existingNote.Title = noteDto.Title;
            existingNote.Description = noteDto.Description;

            var updatedNote = await _repo.UpdateNoteAsync(existingNote, email);
            if (updatedNote == null) return BadRequest(new ApiResponse(400, "Problem updating note"));


            return Ok(updatedNote);
        }
    }
}

