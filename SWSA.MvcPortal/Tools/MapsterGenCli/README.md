
# MapsterGenCli

`MapsterGenCli` 是一个轻量级命令行工具，用于扫描实体类目录并自动生成 [Mapster](https://github.com/MapsterMapper/Mapster) 的配置类，同时将注册代码插入到指定的 `MapsterConfig.cs` 文件中。该工具适用于 ASP.NET MVC 项目，支持通过 `appsettings.json` 配置路径和命名空间。

---

## 📦 功能特性

- 根据实体类自动生成 `xxxMapsterConfig.cs` 配置文件（仅生成一次，不覆盖已有文件）。
- 自动插入 `new XxxMapsterConfig(),` 注册语句到 `MapsterConfig.cs` 的指定标记区域。
- 使用配置文件集中管理路径和命名空间。

---

## 📁 示例项目结构

```
SWSA.MvcPortal/
├── Entities/
│   └── Company.cs
├── Commons/
│   ├── MapsterConfigs/
│   │   ├── CompanyMapsterConfig.cs
│   │   └── MapsterConfig.cs
├── Tools/
│   └── MapsterGenCli/
│       ├── MapsterGenCli.exe
│       └── appsettings.json
```

---

## ⚙️ appsettings.json 配置说明

```json
{
  "entitiesPath": "../../../SWSA.MvcPortal/Entities",
  "outputPath": "../../../SWSA.MvcPortal/Commons/MapsterConfigs",
  "mapsterConfigFile": "../../../SWSA.MvcPortal/Commons/MapsterConfigs/MapsterConfig.cs",
  "entityNamespace": "SWSA.MvcPortal.Entities",
  "configNamespace": "SWSA.MvcPortal.Commons.MapsterConfigs"
}
```

| 字段名              | 说明                                 |
|---------------------|--------------------------------------|
| `entitiesPath`      | 实体类所在路径（支持相对路径）       |
| `outputPath`        | 自动生成的配置类输出路径             |
| `mapsterConfigFile` | `MapsterConfig.cs` 文件路径          |
| `entityNamespace`   | 实体类的命名空间                     |
| `configNamespace`   | 自动生成的配置类使用的命名空间       |

---

## 🚀 使用方式

1. **配置 appsettings.json**  
   确保路径正确指向你的项目结构。

2. **执行命令行工具**  
   在 CLI 中进入 `MapsterGenCli` 所在目录，运行：
   ```bash
   .\MapsterGenCli.exe
   ```

3. **查看输出**
   工具会：
   - 为每个实体生成一个对应的 `XxxMapsterConfig.cs` 文件（如果未存在）。
   - 向 `MapsterConfig.cs` 的 `//#Mapster Config (auto generated)` 和 `//#Mapster Config end` 之间添加注入代码。

---

## ✏️ MapsterConfig.cs 标记示例

确保 `MapsterConfig.cs` 中有以下标记：

```csharp
// 注入 Mapster 配置
var configs = new List<IMapsterConfig>
{
    //#Mapster Config (auto generated)
    new CompanyMapsterConfig(),
    new UserMapsterConfig(),
    //#Mapster Config end
};
```

---

## 📝 注意事项

- 工具只在目标 `MapsterConfig.cs` 文件存在指定标记时生效。
- 不会覆盖已有的配置文件，只创建缺失的项。
- 可手动运行工具，适用于版本控制，不推荐自动构建时运行。

---

## 🔗 相关链接

- Mapster 官方文档：https://github.com/MapsterMapper/Mapster

---
