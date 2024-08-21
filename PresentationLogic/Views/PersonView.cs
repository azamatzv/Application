using Application.BusinesLogic.Services;
using Application.DataAccess;
using Application.Models;
using Application.Models.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Application.PresentationLogic.Views;

public class PersonView
{
    private readonly IPersonServic _personSevice;
    public PersonView()
    {
        _personSevice = new PersonServic(
            personDataAccess: new PersonDataAccess(
                appContext: new Data.AppDbContext()));
    }

    public async Task Menu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("1. Yangi shaxs qo'shish");
            Console.WriteLine("2. Barcha shaxslarni ko'rsatish");
            Console.WriteLine("3. Shaxsni yangilash");
            Console.WriteLine("4. Shaxsni o'chirish");
            Console.WriteLine("5. Chiqish");
            Console.WriteLine();
            Console.Write("Tanlang: ");

            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    await AddPerson();
                    break;
                case "2":
                    await ReadPerson();
                    break;
                case "3":
                    await UpdatePerson();
                    break;
                case "4":
                    await DeletePerson();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Noto'g'ri tanlov.  Davom etish uchun biror tugmani bosing...");
                    Console.ReadKey();
                    break;
            }
        }



        async Task AddPerson()
        {
            Console.Clear();
            Console.WriteLine("Yangi shaxs qo'shish");

            Console.Write("Ismingizni kiriting: ");
            string? name = Console.ReadLine();

            Console.Write("Yoshingizni kiriting: ");
            string? ageInput = Console.ReadLine();

            if (!short.TryParse(ageInput, out short age))
            {
                Console.WriteLine("Yosh noto'g'ri formatda kiritildi. Davom etish uchun biror tugmani bosing...");
                Console.ReadKey();
                return;
            }

            Console.Write("Email manzilingizni kiriting: ");
            string? email = Console.ReadLine();

            var person = new Person { Name = name, Age = age, Email = email };

            try
            {
                bool isCreated = await this._personSevice.AddPersonAsync(person);

                if (isCreated)
                {
                    Console.Clear();
                    Console.WriteLine("Yangi shaxs muvaffaqiyatli qo'shildi. Davom etish uchun biror tugmani bosing...");
                    Console.ReadKey();
                }
            }
            catch (AlreadyExistException ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
        }


        async Task ReadPerson()
        {
            Console.Clear();

            var people = await _personSevice.GetAllPeople().ToListAsync();

            Console.WriteLine("Ma'lumotlar bazasidagi barcha shaxslar!");
            Console.WriteLine();

            people.ForEach(p => Console.WriteLine($"ID: {p.Id}, Ism: {p.Name}, Yosh: {p.Age}, Email: {p.Email}"));

            Console.WriteLine();
            Console.WriteLine("Davom etish uchun biror tugmani bosing...");
            Console.ReadKey();
        }


        async Task UpdatePerson()
        {
            Console.Clear();
            await ReadPerson();

            Console.Write("Yangilamoqchi bo'lgan shaxs ID sini kiriting: ");
            int id = int.Parse(Console.ReadLine());
            Console.Clear();

            Console.Write($"Yangi ismni kiriting (agar o'zgarishsiz qolishini xohlasangiz '~' ) : ");
            string? newName = Console.ReadLine();
            if (newName == "~") newName = null;

            Console.Write($"Yangi yoshni kiriting (agar o'zgarishsiz qolishini xohlasangiz '~' ) : ");
            string? newAge = Console.ReadLine();
            short? age = null;
            if (newAge == "~")
            {
                age = null;
            }
            else
            {
                age = short.Parse(newAge);
            }

            Console.Write($"Yangi emailni kiriting (agar o'zgarishsiz qolishini xohlasangiz '~' ) : ");
            string? newEmail = Console.ReadLine();
            if (newEmail == "~") newEmail = null;

            var person = new Person()
            {
                Id = id,
                Name = newName,
                Age = age,
                Email = newEmail
            };

            try
            {
                bool isUpdate = await this._personSevice.UpdatePersonAsync(person);

                if (isUpdate)
                {
                    Console.WriteLine();
                    Console.WriteLine("Shaxs muvaffaqiyatli yangilandi. Davom etish uchun biror tugmani bosing...");
                    Console.ReadKey();
                }
            }
            catch (NotFoundException ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
        }


        async Task DeletePerson()
        {
            Console.Clear();
            Console.Write("O'chirmoqchi bo'lgan shaxs ID sini kiriting (orqaga qaytish uchun '0' ni bosing): ");
            string? idInput = Console.ReadLine();

            if (idInput == "0") return;

            if (!int.TryParse(idInput, out int id))
            {
                Console.WriteLine("ID noto'g'ri formatda kiritildi. Dastur to'xtatildi.");
                Console.ReadKey();
                return;
            }

            try
            {
                bool isDelete = await this._personSevice.DeletePersonAsync(id : id);

                if (isDelete)
                {
                    Console.WriteLine();
                    Console.WriteLine("Shaxs muvaffaqiyatli o'chirildi.  Davom etish uchun biror tugmani bosing...");
                    Console.ReadKey();
                }
            }
            catch (NotFoundException ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
        }
    }
}