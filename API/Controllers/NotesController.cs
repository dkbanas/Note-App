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
        public async Task<ActionResult<Pagination<Core.Entities.Note>>> GetNotes(int pageIndex = 1, int pageSize = 10, string sort = null, string search = null)
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();
            var notes = await _repo.GetAllNotesAsync(pageIndex, pageSize, email, sort, search);
            var totalItems = await _repo.CountAsync(email);

            if (notes == null || !notes.Any()) return NotFound(new ApiResponse(404));

            var paginationData = new Pagination<Core.Entities.Note>(
                pageIndex,
                pageSize,
                totalItems,
                notes
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
        public async Task<ActionResult<Core.Entities.Note>> CreateNote([FromBody] Note note)
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();
            note.UserEmail = email;
            var createdNote = await _repo.CreateNoteAsync(note);
            if (createdNote == null) return BadRequest(new ApiResponse(400, "Problem creating note"));
            return CreatedAtAction(nameof(GetNoteById), new { id = createdNote.Id }, createdNote);
        }
    }
}

