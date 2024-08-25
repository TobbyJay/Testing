using Microsoft.EntityFrameworkCore;
namespace ConsoleApp1.Data;
public class UserContext : DbContext
{
    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {      
    }
    public DbSet<User> Users { get; set; }
    public DbSet<StudentInfo> StudentInfos { get; set; }
}