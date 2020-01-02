# OPC-Proxy Standalone Executable

Standalone OPC-Proxy, runs a configurable opc-proxy with GRPC, InfluxDB and Kafka registered endpoint.


## Docs at [opc-proxy.readthedocs.io](https://opc-proxy.readthedocs.io/en/latest/GettingStarted/docker.html)


# Getting Started


Requirements:

- Install .NET Core >= 3.1 (all three library: .NET core SDK, .NET core Runtime, ASP .NET core runtime.)
- A test OPC-server, we suggest the [Python-OPCUA](https://github.com/FreeOpcUa/python-opcua/blob/master/examples/server-minimal.py) or the [Node-OPCUA](https://github.com/node-opcua/node-opcua-sampleserver) if you are familiar with NodeJS.

The .NET dependencies are not needed if you run it with Docker.


### Run it with Docker

Start your OPC-test server first.

``` bash
docker pull openscada/opc-proxy
mkdir local_config  # host dir to share configs
wget -O local_config/proxy_config.json  https://raw.githubusercontent.com/opc-proxy/opcProxy-Standalone/master/proxy_config.json
cd local_config
OPC_LOCAL_CONF=$(pwd)
docker create --name proxy_test --network="host" -v ${OPC_LOCAL_CONF}:/app/configs openscada/opc-proxy
docker start -i proxy_test
```


### OR Build with .NET and Run

Start your OPC-test server first.

```bash
git clone git@github.com:opc-proxy/opcProxy-Standalone.git
cd opcProxy-Standalone/
dotnet build
dotnet run
```


### Minimal Config file
Create a config file named ```proxy_config.json```:

``` js
/* proxy_config.json */
{
       "opcServerURL":"opc.tcp://localhost:4840/freeopcua/server/",

       "loggerConfig" :{
        "loglevel" :"debug"
       },

       "nodesLoader" : {
        "targetIdentifier" : "browseName",
        "whiteList":["MyVariable"]
       }
        
       "httpConnector" :   false,
       "influxConnector" : false,
       "kafkaConnector":   false
}

```
This will tell the OPC-Proxy that:

- Needs to connect to an OPC server at the specified URL, the config is for the [Python minimal server example](https://github.com/FreeOpcUa/python-opcua/blob/master/examples/server-minimal.py), 
  if you are using another test server you need to update that line.
- The nodesLoader here will match against a whitelist all nodes of the server, it will look for a Node with ``BrowseName`` attribute
  equals to  ``MyVariable``, which is default for our test server.
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
