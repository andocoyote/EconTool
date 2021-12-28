using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EconTool.Tests
{
    internal interface IEconToolTest
    {
        public Task Run();
        public void Display();
    }
}
