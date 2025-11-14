using System.Linq;
using System.Text.RegularExpressions;

namespace Swexato.Api.Services
{
    // Implementação simples, pequena, testável — KISS & DRY friendly.
    public static class CpfValidator
    {
        public static bool Validar(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf)) return false;
            var onlyDigits = Regex.Replace(cpf, @"\D", "");
            if (onlyDigits.Length != 11) return false;

            // rejeita sequências repetidas
            if (onlyDigits.Distinct().Count() == 1) return false;

            int[] nums = onlyDigits.Select(c => int.Parse(c.ToString())).ToArray();

            // calcula 1º dígito verificador
            if (!CheckDigit(nums, 9)) return false;
            // calcula 2º dígito verificador
            if (!CheckDigit(nums, 10)) return false;

            return true;
        }

        private static bool CheckDigit(int[] nums, int length)
        {
            int sum = 0;
            int weight = length + 1; // starts at 10 or 11
            for (int i = 0; i < length; i++)
            {
                sum += nums[i] * (weight--);
            }
            var remainder = sum % 11;
            var dig = remainder < 2 ? 0 : 11 - remainder;
            return nums[length] == dig;
        }
    }
}
