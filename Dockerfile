FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Use the SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["parc/parc.csproj", "parc/"]
RUN dotnet restore "parc/parc.csproj"
COPY . .
WORKDIR "/src/parc"
RUN dotnet build "parc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "parc.csproj" -c Release -o /app/publish

# Copy the build output to the base image and set the entry point
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "YourApi.dll"]
