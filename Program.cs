using OpcProxyCore;
using Newtonsoft.Json.Linq;
using OpcProxyClient;
using OpcGrpcConnect;
using OpcInfluxConnect;
using opcKafkaConnect;

namespace OPC_Proxy
{
    class Program
    {
        static int Main(string[] args)
        {

            serviceManager manager = new serviceManager(args);            

            HttpImpl opcHttpConnector = new HttpImpl();
            InfluxImpl influx = new InfluxImpl();
            KafkaConnect kafka = new KafkaConnect();

            manager.addConnector(opcHttpConnector);
            manager.addConnector(influx);
            manager.addConnector(kafka);

            manager.run();

            return (int)OPCclient.ExitCode;           

            // db.Dispose();
        }
        
    }
}
