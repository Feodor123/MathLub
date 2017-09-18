using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FedyaMath
{
    class Multiplication : MathExpression
    {
        public MathExpression[] expressions;
        public Multiplication(MathExpression[] expressions)
        {
            this.expressions = expressions;
        }
        public override double Calculate()
        {
            double d = 1;
            foreach (var mathExpression in expressions)
            {
                d *= mathExpression.Calculate();
            }
            return d;
        }
    }
}
