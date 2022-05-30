using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EconTool.ServicePrincipalProvider
{
    public class ServicePrincipalModel
    {
        public string ClientID { get; set; } = null;
        public string TokenSecret { get; set; } = null;
        public string TenantID { get; set; } = null;
        public string AppIDURI { get; set; } = null;
    }
}
