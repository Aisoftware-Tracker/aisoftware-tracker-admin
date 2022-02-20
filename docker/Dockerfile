FROM gcr.io/google-appengine/aspnetcore:3.1
ADD ./ /app 
ENV ASPNETCORE_URLS=http://*:${PORT}
WORKDIR /app
ENTRYPOINT [ "dotnet", "Aisoftware.Tracker.Admin.dll"]