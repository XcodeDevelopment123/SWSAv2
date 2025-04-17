using SWSA.MvcPortal.Commons.Services.Messaging.Enums;

namespace SWSA.MvcPortal.Commons.Services.Messaging;

// 说明：消息结构体，封装所有要发送的信息（通道、接收者、模板、参数）。未来也可加字段如 MessageId、RetryCount 等。
public class MessageEnvelope
{
    public MessagingChannel Channel { get; set; }
    public string Recipient { get; set; } = null!;
    public string TemplateCode { get; set; } = null!;
    public Dictionary<string, string> Data { get; set; } = new();
}


public class MessagingResult
{
    public bool IsSuccess { get; set; } = false;
    public string Message { get; set; } = string.Empty;
    public MessagingResult(bool success, string message)
    {
        IsSuccess = success;
        Message = message;
    }

}