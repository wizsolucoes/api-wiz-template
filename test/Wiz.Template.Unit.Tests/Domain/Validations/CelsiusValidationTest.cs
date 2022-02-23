using FluentAssertions;
using Wiz.Template.Domain.ValueObjects;
using Xunit;

namespace Wiz.Template.Unit.Tests.Domain.Validations
{
    public class CelsiusValidationTest
    {
        [Theory]
        [InlineData(-101)]
        [InlineData(101)]
        public void Deve_Gerar_IsValid_False_Caso_Temperatura_Invalida(
            int temperatureC
        )
        {
            // Arrange
            // Act
            var celsius = Celsius.From(temperatureC);

            // Assert
            celsius.IsValid.Should().BeFalse();
            celsius.ValidationResult.Errors.Count.Should().Be(1);
        }
    }
}
