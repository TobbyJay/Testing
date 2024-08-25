using ConsoleApp1.Services;
using ConsoleApp1.Services.Repository;
using Microsoft.Extensions.Logging;
using Moq;
namespace ConsoleApp1.UnitTests;
public class UserUnitTests
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly Mock<ILogger<UserService>> _mockUserLogger;
    private readonly UserService _userService;
    public UserUnitTests()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _mockUserLogger = new Mock<ILogger<UserService>>();
        _userService = new(_mockUserRepository.Object,_mockUserLogger.Object);
    }
    [Fact]
    public async Task When_UserEntersEmptyOrNullUserNameOrPassword_ReturnErrorMessage()
    {
        // arrange
        var userDto = new UserDtoBuilder()
                            .WithUsername(null)
                            .WithAges([20,25])
                            .WithPassword(string.Empty)
                            .WithStudentInfo(new StudentInfoDto { Name = "tobby umoh"})
                            .Build();
        // act 
        var result = await _userService.CreateUser(userDto);
        //assert
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal("username of password cannot be null or empty", result.Message);
    }
    [Fact]
    public async Task When_UserisCreatedSuccessfully_ReturnSuccess()
    {
        // arrange
        var userDto = new UserDtoBuilder()
                            .WithUsername("testuser")
                            .WithAges([20,25])
                            .WithPassword("password")
                            .WithStudentInfo(new StudentInfoDto { Name = "tobby umoh"})
                            .Build();
        // act 
        var result = await _userService.CreateUser(userDto);
        //assert
        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Equal("User registered successfully", result.Message);

        // verify that user was created exactly once.
        _mockUserRepository.Verify(repo => repo.CreateUser(It.IsAny<User>()), Times.Once);
    }
    [Fact]
    public async Task When_UserIsCalledUsingRightUserId_ReturnSuccess()
    {
        // arrange 
        var userId = 1;

        //var expectedUser = new User
        //{
        //    UserId = userId,
        //    Username = "TestUser",
        //    Password = "password",
        //    Ages = new List<int> { 25, 30 },
        //    StudentInfo = new StudentInfo { Name = "John Doe", UserId = userId }
        //};

        var expectedUser = new UserBuilder()
                                .WithUsername("tobbyumoh")
                                .WithPassword("password")
                                .WithStudentInfo(new StudentInfo { Name = "tobby umoh", UserId = 1 })
                                .WithUserId(userId)
                                .WithAges([20, 35])
                                .Build();

        _mockUserRepository.Setup(repo => repo.GetUserById(userId)).ReturnsAsync(expectedUser);
        // act
        var result = await _userService.GetUserById(userId);
        // assert
        Assert.True(result.Success);
        Assert.Equal("User retrieved successfully", result.Message);
        Assert.NotNull(result.Data);
    }
    [Fact]
    public async Task When_UserIsCalledUsingWrongUserId_ReturnFailure()
    {
        // arrange 
        var userId = 99;

        _mockUserRepository.Setup(repo => repo.GetUserById(userId)).ReturnsAsync((User)null);
        // act
        var result = await _userService.GetUserById(userId);
        // assert
        Assert.False(result.Success);
        Assert.Equal("User not found", result.Message);
        Assert.Null(result.Data);
    }
    [Fact]
    public async Task When_UsersAreRetrieved_ReturnSuccess()
    {
        // arrange 
        //var mockUsers = new List<User>
        //{
        //    new User
        //    {
        //        UserId = 1,
        //        Username = "TestUser",
        //        Password = "password",
        //        Ages = new List<int> { 25, 30 },
        //        StudentInfo = new StudentInfo { Name = "John Doe", UserId = 1 }
        //    },new User
        //    {
        //        UserId = 1,
        //        Username = "TestUser",
        //        Password = "password",
        //        Ages = new List<int> { 25, 30 },
        //        StudentInfo = new StudentInfo { Name = "John Doe", UserId = 1 }
        //    },
        //};

        var mockedUsers = new UserBuilder()
                                .WithUsername("tobbyumoh")
                                .WithPassword("password")
                                .WithStudentInfo(new StudentInfo {  Name = "tobby umoh", UserId = 1 })
                                .WithUserId(1)
                                .WithAges([20,35])
                                .BuildList(2);

        _mockUserRepository.Setup(repo => repo.GetUsers()).ReturnsAsync(mockedUsers);

        // act
        var result = await _userService.GetUsers();
        // assert
        Assert.True(result.Success);
        Assert.Equal("User retrieved successfully", result.Message);
        Assert.NotNull(result.Data);
    }
}
