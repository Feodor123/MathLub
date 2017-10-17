using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FedyaMath
{
    class Division : MathExpression
    {
        MathExpression expression1;
        MathExpression expression2;
        public Division(MathExpression expression1, MathExpression expression2)
        {
            this.expression1 = expression1;
            this.expression2 = expression2;
        }
        public override double Calculate()
        {
            return expression1.Calculate()/expression2.Calculate();
        }
    }
}
