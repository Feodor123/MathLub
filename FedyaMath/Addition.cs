using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FedyaMath
{
    class Addition : MathExpression
    {
        public MathExpression[] expressions;
        public Addition(MathExpression[] expressions)
        {
            List<MathExpression> exp = new List<MathExpression>();
            foreach (var m in expressions)
            {
                if (m is Addition)
                {
                    exp.AddRange(((Addition)m).expressions);
                }
                else
                {
                    exp.Add(m);
                }
            }
            this.expressions = exp.ToArray();
        }
        public Addition(MathExpression expression1, MathExpression expression2)
        {
            List<MathExpression> exp = new List<MathExpression>();
            if (expression1 is Addition)
            {
                exp.AddRange(((Addition)expression1).expressions);
            }
            else
            {
                exp.Add(expression1);
            }
            if (expression2 is Addition)
            {
                exp.AddRange(((Addition)expression2).expressions);
            }
            else
            {
                exp.Add(expression2);
            }
            {
                expressions = exp.ToArray();
            }
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
