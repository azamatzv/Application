using Application.Data;
using Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.DataAccess;

public class PersonDataAccess : IPersonDataAccess
{
    private readonly AppDbContext _context;
    public PersonDataAccess(AppDbContext appContext)
    {
        _context = appContext;
    }

    public async Task DeletePersonAsync(Person person)
    {
        var result = this._context.Persons.Remove(person);
        await this._context.SaveChangesAsync();
    }

    public async Task InsertPersonAsync(Person person)
    {
        var result = await this._context.Persons.AddAsync(person);
        await _context.SaveChangesAsync();
    }

    public IQueryable<Person> SelectAllPeople()
        => this._context.Persons.AsNoTracking();

    public async Task<Person?> SelectPersonByIdAsync(int id)
    {
        return await this._context.Persons.Where(person => person.Id == id).FirstOrDefaultAsync();
    }

    public async Task UpdatePersonAsync(Person person)
    {
        var result = this._context.Persons.Update(person);
        await _context.SaveChangesAsync();
    }
}