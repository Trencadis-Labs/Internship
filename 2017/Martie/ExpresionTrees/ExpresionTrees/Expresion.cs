using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpresionTrees
{
    public class Expresion : IBinaryExpresion
    {
        public IOperand LeftOperand { get; set; }

        public IOperand RightOperand { get; set; }

        public IOperator MiddleOperator { get; set; }


    }
}
