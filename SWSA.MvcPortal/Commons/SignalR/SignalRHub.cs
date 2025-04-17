using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Serilog;
using SWSA.MvcPortal.Commons.Extensions;

namespace SWSA.MvcPortal.Commons.SignalR;


//Client method
//[Authorize]
public class SignalRHub : Hub
{
    // 连接成功时触发
    public override async Task OnConnectedAsync()
    {
        //Add staff to group 
        //var staffId = Context.UserIdentifier;
        //if (staffId != null)
        //{
        //    await Groups.AddToGroupAsync(Context.ConnectionId, $"User_{staffId}");
        //}

        await base.OnConnectedAsync();
    }

    // 断开连接时触发
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        //var staffId = Context.UserIdentifier;
        //if (staffId != null)
        //{
        //    await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"User_{staffId}");
        //}
        await base.OnDisconnectedAsync(exception);
    }

    // 向一个组发送消息
    public async Task SendMessageToGroup(SignalRGroupType groupName, string message)
    {
       Log.Information($"Send to {groupName}, Message :{message}");

        await Clients.Group(groupName.GetDisplayName()).SendAsync("ReceiveMessage", message);
    }

    // 向一个用户发送消息
    public async Task SendMessageToUser(string staffId, string message)
    {
        Log.Information($"Send to staff {staffId}, Message :{message}");

        await Clients.User(staffId).SendAsync("ReceiveMessage", message);
    }

    public async Task AddStaffToGroup(string staffId)
    {
        var groupName = $"Group_{staffId}";
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

        Log.Information($"Staff {staffId} with Connection {Context.ConnectionId} added to group {groupName}");
    }

}