using Application.Models;

namespace Application.BusinesLogic.Services;

public interface IPersonServic
{
    Task<bool> AddPersonAsync(Person person);
    Task<bool> UpdatePersonAsync(Person person);
    Task<bool> DeletePersonAsync(int id);
    Task<Person> SelectPersonById(int id);
    IQueryable<Person> GetAllPeople();
}