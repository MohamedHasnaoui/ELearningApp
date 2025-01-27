using ELearningApp.Data;
using ELearningApp.Model;

namespace ELearningApp.Services
{
    public interface IMessageService
    {
        Task<bool> SaveMessageAsync(Message message);
        Task<List<(ApplicationUser User, Message? LastMessage, bool IsOnline, int NbrUnseenMsg)>> GetAllUsersOrderByLastMessageAsync(string currentUserId);
        Task<List<Message>> GetConversationAsync(string userId1, string userId2);
        Task<List<(ApplicationUser User, Message? LastUnseenMessage)>> GetUsersWithUnseenMessages(string currentUserId);
        Task<List<(ApplicationUser User, Message? LastUnseenMessage)>> GetUsersWithUnseenMessages2(string currentUserId);
        Task MarkAllAsSeen(string currentUserId, string senderId);
    }
}
