using System.Linq;
using Validate.Abstractions;

namespace Validate
{
    public class Cnpj : IValidator<string>
    {
        public bool IsValid(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;
            if (value.Length != 14) return false;

            var cnpjArray = value.ToCharArray();
            if (cnpjArray.Distinct().Count() == 1) return false;

            var multiplier1 = new[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplier2 = new[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            var principal = value[..12];
            var sum = 0;

            for (var i = 0; i < 12; i++)
                sum += int.Parse(principal[i].ToString()) * multiplier1[i];

            var mod = sum % 11;
            if (mod < 2)
                mod = 0;
            else
                mod = 11 - mod;

            var digit = mod.ToString();
            principal += digit;
            sum = 0;
            for (var i = 0; i < 13; i++)
                sum += int.Parse(principal[i].ToString()) * multiplier2[i];

            mod = sum % 11;
            if (mod < 2)
                mod = 0;
            else
                mod = 11 - mod;

            digit += mod;
            return value.EndsWith(digit);
        }
    }
}