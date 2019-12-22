# opcProxySample

# Build

#### Docker
```bash
dotnet publish -c Release -r linux-musl-x64 --self-contained true 
docker build -t opc_proxy -f Dockerfile .
# create a directory, the config file must be named "proxy_config.json" 
docker create --name foo -v absolute_path_to_config_dir:/app/configs  --network="host" opc_proxy 
docker start -i foo
```
