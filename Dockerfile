FROM mcr.microsoft.com/dotnet/core/aspnet:6.0
WORKDIR /app
ADD . .
ENTRYPOINT ["dotnet", "FewBox.Service.DBJob.dll"]