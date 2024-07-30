using API.Errors;
using API.Helpers;
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
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Pagination<Core.Entities.Note>>> GetNotes(int pageIndex = 1, int pageSize = 10, string sort = null,string search = null)
        {
            var totalItems = await _repo.CountAsync();
            var notes = await _repo.GetAllNotesAsync(pageIndex, pageSize, sort,search);
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
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Core.Entities.Note>> GetNotes(int id)
        {
            var note = await _repo.GetNotebyIdAsync(id);
            if(note == null) return NotFound(new ApiResponse(404));
            return Ok(note);
        }
    }
}

