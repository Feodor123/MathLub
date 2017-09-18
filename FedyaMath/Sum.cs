using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FedyaMath
{
    class Sum : MathExpression
    {
        public MathExpression[] expressions;
        public Sum(MathExpression[] expressions)
        {
            this.expressions = expressions;
        }
        public override double Calculate()
        {
            double sum = 0;
            foreach(var mathExpression in expressions)
            {
                sum += mathExpression.Calculate();
            }
            return sum;
        }
    }
}
