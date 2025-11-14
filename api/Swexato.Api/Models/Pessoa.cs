using System;

namespace Swexato.Api.Models
{
    public class Pessoa
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nome { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public string? Email { get; set; }
        public DateTime? DataNascimento { get; set; }
        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    }
}
