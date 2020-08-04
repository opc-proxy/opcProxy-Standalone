FROM  mcr.microsoft.com/dotnet/core/runtime-deps:3.1.0-alpine3.10

RUN apk add --update --no-cache \
    gcompat \
    libc6-compat \
    librdkafka-dev
COPY bin/Release/netcoreapp2.2/linux-musl-x64/publish/ app/
COPY Opc.Ua.SampleClient.Config.xml / 
CMD  app/opcProxySample --config app/configs/proxy_config.json
