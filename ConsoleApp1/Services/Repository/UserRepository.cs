using ConsoleApp1.Data;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1.Services.Repository;
public class UserRepository : IUserRepository
{
    private readonly UserContext _context;
    public UserRepository(UserContext context)
    {
        _context = context;
    }

    public async Task CreateUser(User user)
    {
        _context.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetUserById(int id)
    {
        return await _context.Users.FirstOrDefaultAsync(x=>x.UserId == id);
    }

    public async Task CreateStudentInfoOfUser(StudentInfo studentInfo)
    {      
        _context.Add(studentInfo);
        await _context.SaveChangesAsync();
    }

    public async Task<List<User>> GetUsers()
    {
       return await _context.Users
                            .Include(s=>s.StudentInfo)
                            .ToListAsync();
    }
}