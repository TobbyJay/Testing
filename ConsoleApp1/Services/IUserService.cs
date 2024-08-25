namespace ConsoleApp1.Services;
public interface IUserService
{
    Task<ApiResponse<User>> CreateUser(UserDto request);
    Task<ApiResponse<User>> GetUserById(int id);
    Task<ApiResponse<List<User>>> GetUsers();
}