namespace DDDSamples.Domain
{
    using CSharpFunctionalExtensions;
    using System;
    using ValueObjects;

    public class Bank
    {
        private const int MaxLengthInChars = 50;
        private BicCode _bicCode;

        private Bank()
        {
        }

        public BicCode BICs { get; private set; }
        public string Code { get; private set; }
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public static Result<Bank> Create(string code, string name, string bic)
        {
            var bank = new Bank();

            return Constraints
                .AddResult(bank.SetName(name))
                .AddResult(bank.SetCode(code))
                .AddResult(bank.SetBic(bic))
                .CombineIn(bank);
        }

        public Result<Bank> SetBic(string value)
        {
            var bicCode = BicCode.Create(value);
            if (bicCode.IsFailure)
                return Result.Failure<Bank>(bicCode.Error);
            _bicCode = bicCode.Value;
            return Result.Success(this);
        }

        public Result<Bank> SetCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code)) return Result.Failure<Bank>("Codigo No valido");

            Code = code;
            return Result.Success(this);
        }

        public Result<Bank> SetId(Guid id)
        {
            if (id == Guid.Empty) return Result.Failure<Bank>("Id Invalido");

            Id = id;
            return Result.Success(this);
        }

        public Result<Bank> SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length > MaxLengthInChars)
                return Result.Failure<Bank>("Nombre de Banco Inválido");

            Name = name;
            return Result.Success(this);
        }
    }
}