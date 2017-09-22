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
            object lastObj = null;
            int bracketCount = 0;
            char lastChar = ' ';
            bool wasPoint = false;
            bool wasMinus = false;
            double d = 0;
            foreach (var ch in input)
            {
                if (ch == ' ')
                {
                    //nothing;
                    continue;
                }
                else if (Char.IsDigit(ch))
                {
                    if ((lastObj != null) && !(lastObj is Operator))
                    {
                        throw new InvalidSyntaxException();
                    }
                }
                else if (ch == '.' || ch == ',')
                {
                    if (!Char.IsDigit(lastChar))
                    {
                        throw new InvalidSyntaxException();
                    }
                }
                else if (ch == '(')
                {
                    if (lastObj is Unit)
                    {
                        objects.Add(new Operator("*",bracketCount, lastObj));
                    }
                    bracketCount++;
                }
                else if (ch == ')')
                {
                    if (lastObj is Operator)
                    {
                        throw new InvalidSyntaxException();
                    }
                    if (bracketCount == 0)
                    {
                        throw new BracketOverflowException();
                    }
                    bracketCount--;
                }
                else if (Char.IsLetter(ch))
                {

                }
                else if (ch == '+' || ch == '-' || ch == '*' || ch == '/' || ch == '^')
                {
                    objects.Add(new Operator(ch.ToString(),bracketCount, lastObj));
                }
                else
                {
                    throw new InvalidSyntaxException();
                }
                lastObj = objects.Last();
                lastChar = ch;
            }
            if (bracketCount != 0)
            {
                throw new BracketOverflowException();
            }
        }
        private class Operator
        {
            public const int bracketPriority = 8;
            public string operation;
            public int priority;
            public Operator(string operation,int bracketCount, object lastObject)
            {
                switch (operation)
                {
                    case "+":
                        priority = bracketPriority * bracketCount + 1;
                        break;
                    case "-":
                        if (lastObject == null || !(lastObject is Unit))
                        {
                            priority = bracketPriority * bracketCount + 6;
                        }
                        else
                        {
                            priority = bracketPriority * bracketCount + 2;
                        }
                        break;
                    case "*":
                        priority = bracketPriority * bracketCount + 3;
                        break;
                    case "/":
                        priority = bracketPriority * bracketCount + 4;
                        break;
                    case "^":
                        priority = bracketPriority * bracketCount + 7;
                        break;
                    default:
                        priority = bracketPriority * bracketCount + 5;
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
