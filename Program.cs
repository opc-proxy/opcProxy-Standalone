using System;
using System.Threading;
using System.Threading.Tasks;
using OpcProxyCore;
using Newtonsoft.Json.Linq;
using OpcProxyClient;
//using OpcGrpcConnect;
using OpcInfluxConnect;
//using opcKafkaConnect;
using opcRESTconnector;
using opcPrometheus;

namespace OPC_Proxy
{
    class Program
    {
 
        static int Main(string[] args)
        {
            // instantiaing the manager, this will load configuration from file or args
            serviceManager manager = new serviceManager(args);

            // getting the raw configuration 
            JObject raw_conf = manager.getRawConfig();
            standaloneConfigs user_config = raw_conf.ToObject<standaloneConfigs>();
/*            if(user_config.grpcConnector) {
                HttpImpl opcHttpConnector = new HttpImpl();
                manager.addConnector(opcHttpConnector);
            }
	    */
            if(user_config.influxConnector){
                InfluxConnect influx = new InfluxConnect();
                manager.addConnector(influx);
            }
	    /*
            if(user_config.kafkaConnector){
                KafkaConnect kafka = new KafkaConnect();
                manager.addConnector(kafka);
            }*/
            if(user_config.RESTConnector){
                opcREST rest = new opcREST();
                manager.addConnector(rest);
            }

	    if(user_config.PromConnector){
		    opcPromConnect prom = new opcPromConnect();
		    manager.addConnector(prom);
	    }

            manager.run();
            return 0; 
        }
        
    }

    /// <summary>
    /// Additional configuration class only for the standalone OPC-Proxy executable
    /// </summary>
    public class standaloneConfigs {
        public bool grpcConnector {get;set;}
        public bool influxConnector {get;set;}
        public bool kafkaConnector {get;set;}
        public bool RESTConnector {get;set;}
        public bool PromConnector {get;set;}

        public standaloneConfigs(){
            grpcConnector = false;
            influxConnector = false;
            kafkaConnector = false;
            RESTConnector = false;
            PromConnector = false;
        }
    }  
}
