using ConsoleApp1.Data;
using ConsoleApp1.Services.Repository;
using Microsoft.EntityFrameworkCore;
namespace ConsoleApp1.UnitTests;
public class UserRepositoryUnitTests
{
    private readonly UserRepository _userRepository;
    private readonly UserContext _context;
    public UserRepositoryUnitTests()
    {
        // Setup in-memory database
        var options = new DbContextOptionsBuilder<UserContext>()
            .UseInMemoryDatabase("TestDatabase")
            .Options;

        _context = new UserContext(options);

        _userRepository = new(_context);
    }
    [Fact]
    public async Task When_UserIsCreated_ReturnSuccess()
    {
        // arrange

        var userDto = new UserBuilder()
                            .WithUsername("tobbyumoh")
                            .WithAges([20, 25])
                            .WithPassword("password")
                            .WithStudentInfo(new StudentInfo { Name = "tobby umoh" })
                            .Build();
        // act
        await _userRepository.CreateUser(userDto);

        // assert
        var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userDto.UserId);
        Assert.NotNull(user);
        Assert.Equal("tobbyumoh", user.Username);
    }
    [Fact]
    public async Task Should_GetUserById_And_ReturnSuccess()
    {
        // arrange
        var userDto = new UserBuilder()
                            .WithUsername("tobbyumoh")
                            .WithAges([20, 25])
                            .WithPassword("password")
                            .WithStudentInfo(new StudentInfo { Name = "tobby umoh" })
                            .Build();

        _context.Users.Add(userDto);
        await _context.SaveChangesAsync();

        // act
        var user = await _userRepository.GetUserById(userDto.UserId);
        // assert
        Assert.NotNull(user);
        Assert.Equal("tobbyumoh", user.Username);
    }
    [Fact]
    public async Task Should_GetAllUsers_And_ReturnSuccess()
    {
        // arrange
        var userId = 1;
        var mockedUser = new UserBuilder()
                            .WithUsername("tobbyumoh")
                            .WithAges([20, 25])
                            .WithPassword("password")
                            .WithStudentInfo(new StudentInfo { Name = "tobby umoh" })
                            .BuildList(4);

        _context.AddRange(mockedUser);
        await _context.SaveChangesAsync();

        // act
        var user = await _userRepository.GetUsers();
        // assert
        Assert.NotEmpty(user);
    }
    [Fact]
    public async Task Should_NotGetAllUsers_And_ReturnSuccess()
    {
        // arrange
        var userId = 1;
        var mockedUser = new UserBuilder()
                            .WithUsername("tobbyumoh")
                            .WithAges([20, 25])
                            .WithPassword("password")
                            .WithStudentInfo(new StudentInfo { Name = "tobby umoh" })
                            .BuildList(0);

        _context.AddRange(mockedUser);
        await _context.SaveChangesAsync();

        // act
        var user = await _userRepository.GetUsers();
        // assert
        Assert.Empty(user);
    }
}