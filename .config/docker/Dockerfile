FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY Aisoftware.Tracker.Admin/*.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish Aisoftware.Tracker.Admin -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .

CMD ASPNETCORE_URLS="http://*:$PORT" dotnet Aisoftware.Tracker.Admin.dll