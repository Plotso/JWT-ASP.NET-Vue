namespace JWTShowcase.Posts.Services;

using AutoMapper;
using Data.Models;
using JWTShowcase.Data;
using Microsoft.EntityFrameworkCore;
using Models;

public class AuthorsService : DataService<Author>, IAuthorsService
{
    private readonly IMapper _mapper;

    public AuthorsService(DbContext db, IMapper mapper) : base(db)
        => _mapper = mapper;

    public async Task<Author> GetByUserId(string userId)
        => await All()
            .Where(a => a.UserId == userId)
            .SingleOrDefaultAsync();

    public async Task<Author> GetSystemUser() =>
        await All().Where(a => a.Username == Constants.SystemUser).SingleOrDefaultAsync();
}