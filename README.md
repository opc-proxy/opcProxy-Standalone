# opcProxySample

# Build

#### Docker
```bash
dotnet publish -c Release -r linux-musl-x64 --self-contained true 
docker build -t openscada/opc-proxy -f Dockerfile .
# create a directory, the config file must be named "proxy_config.json" 
docker create --name foo -v absolute_path_to_config_dir:/app/configs  --network="host" openscada/opc-proxy 
docker start -i foo
```
