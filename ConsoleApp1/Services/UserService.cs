using ConsoleApp1.Services.Repository;
using Microsoft.Extensions.Logging;

namespace ConsoleApp1.Services;
public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly ILogger<UserService> _logger;
    public UserService(IUserRepository repository,
                       ILogger<UserService> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    public async Task<ApiResponse<User>> CreateUser(UserDto request)
    {
        try
        {
            if (string.IsNullOrEmpty(request.Username)
                 || string.IsNullOrEmpty(request.Password))
                return new ApiResponse<User> { Success = false, Message = "username of password cannot be null or empty" };
          
            var user = await CreateUserRecord(request);

            await CreateStudentInfo(request, user);

            return new ApiResponse<User> { Success = true, Message = "User registered successfully", Data = user };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occured {Exception}.", ex);
            return new ApiResponse<User> { Success = false, Message = "Something went wrong while creating User" };
        }
    }
    private async Task<User> CreateUserRecord(UserDto request)
    {
        var user = new User
        {
            Username = request.Username,
            Password = request.Password,
            Ages = request.Ages
        };

        await _repository.CreateUser(user);
        return user;
    }
    private async Task CreateStudentInfo(UserDto request, User user)
    {
        var studentInfo = new StudentInfo
        {
            Name = request.StudentInfoDto.Name,
            UserId = user.UserId
        };
        await _repository.CreateStudentInfoOfUser(studentInfo);
    }
    public async Task<ApiResponse<User>> GetUserById(int id)
    {
        var user = await _repository.GetUserById(id);

        var message = string.Empty;
        var success = false;
        if(user is not null)
        {
            message = "User retrieved successfully";
            success = true;
        }
        else
        {
            message = "User not found";
            success = false;
        }

        return new ApiResponse<User> { Success = success, Data = user!, Message = message };
    }

    public async Task<ApiResponse<List<User>>> GetUsers()
    {
        var user = await _repository.GetUsers();

        var message = !user.Any() ? "User not found" : "User retrieved successfully";

        return new ApiResponse<List<User>> { Success = true, Data = user.ToList()!, Message = message };
    }
}