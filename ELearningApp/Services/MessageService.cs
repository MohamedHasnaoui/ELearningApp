using ELearningApp.Data;
using ELearningApp.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ELearningApp.Services
{
    public class MessageService : IMessageService
    {
        private readonly ApplicationDbContext _context;
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public MessageService(ApplicationDbContext context, IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            _context = context;
            _dbContextFactory = dbContextFactory;
        }

        public async Task<bool> SaveMessageAsync(Message message)
        {
            await _context.Messages.AddAsync(message);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
        public async Task<List<(ApplicationUser User, Message? LastMessage, bool IsOnline, int NbrUnseenMsg)>> GetAllUsersOrderByLastMessageAsync(string currentUserId)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();

            // Get the current user and determine their type
            var currentUser = await dbContext.Users.OfType<ApplicationUser>()
                .FirstOrDefaultAsync(u => u.Id == currentUserId);

            if (currentUser == null)
            {
                throw new Exception("User not found.");
            }

            IQueryable<ApplicationUser> targetUsers;

            // Logic based on the user type
            if (currentUser is Etudiant)
            {
                // Get all Enseignant users who either follow this Etudiant or have a course started by this Etudiant
                targetUsers = dbContext.Users.OfType<Enseignant>()
                    .Where(e =>
                        e.Followers.Any(f => f.EtudiantId == currentUserId) || // Check if the `Enseignant` is followed by the `Etudiant`
                        dbContext.CoursCommences.Any(cc => cc.Cours.EnseignantId == e.Id && cc.EtudiantId == currentUserId) // Check if the `Etudiant` has started a course taught by this `Enseignant`
                    );


            }
            else if (currentUser is Enseignant)
            {
                // Get all Etudiant users who have started a course created by this Enseignant or are followers
                targetUsers = dbContext.Users.OfType<Etudiant>()
                    .Where(e =>
                        e.CoursCommences.Any(cc => cc.Cours.EnseignantId == currentUserId) ||
                        e.Following.Any(f => f.EnseignantId == currentUserId));
            }
            else
            {
                targetUsers = dbContext.Users
                    .Where(u => u.Id != currentUserId);
            }

            // Fetch users with their last messages and unseen message count
            var usersWithLastMessages = await targetUsers
                .Select(u => new
                {
                    User = u,
                    LastMessage = dbContext.Messages
                        .Include(m => m.Sender)
                        .Include(m => m.Receiver)
                        .Where(m => (m.SenderId == currentUserId && m.ReceiverId == u.Id) ||
                                    (m.SenderId == u.Id && m.ReceiverId == currentUserId))
                        .OrderByDescending(m => m.SentAt)
                        .FirstOrDefault(),
                    NbrUnseenMsg = dbContext.Messages
                        .Where(m => m.ReceiverId == currentUserId &&
                                    m.SenderId == u.Id &&
                                    !m.Status)
                        .Count()
                })
                .OrderByDescending(x => x.LastMessage.SentAt)
                .ToListAsync();

            // Map results to the desired tuple format
            return usersWithLastMessages
                .Select(x => (x.User, x.LastMessage, false, x.NbrUnseenMsg))
                .ToList();
        }

        public async Task<List<(ApplicationUser User, Message? LastUnseenMessage)>> GetUsersWithUnseenMessages(string currentUserId)
        {
            var usersWithUnseenMessages = await _context.Users
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
                .ToListAsync();

            return usersWithUnseenMessages
                .Select(x => (x.User, x.LastUnseenMessage)) // Tuple of user, message
                .ToList();
        }
        public async Task<List<(ApplicationUser User, Message? LastUnseenMessage)>> GetUsersWithUnseenMessages2(string currentUserId)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();
            var usersWithUnseenMessages = await dbContext.Users
                .Where(u => u.Id != currentUserId) // Exclude the current user
                .Select(u => new
                {
                    User = u,
                    LastUnseenMessage = dbContext.Messages
                        .Include(m => m.Sender)
                        .Include(m => m.Receiver)
                        .Where(m =>
                            m.Status == false && // Only include unseen messages
                            ((m.SenderId == currentUserId && m.ReceiverId == u.Id) || // Sent by the current user
                             (m.SenderId == u.Id && m.ReceiverId == currentUserId))) // Received by the current user
                        .OrderByDescending(m => m.SentAt) // Order by latest message
                        .FirstOrDefault()
                })
                .Where(x => x.LastUnseenMessage != null && x.LastUnseenMessage.SenderId != currentUserId) // Only include users with unseen messages
                .OrderByDescending(x => x.LastUnseenMessage.SentAt) // Order users by latest unseen message time
                .ToListAsync();

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
