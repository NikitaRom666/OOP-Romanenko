FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src


COPY *.csproj ./
RUN dotnet restore


COPY . ./
RUN dotnet publish -c Release -o /app/publish


FROM mcr.microsoft.com/dotnet/runtime:9.0 AS runtime
WORKDIR /app


LABEL maintainer="YourName"
LABEL version="1.0"
LABEL description="MyDockerApp — лабораторна робота №38"


RUN useradd -m appuser
USER appuser


COPY --from=build /app/publish ./


HEALTHCHECK --interval=30s --timeout=3s --retries=3 \
  CMD dotnet MyDockerApp.dll --health 2>/dev/null || exit 0

ENTRYPOINT ["dotnet", "MyDockerApp.dll"]