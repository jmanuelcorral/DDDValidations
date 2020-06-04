namespace DDDSamples.Domain.Tests
{
    using CSharpFunctionalExtensions;
    using FluentAssertions;
    using ValueObjects;
    using Xunit;

    public class BankTests
    {
        [InlineData("4433", "SCFBESMM", "Banco de Mundodisco")]
        [InlineData("3344", "SCFBESMMXXX", "Banco del MAL")]
        [Theory]
        public void BankShouldPass(string code, string bicCode, string name)
        {
            // Arrange
            Result<Bank> myBank;
            // Act
            myBank = Bank.Create(code, name, bicCode);
            // Assert
            myBank.IsSuccess.Should().BeTrue();
        }

        [InlineData("CCRIES2A")]
        [InlineData("MNTYESMM")]
        [InlineData("CAGLESMM")]
        [InlineData("SCFBESMM")]
        [InlineData("SCFBESMMXXX")]
        [Theory]
        public void BicCodesShouldPass(string bicCode)
        {
            // Arrange
            Result<BicCode> myBicCode;
            // Act
            myBicCode = BicCode.Create(bicCode);
            // Assert
            myBicCode.IsSuccess.Should().BeTrue();
        }
    }
}