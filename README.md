## 流程
1. **確認並修改sql server連線位址**
2. 確認根目錄位置
3. 確認Docker Compose版本
4. 執行Docker Compose Up
5. 執行 SQL COMMAND
6. 測試網址: http://localhost:5035/index.html
7. 移除 Docker Compose
8. 檢查是否正確移除

## 根目錄
```pwsh
PS> ls
Mode                 LastWriteTime         Length Name
----                 -------------         ------ ----
d----        2024/11/30 上午 11:15                .idea
d----        2024/11/30 下午 12:08                ToDoListApp
d----        2024/11/30 下午 12:39                ToDoListTest
-a---        2024/11/29 下午 11:51           1852 .gitignore
-a---        2024/11/30 下午 12:09            541 docker-compose.yml
-a---        2024/11/30 下午 12:38              0 README.md
-a---        2024/11/30 上午 11:17           1544 ToDoListApp.sln
```

## Docker Compose Version
```pwsh
PS> wsl docker compose version                        
Docker Compose version v2.27.1
```

## Docker Compose Up
```pwsh
PS> wsl docker compose -p todoproject up
```

## SQL COMMAND
```SQL
CREATE DATABASE app;
GO

USE app;
GO

CREATE TABLE ToDo (
                      Id INT IDENTITY(1,1) PRIMARY KEY,  -- 自動遞增的主鍵
                      Title NVARCHAR(255) NOT NULL,      -- 非空字符串，最多 255 字符
                      IsCompleted BIT NOT NULL           -- 布林值 (0 或 1)，非空
);
```

## 移除
```pwsh
PS> wsl docker compose -p todoproject down --rmi all --volumes
```

## 查看是否有正確移除
```pwsh
PS> wsl docker ps -a
PS> wsl docker image 
PS> wsl docker volume ls
PS> wsl docker network ls
```