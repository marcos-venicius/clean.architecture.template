using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Application.CQRS.Todos.Queries.ListAllTodos;

public sealed record ListAllTodosQuery(uint Page, uint PageSize) : IRequest<PaginatedList<ListAllTodosQueryDto>>;

public sealed class ListAllTodosQueryHandler : IRequestHandler<ListAllTodosQuery, PaginatedList<ListAllTodosQueryDto>>
{
    private readonly IEFContext _context;
    private readonly IMapper _mapper;

    public ListAllTodosQueryHandler(IEFContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ListAllTodosQueryDto>> Handle(ListAllTodosQuery request, CancellationToken cancellationToken)
    {
        var items = _context.Todos
            .OrderByDescending(x => x.CreatedAt)
            .ProjectTo<ListAllTodosQueryDto>(_mapper.ConfigurationProvider);

        return await PaginatedList<ListAllTodosQueryDto>.CreateAsync(items, request.Page, request.PageSize);
    }
}