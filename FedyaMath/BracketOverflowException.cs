﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FedyaMath
{
    [Serializable]
    class BracketOverflowException : Exception
    {    
            public BracketOverflowException() : base() { }
    }
}
