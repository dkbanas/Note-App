using Core.Entities;
using Core.Queries.NoteQueries;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Handlers.NoteHandlers;
public class GetAllNotesHandler : IRequestHandler<GetAllNotesQuery, IReadOnlyList<Note>>
{
    private readonly NoteContext _noteContext;

    public GetAllNotesHandler(NoteContext noteContext)
    {
        _noteContext = noteContext;
    }
    public async Task<IReadOnlyList<Note>> Handle(GetAllNotesQuery request, CancellationToken cancellationToken)
    {
        var query = _noteContext.Notes.Where(n => n.UserEmail == request.userEmail);

        if (!string.IsNullOrEmpty(request.search))
        {
            query = query.Where(n => n.Title.Contains(request.search));
        }

        switch (request.sort?.ToLower())
        {
            case "alphabetically":
                query = query.OrderBy(n => n.Title);
                break;
            case "alphabetically-reverse":
                query = query.OrderByDescending(n => n.Title);
                break;
            case "date":
                query = query.OrderByDescending(n => n.CreatedDate); 
                break;
            case "date-reverse":
                query = query.OrderBy(n => n.CreatedDate); 
                break;
            default:
                query = query.OrderByDescending(n => n.CreatedDate); 
                break;
        }

        query = query.Skip((request.pageIndex - 1) * request.pageSize).Take(request.pageSize);

        return await query.ToListAsync(cancellationToken);
    }
}