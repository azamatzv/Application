using Application.DataAccess;
using Application.Models;
using Application.Models.Exceptions;

namespace Application.BusinesLogic.Services;

public class PersonServic : IPersonServic
{
    private readonly IPersonDataAccess _personDataAccess;
    public PersonServic(IPersonDataAccess personDataAccess)
    {
        this._personDataAccess = personDataAccess;
    }
    public async Task<bool> AddPersonAsync(Person person)
    {
        var storedPerson = await this._personDataAccess.SelectPersonByIdAsync(person.Id);
        if (storedPerson is not null)
        {
            throw new AlreadyExistException($"{person.Id} bunday Idga ega shaxs oldindan mavjud. Davom etish uchun biror tugmani bosing...");
        }

        await this._personDataAccess.InsertPersonAsync(person);

        return true;
    }
        
    public async Task<bool> DeletePersonAsync(int id)
    {
        var storedPerson = await this._personDataAccess.SelectPersonByIdAsync(id);
        if (storedPerson is null)
        {
            throw new NotFoundException($"{id} bunday Idga ega shaxs topilmadi. Davom etish uchun biror tugmani bosing...");
        }

        await this._personDataAccess.DeletePersonAsync(storedPerson);

        return true;
    }

    public IQueryable<Person> GetAllPeople()
     => this._personDataAccess.SelectAllPeople();


    public async Task<Person> SelectPersonById(int id)
    {
        var storedPerson = await this._personDataAccess.SelectPersonByIdAsync(id);
        if (storedPerson is null)
        {
            throw new NotFoundException($"{id} bunday Idga ega shaxs topilmadi. Davom etish uchun biror tugmani bosing...");
        }

        return storedPerson;
    }

    public async Task<bool> UpdatePersonAsync(Person person)
    {
        var storedPerson = await this._personDataAccess.SelectPersonByIdAsync(person.Id);
        if (storedPerson is null)
        {
            throw new NotFoundException($"{person.Id} bunday Idga ega shaxs topilmadi. Davom etish uchun biror tugmani bosing...");
        }

        storedPerson.Name = person.Name ?? storedPerson.Name;
        storedPerson.Age = person.Age ?? storedPerson.Age;
        storedPerson.Email = person.Email ?? storedPerson.Email;

        await this._personDataAccess.UpdatePersonAsync(storedPerson);

        return true;
    }
}