using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Swexato.Api.Data;
using Swexato.Api.Models;

namespace Swexato.Api.Services
{
    public class PessoaService
    {
        private readonly AppDbContext _db;
        public PessoaService(AppDbContext db) => _db = db;

        public async Task<Pessoa> CriarAsync(Pessoa p)
        {
            if (!CpfValidator.Validar(p.CPF))
                throw new ArgumentException("CPF inválido.");

            p.CPF = NormalizeCpf(p.CPF);
            _db.Pessoas.Add(p);
            await _db.SaveChangesAsync();
            return p;
        }

        private string NormalizeCpf(string cpf) => System.Text.RegularExpressions.Regex.Replace(cpf, @"\D", "");
    }
}
