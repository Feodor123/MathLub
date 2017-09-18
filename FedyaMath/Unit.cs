using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FedyaMath
{
    class Unit : MathExpression
    {
        int type;
        double value;
        string name;
        public Unit(int type,string name)
        {
            this.type = type;
            switch (type)
            {
                case 1://variable
                    this.name = name;
                    break;
                case 2://parameter
                    this.name = name;
                    break;
            }
        }
        public Unit(string name)
        {
            this.name = name;
        }
        public Unit(double value)
        {
            type = 0;
            this.value = value; 
        }
        public override double Calculate()
        {
            switch (type)
            {
                case 0:
                    return value;
                default:
                    return notConstants[name];
            }
        }
    }
}
