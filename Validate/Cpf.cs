using System;
using System.Linq;
using Extensions;
using Validate.Abstractions;

namespace Validate
{
    public class Cpf : IValidator<string>
    {
        public bool IsValid(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;
            if (value.Length != 11) return false;

            var cpfArray = value.ToCharArray();
            if (cpfArray.Distinct().Count() == 1) return false;

            var numbers = cpfArray.Select(x => Convert.ToInt32(x.ToString())).ToArray();
            var dv = new int[2];
            for (var i = 0; i <= 1; i++)
            {
                var sum = 0;
                for (var j = 0; j <= 8 + i; j++) sum += numbers[j] * (10 + i - j);

                dv[i] = sum * 10 % 11;
                if (dv[i] == 10) dv[i] = 0;
            }

            return dv[0] == numbers[9] && dv[1] == numbers[10];
        }

        private static Guid GetValueAsGuid(string value) => value.GenerateGuid();
    }
}