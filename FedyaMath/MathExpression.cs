using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FedyaMath
{
    abstract class MathExpression
    {
        public static Dictionary<string, double> notConstants = new Dictionary<string, double>(); 
        List<MathExpression> expressions = new List<MathExpression>();
        public abstract double Calculate();
    }
}
