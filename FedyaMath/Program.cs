﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FedyaMath
{
    class Program
    {
        public static MathExpression Parser(string input)
        {
            List<object> objects = new List<object>();
            object lastObj = null;
            int bracketCount = 0;
            char lastChar = ' ';
            string func = "";
            bool wasPoint = false;
            bool wasDouble = false;
            int ii = 1;
            double d = 0;
            foreach (var ch in input)
            {
                if (!Char.IsLetter(ch) && func.Count() > 0)
                {
                    objects.Add(new Operator(func,bracketCount,lastObj));
                    lastObj = objects.Last();
                    func = "";
                }
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
                    if (!wasDouble)
                    {
                        wasDouble = true;
                        wasPoint = false;
                        ii = 1;
                        d = 0;
                    }
                    if (wasPoint)
                    {
                        d += int.Parse(ch.ToString()) / Math.Pow(10, ii);
                        ii++;
                    }
                    else
                    {
                        d = 10 * d + int.Parse(ch.ToString());
                    }
                }
                else if (ch == '.' || ch == ',')
                {
                    if (!Char.IsDigit(lastChar))
                    {
                        throw new InvalidSyntaxException();
                    }
                    wasPoint = true;
                }
                else
                {
                    if (wasDouble)
                    {
                        wasDouble = false;
                        objects.Add(new Unit(d));
                        lastObj = objects.Last();
                    }
                    if (ch == '(')
                    {
                        if (lastObj is Unit)
                        {
                            objects.Add(new Operator("*", bracketCount, lastObj));
                            lastObj = objects.Last();
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
                        if(lastObj is Unit)
                        {
                            objects.Add(new Operator("*", bracketCount, lastObj));
                            lastObj = objects.Last();
                        }
                        func += ch;
                    }
                    else if (ch == '+' || ch == '-' || ch == '*' || ch == '/' || ch == '^')
                    {
                        objects.Add(new Operator(ch.ToString(), bracketCount, lastObj));
                        lastObj = objects.Last();
                    }
                    else
                    {
                        throw new InvalidSyntaxException();
                    }
                }
                if (objects.Count() != 0)
                {
                    lastObj = objects.Last();
                }
                lastChar = ch;
            }
            if (func.Count() > 0)
            {
                objects.Add(new Operator(func, bracketCount, lastObj));
                lastObj = objects.Last();
            }
            if (wasDouble)
            {
                wasDouble = false;
                objects.Add(new Unit(d));
                lastObj = objects.Last();
            }
            if (bracketCount != 0)
            {
                throw new BracketOverflowException();
            }
            for (int i = 0;i < objects.Count();i++)
            {
                if (objects[i] is Unit)
                {
                    bracketCount = 0;
                    if (i != 0 && objects[i - 1] is Unit)
                    {
                        throw new InvalidSyntaxException();
                    }
                }
                else
                {
                    if (i == 0 || objects[i - 1] is Operator)
                    {
                        if (!(((Operator)objects[i]).operation == "-"))
                        {
                            throw new InvalidSyntaxException();
                        }
                        else
                        {
                            bracketCount++;
                            objects.Insert(i,new Unit(0));
                            i++;
                            objects[i] = new Operator("-",((Operator)objects[i]).bracketCount + bracketCount);
                        }
                    }
                }
            }
            while(objects.Count() > 1)
            {
                ii = 1;
                Operator best = (Operator)(objects[1]);
                for (int i = 3;i < objects.Count(); i += 2)
                {
                    if (best.priority < ((Operator)objects[i]).priority)
                    {
                        best = (Operator)objects[i];
                        ii = i;
                    }
                }
                MathExpression exp = null;
                switch (best.operation)//TODO
                {
                    case "+":
                        exp = new Addition((MathExpression)objects[ii - 1], (MathExpression)objects[ii + 1]);
                        break;
                    case "-":
                        exp = new Subtraction((MathExpression)objects[ii - 1], (MathExpression)objects[ii + 1]);
                        break;
                    case "*":
                        exp = new Multiplication((MathExpression)objects[ii - 1], (MathExpression)objects[ii + 1]);
                        break;
                    case "/":
                        exp = new Division((MathExpression)objects[ii - 1], (MathExpression)objects[ii + 1]);
                        break;
                    case "^":
                        exp = new Pow((MathExpression)objects[ii - 1], (MathExpression)objects[ii + 1]);
                        break;
                    default:
                        exp = new Function((MathExpression)objects[ii + 1],best.operation);
                        break;
                }
                objects[ii] = exp;
                objects.RemoveAt(ii + 1);
                objects.RemoveAt(ii - 1);
            }
            return (MathExpression)objects[0];
        }
        private class Operator
        {
            public const int bracketPriority = 8;
            public string operation;
            public int priority;
            public int bracketCount;
            public Operator(string operation,int bracketCount, object lastObject)
            {
                this.bracketCount = bracketCount;
                this.operation = operation;
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
            public Operator(string operation, int bracketCount)
            {
                this.bracketCount = bracketCount;
                this.operation = operation;
                switch (operation)
                {
                    case "+":
                        priority = bracketPriority * bracketCount + 1;
                        break;
                    case "-":
                        priority = bracketPriority * bracketCount + 2;
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
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");
            string expression = input[0];
            //string actionType = input[1];
            MathExpression mathExpression = Parser(expression);
            double d = mathExpression.Calculate();
        }
    }
}
