using ELearningApp.Data;
using ELearningApp.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ELearningApp.Services
{
    public class MessageService : IMessageService
    {
        private readonly ApplicationDbContext _context;

        public MessageService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveMessageAsync(Message message)
        {
            await _context.Messages.AddAsync(message);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
        public async Task<List<(ApplicationUser User, Message? LastMessage, bool IsOnline, int NbrUnseenMsg)>> GetAllUsersOrderByLastMessageAsync(string currentUserId)
        {
            var usersWithLastMessages = await _context.Users
                .Where(u => u.Id != currentUserId) // Exclude the current user
                .Select(u => new
                {
                    User = u,
                    LastMessage = _context.Messages
                        .Include(m => m.Sender)
                        .Include(m => m.Receiver)
                        .Where(m => (m.SenderId == currentUserId && m.ReceiverId == u.Id) ||
                                    (m.SenderId == u.Id && m.ReceiverId == currentUserId))
                        .OrderByDescending(m => m.SentAt)
                        .FirstOrDefault(), // Get the latest message
                    NbrUnseenMsg = _context.Messages
                        .Where(m => m.ReceiverId == currentUserId &&
                                    m.SenderId == u.Id &&
                                    !m.Status)
                        .Count()
                })
                .OrderByDescending(x => x.LastMessage.SentAt) // Order users by latest message time
                .ToListAsync(); // Use ToListAsync for asynchronous operation

            return usersWithLastMessages
                .Select(x => (x.User, x.LastMessage, false, x.NbrUnseenMsg)) // Tuple of user, message, online status, unseen count
                .ToList();
        }

        public List<(ApplicationUser User, Message? LastUnseenMessage)> GetUsersWithUnseenMessages(string currentUserId)
        {
            var usersWithUnseenMessages = _context.Users
                .Where(u => u.Id != currentUserId) // Exclude the current user
                .Select(u => new
                {
                    User = u,
                    LastUnseenMessage = _context.Messages
                        .Include(m => m.Sender)
                        .Include(m => m.Receiver)
                        .Where(m =>
                            m.Status == false && // Only include unseen messages
                            ((m.SenderId == currentUserId && m.ReceiverId == u.Id) || // Sent by the current user
                             (m.SenderId == u.Id && m.ReceiverId == currentUserId))) // Received by the current user
                        .OrderByDescending(m => m.SentAt) // Order by latest message
                        .FirstOrDefault()
                })
                .Where(x => x.LastUnseenMessage != null && x.LastUnseenMessage.SenderId !=currentUserId) // Only include users with unseen messages
                .OrderByDescending(x => x.LastUnseenMessage.SentAt) // Order users by latest unseen message time
                .ToList();

            return usersWithUnseenMessages
                .Select(x => (x.User, x.LastUnseenMessage)) // Tuple of user, message
                .ToList();
        }


        public async Task<List<Message>> GetConversationAsync(string userId1, string userId2)
        {
            return await _context.Messages
                .Include(m => m.Sender)
                .Include(m => m.Receiver)
                .Where(m =>
                    (m.SenderId == userId1 && m.ReceiverId == userId2) ||
                    (m.SenderId == userId2 && m.ReceiverId == userId1))
                .OrderBy(m => m.SentAt)
                .ToListAsync();
        }
        public async Task MarkAllAsSeen(string currentUserId, string senderId)
        {
            // Get all messages sent by the specified sender to the current user that are not yet seen
            var messages = _context.Messages
                .Where(m => m.ReceiverId == currentUserId &&
                            m.SenderId == senderId &&
                            !m.Status)
                .ToList();

            if (messages.Any())
            {
                // Mark all as seen
                foreach (var message in messages)
                {
                    message.Status = true;
                }

                // Save changes to the database
                await _context.SaveChangesAsync();
            }
        }


    }
}
