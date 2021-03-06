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

    //
    // MaximumRevenue API
    /*
    {
        "symbols": "x",
        "fx": "10 - 0.001*x"
    }
    */
    public class AndoEconMaximumRevenueRequestModel
    {
        public string Symbols { get; set; } = "";
        public string Fx { get; set; } = "";
    }

    /*
    {
        "demandFunction": "10 - 0.001*x",
        "revenueFunction": "x * (10 - 0.001*x)",
        "marginalRevenueFunction": "10 - 0.002*x",
        "optimumQuantity": 5000.0,
        "itemPrice": 5.0,
        "totalRevenue": 25000.0
    }
    */
    public class AndoEconMaximumRevenueResponseModel
    {
        public string DemandFunction { get; set; } = "";
        public string RevenueFunction { get; set; } = "";
        public string MarginalRevenueFunction { get; set; } = "";
        public double OptimumQuantity { get; set; } = 0.0;
        public double ItemPrice { get; set; } = 0.0;
        public double TotalRevenue { get; set; } = 0.0;
    }

    //
    // MaximumProfit API
    /*
    {
        "symbols": "x",
        "fx": "10 - 0.001*x",
        "cx": "5000 + 2*x"
    }
    */
    public class AndoEconMaximumProfitRequestModel
    {
        public string Symbols { get; set; } = "";
        public string Fx { get; set; } = "";
        public string Cx { get; set; } = "";
    }

    /*
    {
        "demandFunction": "10 - 0.001*x",
        "costFunction": "5000 + 2*x",
        "revenueFunction": "x * (10 - 0.001*x)",
        "profitFunction": "(x * (10 - 0.001*x))-(5000 + 2*x)",
        "marginalProfitFunction": "8 - 0.002*x",
        "optimumQuantity": 4000.0,
        "itemPrice": 6.0,
        "totalRevenue": 24000.0
    }
    */
    public class AndoEconMaximumProfitResponseModel
    {
        public string DemandFunction { get; set; } = "";
        public string CostFunction { get; set; } = "";
        public string RevenueFunction { get; set; } = "";
        public string ProfitFunction { get; set; } = "";
        public string MarginalProfitFunction { get; set; } = "";
        public double OptimumQuantity { get; set; } = 0.0;
        public double ItemPrice { get; set; } = 0.0;
        public double TotalRevenue { get; set; } = 0.0;
    }
}
