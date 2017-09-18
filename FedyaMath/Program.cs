using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FedyaMath
{
    class Program
    {
        public MathExpression Parser(string input)
        {
            List<object> objects = new List<object>();
            uint bracketCount = 0;
            foreach (var ch in input)
            {
                if (ch == ' ')
                {
                    //nothing;
                }
                else if (ch == '(')
                {
                    bracketCount++;
                }
                else if (ch == ')')
                {
                    if (bracketCount == 0)
                    {
                        throw new BracketOverflowException();
                    }
                    bracketCount--;
                }
                else if (Char.IsDigit(ch))
                {

                }
                else if (ch == '.' || ch == ',')
                {

                }
                else if (Char.IsLetter(ch))
                {

                }
                else if (ch == '+' || ch == '-' || ch == '*' || ch == '/' || ch == '^')
                {
                    objects.Add(new Operator(ch.ToString(),(int)bracketCount));
                }               
            }
        }
        private class Operator
        {
            public const int bracketPriority = 5;
            public string operation;
            public int priority;
            public Operator(string operation,int bracketPriority)
            {
                switch (operation)
                {
                    case "+":
                        priority = bracketPriority + 1;
                        break;
                    case "-":
                        priority = bracketPriority + 1;
                        break;
                    case "*":
                        priority = bracketPriority + 2;
                        break;
                    case "/":
                        priority = bracketPriority + 2;
                        break;
                    case "^":
                        priority = bracketPriority + 4;
                        break;
                    default:
                        priority = bracketPriority + 3;
                        break;
                }
            }
        }
        private class Number
        {
            Unit unit;
            public Number(string str)
            {
                double d;
                double.TryParse(str,out d);
                if (double.TryParse(str, out d))
                {
                    unit = new Unit(str);
                }
                else
                {
                    unit = new Unit(d);
                }
            }
        }
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");
            string expression = input[0];
            string actionType = input[1];
        }
    }
}
