using Serilog.Sinks.Grafana.Loki.HttpClients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shared.Dependency
{
    public class CustomLokiHttpClient : LokiHttpClient
    {
        public CustomLokiHttpClient() : base()
        {
            //todo: template: make generate new guid
            HttpClient.DefaultRequestHeaders.Add("X-Scope-OrgID", "6E16FC67-7894-4CD4-9E8C-9EA1C5D3886F");
        }
    }
}
