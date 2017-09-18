using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FedyaMath
{
    class Fraction : MathExpression
    {
        private int numerator;
        private int denominator;
        public Fraction(int numerator,int denominator)
        {
            this.numerator = numerator;
            this.denominator = denominator;
            Reduce();
        }
        public override double Calculate()
        {
            return (double)numerator / (double)denominator;
        }
        public int NOD(int i1,int i2)
        {
            if (i1 == 0 && i2 == 0)
            {
                return 1;
            }
            while (true)
            {
                if (i1 > i2)
                {                   
                    if (i2 == 0)
                    {
                        return i1;
                    }
                    i1 = i1 % i2;
                }
                else
                {
                    if (i1 == 0)
                    {
                        return i2;
                    }
                    i2 = i2 % i1;
                }
            }
        }
        public void Reduce()
        {
            int nod = NOD(numerator,denominator);
            numerator /= nod;
            denominator /= nod;
        }
        public static Fraction operator +(Fraction f1,Fraction f2)
        {
            Fraction f3 = new Fraction((f1.numerator * f2.denominator + f1.denominator * f2.numerator),f1.denominator * f2.denominator);
            f3.Reduce();
            return f3;
        }
        public static Fraction operator -(Fraction f1, Fraction f2)
        {
            Fraction f3 = new Fraction((f1.numerator * f2.denominator - f1.denominator * f2.numerator), f1.denominator * f2.denominator);
            f3.Reduce();
            return f3;
        }
        public static Fraction operator *(Fraction f1, Fraction f2)
        {
            Fraction f3 = new Fraction(f1.numerator * f2.numerator, f1.denominator * f2.denominator);
            f3.Reduce();
            return f3;
        }
        public static Fraction operator /(Fraction f1, Fraction f2)
        {
            Fraction f3 = new Fraction(f1.numerator * f2.denominator, f1.denominator * f2.numerator);
            f3.Reduce();
            return f3;
        }
    }
}
