# Quartz Job Extension & Structure Guide

## 简介 Overview

Quartz 是一个强大的 .NET 分组件用于程序性的任务调度和定时执行。本项目采用带有构建结构的 Quartz 扩展设计，便于后续维护和扩展新任务。

## 项目结构 Project Structure

### ① BaseJobFactory

- 默认设置与 Job 构造过程的基类
- 禁止修改，除非确定其作用
- 如需特殊行为，可 `override` 重写子类方法

### ② QuartzJobFactory

- 支持 DI （依赖注入）能力的工厂类
- 禁止修改

### ③ Jobs Folder

- 每一个工作任务主体逻辑所在
- 使用 DI 获取服务或仓库执行逻辑

### ④ Requests Folder

- 对于需要传递参数的任务，创建独立的 JobRequest 类
- 继承基类并增加自定义属性

### ⑤ Services Folder

- 将逻辑抽离为 Job Service 便于重用/单独调用
- Job 调用 service.方法，而 service 只连 repo/第三方 API

### ⑥ JobSchedulerService

- Quartz 主调度器类
- 禁止修改

### ⑦ Support Folder

包含各类帮助类:

#### JobBuildContext

- 全局基础参数管理
- 禁止随意增加非全局性值

#### JobConstants

- 定义 Job 中用到的 JobKey/ScheduleType/Group

#### JobExecutionResolver

- 处理 DB 中的任务设置 -> 实际 Job 映射
- 每新增 Job ，必须更新 GetJobKeyFromEnum()

#### JobHelper / JobDataBinder

- 自动将 JobRequest 中值 map 到 job 执行时用

#### JobRequestMapper

- 从 context 转换到 job request

#### JobMetadataRegistry

- 新 Job 必须在 Register()中注册 (key, factory, request)
- 禁止修改其他部分

#### QuartzJobListener

- 全局 job 执行监听器（开始/失败/结束）
- 可扩展添加全局行为

---

## 扩展新 Job 流程 Extension Workflow

1. 定义 **ScheduledJobType** (enum)
2. 更新 **JobConstants/JobExecutionResolver** 里的 JobKey
3. 新增 **Job Request** 文件（如需传参）
4. 在 **Jobs** 文件夹中创建新 job
5. 如需，新增应用性工厂类 (**Factory**)
6. **JobMetadataRegistry.Register()** 注册 jobKey, factory, request
7. 在 **JobExecutionResolver.GetJobKeyFromEnum()** 添加 enum 和 key name
8. 在 **DI 注入配置** 中注册:
   - Factory = `AddTransient`
   - Job = `AddScoped`

---

## 第三方文档 Third-Party Documentation

- [Quartz.NET 官方文档 (v3.x)](https://www.quartz-scheduler.net/documentation/)
  > Covers architecture, job and trigger setup, listeners, DI, etc.

---

# Quartz Job Extension & Structure Guide

## Overview

Quartz is a powerful .NET component for programmatic task scheduling and timed execution. This project uses the Quartz extension design with a build structure to facilitate subsequent maintenance and expansion of new tasks.

## Project Structure

### ① BaseJobFactory

- Base class for default settings and job construction process
- Modification prohibited unless its function is determined
- If special behavior is required, `override` can be used to rewrite the subclass method

### ② QuartzJobFactory

- Factory class that supports DI (dependency injection) capabilities
- Modification prohibited

### ③ Jobs Folder

- The main logic of each work task is located
- Use DI to obtain service or warehouse execution logic

### ④ Requests Folder

- For tasks that need to pass parameters, create an independent JobRequest class
- Inherit the base class and add custom properties

### ⑤ Services Folder

- Extract the logic into Job Service for easy reuse/separate call
- Job calls service. method, and service only connects to repo/third-party API

### ⑥ JobSchedulerService

- Quartz main scheduler class
- Modification prohibited

### ⑦ Support Folder

Contains various helper classes:

#### JobBuildContext

- Global basic parameter management
- Do not add non-global values ​​at will

#### JobConstants

- Define JobKey/ScheduleType/Group used in Job

#### JobExecutionResolver

- Process task settings in DB -> actual Job mapping
- GetJobKeyFromEnum() must be updated for each new Job

#### JobHelper / JobDataBinder

- Automatically map the value in JobRequest to the job execution

#### JobRequestMapper

- Convert from context to job request

#### JobMetadataRegistry

- New Job must be registered in Register() (key, factory, request)
- Do not modify other parts

#### QuartzJobListener

- Global job execution listener (start/failure/end)
- Can be extended to add global behavior

---

## Extend new Job Process Extension Workflow

1. Define **ScheduledJobType** (enum)
2. Update the JobKey in **JobConstants/JobExecutionResolver**
3. Add **Job Request** file (if you need to pass parameters)
4. Create a new job in the **Jobs** folder
5. If necessary, add an application factory class (**Factory**)
6. **JobMetadataRegistry.Register()** register jobKey, factory, request
7. Add enum and key name in **JobExecutionResolver.GetJobKeyFromEnum()**
8. Register in **DI injection configuration**:

- Factory = `AddTransient`
- Job = `AddScoped`

---

## Third-Party Documentation

- [Quartz.NET Official Documentation (v3.x)](https://www.quartz-scheduler.net/documentation/)
  > Covers architecture, job and trigger setup, listeners, DI, etc.

---
