FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# 拷貝項目文件
COPY ["ToDoListApp/ToDoListApp.csproj", "./ToDoListApp/"]
COPY ["ToDoListTest/ToDoListTest.csproj", "./ToDoListTest/"]

# 還原依賴
RUN dotnet restore ./ToDoListTest/ToDoListTest.csproj

# 拷貝所有代碼
COPY . .

# 編譯測試項目
RUN dotnet build ./ToDoListTest/ToDoListTest.csproj -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish ./ToDoListTest/ToDoListTest.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ToDoListTest.dll"]
