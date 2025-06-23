using Microsoft.AspNetCore.SignalR;

public class NotificationHub : Hub
{
    public string GetConnectionId()
    {
        return Context.ConnectionId;
    }

    public async Task SendNotification(string user, object notification)
    {
        await Clients.User(user).SendAsync("receiveNotification", notification);
    }

    public async Task SendNotificationToAll(object notification)
    {
        await Clients.All.SendAsync("receiveNotification", notification);
    }

}