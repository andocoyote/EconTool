using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EconTool
{
    //
    // Differentiate API
    /*
    {
        "symbols": "x",
        "fx": "x**5 + 7*x**4 + 3"
    }
    */
    public class AndoEconDifferentiateRequestModel
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
    public class AndoEconDifferentiateResponseModel
    {
        public string Fx { get; set; } = "";
        public string Derivative { get; set; } = "";
    }

    //
    // MarginalUtility API
    /*
    {
        "symbols": "c p",
        "fx": "sqrt(p * c)",
        "variable": "c"
    }
    */
    public class AndoEconMarginalUtilityRequestModel
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
    public class AndoEconMarginalUtilityResponseModel
    {
        public string Fx { get; set; } = "";
        public string MarginalUtility { get; set; } = "";
    }

    //
    // Solve API
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
    public class AndoEconSolveRequestModel
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
    public class AndoEconSolveResponseModel
    {
        public string Fx { get; set; } = "";
        public string Result { get; set; } = "";
    }
}
