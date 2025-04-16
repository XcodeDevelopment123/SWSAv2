我正在开发一个 Messaging Queue 模块，目标是支持多个消息通道（如 Email、SMS、WhatsApp），具有以下要求：

1. 当前为 MVP 阶段，暂时不接入 Redis、RabbitMQ 等外部服务，只使用内存队列或 Channel<T> 实现。
2. 每个消息包含：
   - Channel 类型（Email, SMS, WhatsApp）
   - Recipient
   - TemplateCode（如 OTP、通知、报表）
   - 参数字典（Data）供模板渲染
3. 要求支持：
   - Dispatcher 自动校验模板是否允许使用当前 Channel
   - 可扩展新的 Sender 类型与模板（例如：未来加入 LINE / Telegram / 系统通知）
   - Worker 异步消费队列并调用对应的 Sender
4. 抽象良好，方便未来迁移至生产环境（替换为 Redis/RabbitMQ 实现）
5. 可选：支持模板渲染、日志记录、失败重试机制

🎯 请帮我生成：
- 接口设计（Producer, Consumer, Dispatcher, TemplateRegistry, Sender）
- 内存队列实现（MVP 可运行）
- Worker 实现（支持 BackgroundService）
- 示例用法（如何 Dispatch 一条 OTP 短信）
- 扩展建议（如何支持 Report / Template 渲染）

用 C# 编写，关注可维护性与未来扩展性。


📌 使用建议：
想增加新通道时：在最后加上 请一并帮我新增对 LINE 的 Sender 支持

想转为消息中间件时：加上 请替换 InMemoryQueue 为 Redis Stream/RabbitMQ

想支持多语言模板时：加上 请扩展 TemplateEngine 支持 en / zh-Hans 切换

想连接 UI 表单时：加上 请提供 MessageRequest 的前端 API 接收格式定义

