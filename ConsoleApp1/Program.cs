using ConsoleApp1.Data;
using ConsoleApp1.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace ConsoleApp1;
public static class Program
{
    static void Main(string[] args)
    {
        // Create a ServiceCollection and configure services
        var serviceCollection = new ServiceCollection();

        ConfigureServices(serviceCollection);
    }
    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<UserContext>(options =>
            options.UseInMemoryDatabase("InMemoryDb"));

        services.AddTransient<IUserService, UserService>();
    }

}