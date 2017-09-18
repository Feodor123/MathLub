using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FedyaMath
{
    class Function : MathExpression
    {
        private MathExpression mathExpression;
        public Function(MathExpression mathExpression, string func)
        {
            this.mathExpression = mathExpression;
            try
            {
                action = function[func];
            }
            catch
            {                
                throw new UnknownFunctionException("function " + func + " don't founded");
            }
        }
        public Func<double, double> action;
        public override double Calculate()
        {
            return action(mathExpression.Calculate());
        }
        public Dictionary<String, Func<double, double>> function = new Dictionary<string, Func<double, double>>()
        {
            { "sin", (double x) => {return Math.Sin(x); } },
            { "cos", (double x) => {return Math.Cos(x); } },
            { "tg" , (double x) => {return Math.Tan(x); } },
            { "ctg", (double x) => {return Math.Tan(1/x); } }
        };
    }
}
