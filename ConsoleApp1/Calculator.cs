namespace ConsoleApp1;
public class Calculator
{
    public int Add(int a, int b)
    {
        return a + b;
    }
    public int Substract(int a, int b)
    {
        return a - b;
    }
    public int Divide(int numerator, int denominator)
    {
        if(denominator == 0)
        {
            throw new CustomException("denominator cannot be zero");
        }
        return numerator/denominator;
    }

    public async Task<int> DivideAsync(int numerator, int denominator)
    {
        await Task.Delay(100);
        if (denominator == 0)
        {
            throw new CustomException("denominator cannot be zero");
        }
        return numerator / denominator;
    }
}

public class CustomException: Exception
{
    public CustomException(string message):base(message)
    {
    }
}