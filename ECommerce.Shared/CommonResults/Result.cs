using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.CommonResults
{
    public class Result
    {
        protected List<Error> _error = [];

        public bool IsSuccess => _error.Count == 0;
        public bool IsFailed => !IsSuccess;
        public List<Error> Error => _error;

        protected Result()
        {

        }

        protected Result(Error error)
        {
            _error.Add(error);
        }

        protected Result(List<Error> errors)
        {
            _error.AddRange(errors);
        }

        public static Result Ok() => new Result();
        public static Result Failed(Error error) => new Result(error);
        public static Result Failed(List<Error> errors) => new Result(errors);
    }

    public class Result<TValue> : Result
    {
        private TValue _value;
        public TValue Value => IsSuccess ? _value : throw new InvalidOperationException("Can not access the value ");

        private  Result(TValue value ) :base()
        {
            _value = value;
        }

        private Result(Error error) : base(error)
        {
            _value = default;
        }


        private Result(List<Error> errors ) : base(errors)
        {
            _value = default;
        }

        public static Result<TValue> Ok(TValue value ) => new Result<TValue>(value);
        public static new Result<TValue> Failed(Error error) => new Result<TValue>(error);
        public static new Result<TValue>  Failed(List<Error> errors) => new Result<TValue>(errors);

    }   
}