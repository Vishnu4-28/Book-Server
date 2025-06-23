namespace E_commerce.Server.Hubs
{
    public interface INotification
    {
        Task ReceiveNotification(string message); 
    }
}
