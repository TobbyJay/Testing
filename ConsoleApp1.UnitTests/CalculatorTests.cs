namespace ConsoleApp1.UnitTests;
public class CalculatorTests
{
    private readonly Calculator _calculator;
    public CalculatorTests()
    {
        _calculator = new();
    }
    [Fact]
    public void When_Adding_TwoValues_Should_Return_CorrectSum()
    {
        // arrange
        var expectedSum = 3;
        var num1 = 2;
        var num2 = 1;
        // act
        var result = _calculator.Add(1, 2);
        // assert
        Assert.Equal(expectedSum, result);
    }

    [Fact]
    public void When_Adding_OnePositive_And_NegativeNumber_ReturnsCorrectSum()
    {
        // arrange
        var expectedSum = -4;
        var num1 = 1;
        var num2 = -5;
        
        // act
        var result = _calculator.Add(num1,num2);

        // assert
        Assert.Equal(expectedSum,result);
    }

    [Fact]
    public void When_Subtracting_TwoPositiveNumbers_ResultShould_ReturnCorrectValue()
    {
        // arrange
        var expectedSum = 4;
        var num1 = 5;
        var num2 = 1;
        // act
        var result = _calculator.Substract(num1,num2);
        // assert
        Assert.Equal(expectedSum, result);
    }

    [Fact]
    public void When_Dividing_CheckThatDenominatorIsNotZero_ResultShouldThrowException()
    {
        // arrange
        var num1 = 16;
        var num2 = 0;

        // act and assert

        Assert.Throws<CustomException>(() => _calculator.Divide(num1, num2));
    }
    [Fact]
    public async Task When_DividingAsyncronously_CheckThatDenominatorIsNotZero_ResultShouldThrowException()
    {
        // arrange
        var num1 = 16;
        var num2 = 0;

        // act and assert

        var exception = await Assert.ThrowsAsync<CustomException>(() => _calculator.DivideAsync(num1, num2));
        Assert.Equal("denominator cannot be zero", exception.Message);
    }

    [Fact]
    public void Dividing_Should_ReturnCorrectResult()
    {
        // arrange
        var expectedSum = 4;
        var num1 = 16;
        var num2 = 4;

        // act
        var result = _calculator.Divide(num1, num2);

        // assert

        Assert.Equal(expectedSum, result);
    }
}