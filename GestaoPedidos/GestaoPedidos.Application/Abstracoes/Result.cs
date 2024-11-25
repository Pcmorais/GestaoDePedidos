using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPedidos.Application.Abstracoes
{
    public class Result
    {
        public bool Success { get; }
        public string Error { get; }
        public object Value { get; }

        private Result(bool success, string error, object value)
        {
            Success = success;
            Error = error;
            Value = value;
        }

        public static Result Ok(object value) => new Result(true, null, value);
        public static Result Fail(string error) => new Result(false, error, null);
    }
}
