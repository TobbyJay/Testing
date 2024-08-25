namespace ConsoleApp1;
public class ApiResponse<T> 
    where T : class
{
    public bool Success { get; set; }
    public string Message { get; set; } = default!;
    public T Data { get; set; }
}