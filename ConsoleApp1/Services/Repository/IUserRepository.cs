namespace ConsoleApp1.Services.Repository;
public interface IUserRepository
{
    Task CreateUser(User user);
    Task CreateStudentInfoOfUser(StudentInfo studentInfo);
    Task<User?> GetUserById(int id);
    Task<List<User>> GetUsers();
}