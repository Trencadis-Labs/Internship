using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpresionTrees
{

    public interface IOperand
    {
        int operand { get; }
    }
    public interface IOperator
    {
        string opperator { get; }
    }

    public interface IBinaryExpresion
    {
        IOperand LeftOperand { get; }

        IOperand RightOperand { get; }

        IOperator MiddleOperator { get; }
    }


}
