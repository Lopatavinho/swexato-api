using Xunit;
using Swexato.Api.Services;

namespace Swexato.Tests
{
    public class CpfValidatorTests
    {
        [Theory]
        [InlineData("11144477735")] // válido
        [InlineData("111.444.777-35")] // válido formatado
        public void ValidCpf_ReturnsTrue(string cpf)
        {
            Assert.True(CpfValidator.Validar(cpf));
        }

        [Theory]
        [InlineData("12345678900")]
        [InlineData("11111111111")]
        [InlineData("")]
        [InlineData(null)]
        public void InvalidCpf_ReturnsFalse(string cpf)
        {
            Assert.False(CpfValidator.Validar(cpf));
        }
    }
}
