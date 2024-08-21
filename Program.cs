using Application.PresentationLogic.Views;

namespace Application;

public class Program
{
    static async Task Main(string[] args)
    {
        PersonView personView = new PersonView();
        await personView.Menu();
    }
}