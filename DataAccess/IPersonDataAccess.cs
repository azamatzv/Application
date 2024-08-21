using Application.Models;

namespace Application.DataAccess;

public interface IPersonDataAccess
{
    Task InsertPersonAsync(Person person);
    Task UpdatePersonAsync(Person person);
    Task DeletePersonAsync(Person person);
    Task<Person> SelectPersonByIdAsync(int id);
    IQueryable<Person> SelectAllPeople();
}