using API.Errors;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace API.Controllers
{
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Core.Entities.Note>>> GetNotes(string sort)
        {
            var notes = await _repo.GetAllNotesAsync(sort);
            if (notes == null || !notes.Any()) return NotFound(new ApiResponse(404));
            return Ok(notes);
        }
    
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Core.Entities.Note>> GetNotes(int id)
        {
            var note = await _repo.GetNotebyIdAsync(id);
            if(note == null) return NotFound(new ApiResponse(404));
            return Ok(note);
        }
    }
}

