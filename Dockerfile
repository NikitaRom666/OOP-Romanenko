FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "UnitTestingDemo.sln"
RUN dotnet publish "UnitTestingDemo.sln" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/runtime:9.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "MyProject.dll"]
