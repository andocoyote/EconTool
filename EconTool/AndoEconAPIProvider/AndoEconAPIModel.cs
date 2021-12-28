using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EconTool
{
    //
    // Derivative API
    /*
    {
        "symbols": "x",
        "fx": "x**5 + 7*x**4 + 3"
    }
    */
    public class AndoEconDerivativeRequestModel
    {
        public string Symbols { get; set; } = "";
        public string Fx { get; set; } = "";
    }

    /*
    {
        "fx": "x**5 + 7*x**4 + 3",
        "derivative": "5*x**4 + 28*x**3"
    }
    */
    public class AndoEconDerivativeResponseModel
    {
        public string Fx { get; set; } = "";
        public string Derivative { get; set; } = "";
    }

    //
    // PartialDerivative API
    /*
    {
        "symbols": "c p",
        "fx": "sqrt(p * c)",
        "variable": "c"
    }
    */
    public class AndoEconPartialDerivativeRequestModel
    {
        public string Symbols { get; set; } = "";
        public string Fx { get; set; } = "";
        public string Variable { get; set; } = "";
    }

    /*
    {
        "fx": "sqrt(p * c)",
        "MarginalUtility": "sqrt(c*p)/(2*c)"
    }
    */
    public class AndoEconPartialDerivativeResponseModel
    {
        public string Fx { get; set; } = "";
        public string PartialDerivative { get; set; } = "";
    }

    //
    // Evaluate API
    /*
    {
        "symbols": "p c",
        "fx": "sqrt(c*p)/(2*c)",
        "subs":
        {
            "c":2,
            "p":5
        }
    }
    */
    public class AndoEconEvaluateRequestModel
    {
        public string Symbols { get; set; } = "";
        public string Fx { get; set; } = "";
        public Dictionary<string, double> Subs { get; set; }

    }

    /*
    {
        "fx": "sqrt(c*p)/(2*c)",
        "result": "0.790569415042095"
    }
    */
    public class AndoEconEvaluateResponseModel
    {
        public string Fx { get; set; } = "";
        public string Result { get; set; } = "";
    }

    //
    // Solve API
    /*
    {
        "symbols": "q",
        "fx": "12 - 2/3*q"
    }
    */
    public class AndoEconSolveRequestModel
    {
        public string Symbols { get; set; } = "";
        public string Fx { get; set; } = "";
    }

    /*
    {
        "fx": "12 - 2/3*q",
        "result": "[18]"
    }
    */
    public class AndoEconSolveResponseModel
    {
        public string Fx { get; set; } = "";
        public List<double> Result { get; set; }
    }
}
