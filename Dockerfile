FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
ADD . .
ENTRYPOINT ["dotnet", "FewBox.Service.DBJob.dll"]