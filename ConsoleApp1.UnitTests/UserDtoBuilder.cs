namespace ConsoleApp1.UnitTests;
public class UserDtoBuilder
{
    private string? _username;
    private string? _password;
    private List<int>? _ages;
    private int _userId;
    private StudentInfoDto _student = new();

    public UserDtoBuilder WithUserId(int userId)
    {
        _userId = userId;
        return this;
    }
    public UserDtoBuilder WithUsername(string? username)
    {
        _username = username;
        return this;
    }
    public UserDtoBuilder WithPassword(string password)
    {
        _password = password;
        return this;
    }
    public UserDtoBuilder WithAges(List<int> ages)
    {
        _ages = ages;
        return this;
    }
    public UserDtoBuilder WithStudentInfo(StudentInfoDto student)
    {
        _student = student;
        return this;
    }
    public UserDto Build()
    {
        return new UserDto
        {
            Ages = _ages,
            Password = _password,
            StudentInfoDto = _student,
            Username = _username
        };
    }
}

public class UserBuilder
{
    private int _userId;
    private string? _username;
    private string? _password;
    private List<int>? _ages;
    private StudentInfo _student = new();

    public UserBuilder WithUserId(int userId)
    {
        _userId = userId;
        return this;
    }
    public UserBuilder WithUsername(string? username)
    {
        _username = username;
        return this;
    }
    public UserBuilder WithPassword(string password)
    {
        _password = password;
        return this;
    }
    public UserBuilder WithAges(List<int> ages)
    {
        _ages = ages;
        return this;
    }
    public UserBuilder WithStudentInfo(StudentInfo student)
    {
        _student = student;
        return this;
    }
    public User Build()
    {
        return new User
        {
            UserId = _userId,
            Username = _username,
            Password = _password,
            Ages = _ages,
            StudentInfo = _student
        };
    }
    public List<User> BuildList(int count)
    {
        var users = new List<User>();
        for (int i = 0; i < count; i++)
        {
            users.Add(this.Build());
        }
        return users;
    }
}