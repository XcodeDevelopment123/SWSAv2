
namespace SWSA.MvcPortal.Commons.SignalR;


public class SignalRMessage<T>
{
    public string Message { get; set; }
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string? StaffId { get; set; }
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public T? Data { get; set; }

    public SignalRMessage(string message)
    {
        Message = message;
    }

    public SignalRMessage(string message, string? staffId, T? data)
    {
        Message = message;
        StaffId = staffId;
        Data = data;
    }

    public string ToJson()
    {
        return JsonConvert.SerializeObject(this);
    }

    //public async Task SendPermissionChangeToUser(IHubContext<NotificationHub> hubContext, int userId)
    //{
    //    var eventName = SignalREventType.PermissionChange.ToString();
    //    await hubContext.Clients.User(userId.ToString()).SendAsync(eventName, this.ToJson());
    //}
}