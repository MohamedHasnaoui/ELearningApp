namespace ELearningApp.Hubs
{
    public interface IMessageHub
    {
        Dictionary<string, IEnumerable<string>> GetAllConnectedUsers();
        DateTime? GetLastConnectionTime(string userId);
    }
}
