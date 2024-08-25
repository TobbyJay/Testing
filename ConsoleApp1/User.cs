namespace ConsoleApp1;
public class User
{
    public int UserId { get; set; }
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
    public List<int> Ages { get; set; } = new();
    public StudentInfo StudentInfo { get; set; } = new();
}

public class StudentInfo
{
    public int StudentInfoId { get; set; }
    public string Name { get; set; } = default!;
    public int UserId { get; set; }
}

public class UserDto
{
    public int UserId { get; set; }
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
    public List<int> Ages { get; set; }
    public StudentInfoDto StudentInfoDto { get; set; } = default!;
}
public class StudentInfoDto
{
    public string Name { get; set; } = default!;
}