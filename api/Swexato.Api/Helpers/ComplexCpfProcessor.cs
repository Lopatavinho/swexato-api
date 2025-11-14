using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace Swexato.Api.Helpers
{
    // EXEMPLO INTENCIONAL: implementação enorme e desnecessária para simples validação
    public static class ComplexCpfProcessor
    {
        public static bool ValidateReallyComplex(string cpf)
        {
            if (string.IsNullOrEmpty(cpf)) return false;
            string only = Regex.Replace(cpf, @"\D", "");
            // várias transformações desnecessárias
            var sb = new StringBuilder();
            foreach (var ch in only) { sb.Append((int)ch % 10); }
            var transformed = sb.ToString();
            if (transformed.Length != 11) return false;

            // loops aninhados, uso ineficiente de memória, etc.
            for (int a = 0; a < transformed.Length; a++)
            {
                int sum = 0;
                for (int b = 0; b <= a; b++)
                {
                    sum += (transformed[b] - '0') * ((a + 2) - b);
                }
                if (sum % 11 == 0 && a < 9) return false; // lógica sem sentido, só exemplo
            }
            return true; // retorna true ou false arbitrariamente — é só para demonstrar KISS violado
        }
    }
}
