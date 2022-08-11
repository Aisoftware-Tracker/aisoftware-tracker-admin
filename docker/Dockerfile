FROM gcr.io/google-appengine/aspnetcore:6.0
ADD ./ /app 
ENV ASPNETCORE_URLS=http://*:${PORT}
WORKDIR /app
ENTRYPOINT [ "dotnet", "Aisoftware.Tracker.Admin.dll"]
