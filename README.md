## Docker Compose 筆記

## 流程

1. **確認並修改sql server連線位址**
2. [確認根目錄位置](#根目錄)
3. [確認Docker Compose版本](#docker-compose-version)
4. [檢查並了解Docker Compose中的路徑配置](#檢查並了解docker-compose中的路徑配置)
5. [執行Docker Compose Up](#docker-compose-up)
6. [執行 SQL COMMAND](#sql-command)
7. 測試網址: http://localhost:5035/index.html
8. [移除 Docker Compose](#移除)
9. [檢查是否正確移除](#查看是否有正確移除)

## [根目錄](#流程)

```shell
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

## [Docker Compose Version](#流程)

```shell
PS> wsl docker compose version                        
Docker Compose version v2.27.1
```

## [檢查並了解Docker Compose中的路徑配置](#流程)

```markdown
.
├── docker-compose.yml
├── ToDoListApp/
│ ├── Dockerfile
│ ├── ToDoListApp.csproj
├── ToDoListTest/
│ ├── Dockerfile
│ ├── ToDoListTest.csproj

```

context: 用來搜尋 dockerfile 的根目錄

```yaml
build:
  context: ./ToDoListApp
```

上述配置中，Docker 引擎將以 ToDoListApp 作為根目錄搜尋 dockerfile。

dockerfile: 可以設定更上層的目錄為根目錄，再使用dockerfile指定dockerfile的位置。

```yaml
build:
  context: .
  dockerfile: ./ToDoListTest/Dockerfile
```

### 心得：

構建 Docker 映像需要滿足以下條件：

* Dockerfile 的構建腳本：用於定義如何構建映像。
* 必需的構建文件：包括代碼、配置文件等，供 Dockerfile 的指令（如 COPY 和 ADD）使用。
  context 的作用：

* context 是構建過程的根目錄，必須包含 Dockerfile 所需的所有文件。
* context 必須與 Dockerfile 中的路徑設定一致，保證 Dockerfile 能正確引用文件。

## [Docker Compose Up](#流程)

```pwsh
PS> wsl docker compose -p todoproject up
```

## [SQL COMMAND](#流程)

```sql
CREATE
DATABASE app;
GO

USE app;
GO

CREATE TABLE ToDo
(
    Id          INT IDENTITY(1,1) PRIMARY KEY, -- 自動遞增的主鍵
    Title       NVARCHAR(255) NOT NULL,        -- 非空字符串，最多 255 字符
    IsCompleted BIT NOT NULL                   -- 布林值 (0 或 1)，非空
);
```

## [移除](#流程)

```shell
PS> wsl docker compose -p todoproject down --rmi all --volumes
```

## [查看是否有正確移除](#流程)

```shell
PS> wsl docker ps -a
PS> wsl docker image 
PS> wsl docker volume ls
PS> wsl docker network ls
```