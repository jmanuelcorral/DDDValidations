namespace DDDSamples.Domain.ValueObjects
{
    using CSharpFunctionalExtensions;
    using System.Text.RegularExpressions;

    public class BicCode
    {
        private const string FourLetterCharacters = @"^[A-Za-z]{4}$";
        private const string IsOneToThreeAlphaNumeric = @"^[0-9a-zA-Z]{1,3}$";
        private const string MainOfficeCode = "XXX";

        private const int MaxLength = 11;

        private const int MinLength = 8;

        private const string TwoAlphanumeric = @"^[0-9a-zA-Z]{2}$";

        private const string TwoLetterCharacters = @"^[A-Za-z]{2}$";

        private BicCode()
        {
        }

        public string BankCode { get; private set; }
        public string CountryCode { get; private set; }
        public string LocationCode { get; private set; }
        public string OfficeCode { get; private set; }

        public string Value
        {
            get => $"{BankCode}{CountryCode}{LocationCode}{OfficeCode}";

            private set
            {
                BankCode = value.Substring(0, 4).ToUpperInvariant();
                CountryCode = value.Substring(4, 2).ToUpperInvariant();
                LocationCode = value.Substring(6, 2).ToUpperInvariant();

                if (value.Length > MinLength) OfficeCode = value.Substring(8).ToUpperInvariant();
            }
        }

        public static Result<BicCode> Create(string input)
        {
            if (string.IsNullOrEmpty(input)) return Result.Failure<BicCode>("Bic Vacío");

            if (input.Length < MinLength || input.Length > MaxLength)
                return Result.Failure<BicCode>("Longitud incorrecta");

            var bicCode = new BicCode();

            var bankCode = input.Substring(0, 4);
            var countryCode = input.Substring(4, 2);
            var locationCode = input.Substring(6, 2);

            var result = Constraints
                .AddResult(bicCode.SetBankCode(bankCode))
                .AddResult(bicCode.SetCountryCode(countryCode))
                .AddResult(bicCode.SetLocationCode(locationCode))
                .AddResultIf(input.Length > MinLength, bicCode.SetOfficeCode(input.Substring(8)))
                .CombineIn(bicCode);

            if (result.IsFailure) return result;

            return Result.Success(bicCode);
        }

        public Result<BicCode> SetBankCode(string bankCode)
        {
            if (!Regex.IsMatch(bankCode, FourLetterCharacters))
                return Result.Failure<BicCode>("Error en el Codigo de Banco");

            BankCode = bankCode.ToUpperInvariant();

            return Result.Success(this);
        }

        public Result<BicCode> SetCountryCode(string countryCode)
        {
            if (!Regex.IsMatch(countryCode, TwoLetterCharacters))
                return Result.Failure<BicCode>("Error en el Codigo de Pais");

            CountryCode = countryCode.ToUpperInvariant();

            return Result.Success(this);
        }

        public Result<BicCode> SetLocationCode(string locationCode)
        {
            if (!Regex.IsMatch(locationCode, TwoAlphanumeric))
                return Result.Failure<BicCode>("Error en el Codigo de Ciudad");

            LocationCode = locationCode.ToUpperInvariant();

            return Result.Success(this);
        }

        public Result<BicCode> SetMainOfficeCode()
        {
            OfficeCode = MainOfficeCode;

            return Result.Success(this);
        }

        public Result<BicCode> SetOfficeCode(string officeCode)
        {
            if (!Regex.IsMatch(officeCode, IsOneToThreeAlphaNumeric))
                return Result.Failure<BicCode>("Error en el código de Oficina");

            OfficeCode = officeCode.ToUpperInvariant();

            return Result.Success(this);
        }
    }
}