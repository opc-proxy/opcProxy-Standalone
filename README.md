# OPC-Proxy Standalone Exacutable

Standalone OPC-Proxy, runs a configurable opc-proxy with GRPC, InfluxDB and Kafka registered endpoint.

# Getting Started

Follow the docs at [opc-proxy.readthedocs.io](https://opc-proxy.readthedocs.io/en/latest/GettingStarted/docker.html).

Requirements:

- Install .NET Core >= 2.2 (all three library: .NET core SDK, .NET core Runtime, ASP .NET core runtime.)
- A test OPC-server, we suggest [Node-OPCUA](https://github.com/node-opcua/node-opcua-sampleserver) if you are familiar with NodeJS.


### Start with Docker

``` bash
docker pull openscada/opc-proxy
```


### OR Build and Run
```bash
dotnet build
dotnet run
```


### Minimal Config file
Create a config file named ```proxy_config.json```:

``` js
/* proxy_config.json */
    {
        "opcServerURL":"opc.tcp://localhost:4334/UA/MyLittleServer",

        "loggerConfig" :{
            "loglevel" :"debug"
        },
        
        "nodesLoader" : {
            "targetIdentifier" : "browseName", 
            "whiteList":["MyVariable1"]
        },
        "httpConnector" :   false,
        "influxConnector" : false,
        "kafkaConnector":   false
    }

```
This will tell the OPC-Proxy that:

- Needs to connect to an OPC server at the specified URL, which is the default for **nodeOPCUA-simpleServer**, 
  if you are using another test server you need to update that line.
- The nodesLoader here will match against a whitelist all nodes of the server, it will look for a Node with ``BrowseName`` attribute
  equals to  ``MyVariable1``, which is default for our test server.
- The log level is set to ``DEBUG``, so that we will see the output of the variable changing.
- All connectors are set to ``false``, meaning that this proxy will only connect to the opc-server and nothing more.

# Build the Docker image
```bash
dotnet publish -c Release -r linux-musl-x64 --self-contained true 
docker build -t openscada/opc-proxy -f Dockerfile .
# create a directory, the config file must be named "proxy_config.json" 
docker create --name foo -v absolute_path_to_config_dir:/app/configs  --network="host" openscada/opc-proxy 
docker start -i foo
```
