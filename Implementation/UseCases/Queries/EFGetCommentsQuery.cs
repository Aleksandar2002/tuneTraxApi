using Application;
using Application.DTO.Comment;
using Application.Exceptions;
using Application.UseCases.Queries.Comments;
using Application.UseCases.Queries.Searches;
using AutoMapper;
using DataAccess;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace Implementation.UseCases.Queries;

public class EFGetCommentsQuery : EFUseCase, IGetCommentsQuery
{
    private IMapper _mapper;
    public EFGetCommentsQuery(TuneTraxContext context, IMapper mapper) : base(context)
    {
        _mapper = mapper;

    }

    public string Name => "Get comments";

    public int Id => 23;

    public PagedResponse<CommentDto> Execute(CommentSearch search)
    {
        var track = Context.Tracks.Find(search.TrackId);

        if (track == null)
        {
            throw new EntityNotFoundException(search.TrackId.Value, typeof(Track).Name);
        }



        var query = Context.Comments.Where(c => c.TrackId == search.TrackId && c.IsActive).AsQueryable();


        int totalCount = query.Count();
        int page = search.Page.HasValue ? search.Page.Value : 1;
        int perPage = search.PerPage.HasValue ? search.PerPage.Value : 10;

        var comments = new List<CommentDto>();

        var coms = query.Where(x => x.ParentId == null && x.IsActive).Include(x => x.Children).Skip(perPage * (page - 1)).Take(perPage).ToList();


        return new PagedResponse<CommentDto>
        {
            Data = coms.Select(x => _mapper.Map<CommentDto>(x)).ToList(),
            TotalCount = totalCount,
            PerPage = perPage,
            CurrentPage = page
        };
    }

    public void GetChildren(CommentDto comment)
    {
        // var list = new List<CommentDto>();
        // foreach (var x in comment.Children)
        // {

        //     var com = new CommentDto()
        //     {
        //         Text = x.Text,
        //         Username = x.User.Username,
        //         CreatedAt = x.CreatedAt,
        //         Id = x.Id,

        //     };
        //     if (x.Children != null && x.Children.Count() != 0)
        //     {
        //         com.Children = GetChildren(x);
        //     }
        //     else
        //     {
        //         com.Children = null;
        //     }
        //     list.Add(com);
        // }
        // return list;

        comment.Children = Context.Comments.Where(x => x.ParentId == comment.Id && x.IsActive).Select(x => new CommentDto()
        {
            Text = x.Text,
            Username = x.User.Username,
            CreatedAt = x.CreatedAt,
            Id = x.Id
        }).ToList();

        foreach (var c in comment.Children)
        {
            GetChildren(c);
        }
    }
}
