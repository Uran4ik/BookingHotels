FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80   
ENV ASPNETCORE_ENVIRONMENT=Development

WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["BookingHotels/BookingHotels.csproj", "BookingHotels/"]
RUN dotnet nuget locals all --clear && dotnet restore "BookingHotels/BookingHotels.csproj"
RUN dotnet restore "BookingHotels/BookingHotels.csproj"

COPY . .
FROM build AS publish 
RUN dotnet publish "BookingHotels/BookingHotels.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "BookingHotels.dll" ]