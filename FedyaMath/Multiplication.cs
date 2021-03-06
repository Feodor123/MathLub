﻿using System;
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
            List<MathExpression> exp = new List<MathExpression>();
            foreach(var m in expressions)
            {
                if (m is Multiplication)
                {
                    exp.AddRange(((Multiplication)m).expressions);
                }
                else
                {
                    exp.Add(m);
                }
            }
            this.expressions = exp.ToArray();
        }
        public Multiplication(MathExpression expression1, MathExpression expression2)
        {
            List<MathExpression> exp = new List<MathExpression>(); 
            if (expression1 is Multiplication)
            {
                exp.AddRange(((Multiplication)expression1).expressions);
            }
            else
            {
                exp.Add(expression1);
            }
            if (expression2 is Multiplication)
            {
                exp.AddRange(((Multiplication)expression2).expressions);
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
            double d = 1;
            foreach (var mathExpression in expressions)
            {
                d *= mathExpression.Calculate();
            }
            return d;
        }
    }
}
