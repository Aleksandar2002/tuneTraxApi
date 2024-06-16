using System.Linq.Expressions;
using Application.Exceptions;
using Application.UseCases;
using AutoMapper;
using DataAccess;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Implementation.UseCases.Generics;

public abstract class GenericFind<TEntity, TResult> : EFUseCase, IQuery<int, TResult> where TResult : class where TEntity : Entity
{
    private IMapper _mapper;
    public GenericFind(TuneTraxContext context, IMapper mapper) : base(context)
    {
        _mapper = mapper;

    }

    public abstract string Name { get; }

    public abstract int Id { get; }

    public TResult Execute(int search) //params Expression<Func<TEntity, object>>[] includes
    {
        var data = Context.Set<TEntity>().FirstOrDefault(x => x.Id == search && x.IsActive);

        // foreach (var include in includes)
        // {
        //     query = query.Include(include);
        // }

        // var data = query;

        if (data == null)
        {
            throw new EntityNotFoundException(search, typeof(TEntity).Name);
        }

        return _mapper.Map<TResult>(data);
    }

}
