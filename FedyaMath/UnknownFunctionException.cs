using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FedyaMath
{
    [Serializable]
    public class UnknownFunctionException : Exception
    {
        public UnknownFunctionException(string message) : base(message) { }
    }
}
