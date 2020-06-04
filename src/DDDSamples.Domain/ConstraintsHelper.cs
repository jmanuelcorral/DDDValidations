namespace DDDSamples.Domain
{
    using CSharpFunctionalExtensions;
    using System;
    using System.Collections.Generic;

    public static class Constraints
    {
        public static ConstraintResult AddResult(Result item)
        {
            var myConstraint = new ConstraintResult();
            myConstraint.AddResult(item);
            return myConstraint;
        }

        public static ConstraintResult AddResultIf(bool condition, Result item)
        {
            var myConstraint = new ConstraintResult();
            myConstraint.AddResultIf(condition, item);
            return myConstraint;
        }

        public static Result Evaluate()
        {
            return Result.Success();
        }

        public static Result Evaluate(Result result)
        {
            return result;
        }

        public static Result<T> Evaluate<T>(Result<T> result)
        {
            return result;
        }
    }

    public class ConstraintResult
    {
        private readonly List<Result> _resultsCollection;

        internal ConstraintResult()
        {
            _resultsCollection = new List<Result>();
        }

        public ConstraintResult AddResult(Result item)
        {
            _resultsCollection.Add(item);
            return this;
        }

        public ConstraintResult AddResultIf(bool condition, Result item)
        {
            if (!condition) return this;

            return AddResult(item);
        }

        public Result Combine()
        {
            return Result.Combine(_resultsCollection, Environment.NewLine);
        }

        public Result<T> CombineIn<T>(T value)
        {
            var combinedResult = Combine();
            if (combinedResult.IsFailure) return Result.Failure<T>(combinedResult.Error);

            return Result.Success(value);
        }
    }
}