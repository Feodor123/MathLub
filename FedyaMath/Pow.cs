using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FedyaMath
{
    class Pow : MathExpression
    {
        private MathExpression mathExpression;
        private MathExpression exponent;
        public Pow(MathExpression mathExpression, MathExpression exponent)
        {
            this.mathExpression = mathExpression;
            this.exponent = exponent;
        }
        public override double Calculate()
        {
            return Math.Pow(mathExpression.Calculate(),exponent.Calculate());
        }
    }
}
