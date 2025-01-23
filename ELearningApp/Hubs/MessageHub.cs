using Blazorise;
using ELearningApp.Data;
using ELearningApp.Model;
using ELearningApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using System.Security.Claims;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace ELearningApp.Hubs
{
    public class MessageHub : Hub, IMessageHub
    {
        // Thread-safe dictionary to track connected users and their connection IDs
        private static readonly ConcurrentDictionary<string, HashSet<string>> _userConnections = new();
        private static readonly ConcurrentDictionary<string, DateTime> _userLastConnectionTime = new();
        
        public override async Task OnConnectedAsync()
        {
            //var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            //var user = authState.User;
            var httpContext = Context.GetHttpContext();
            if (httpContext != null)
            {
                // Retrieve the custom data from the query string
                var userId = httpContext.Request.Query["userId"];
                if (!string.IsNullOrEmpty(userId))
                {
                    // Add the connection ID to the user's connection list
                    _userConnections.AddOrUpdate(userId,
                        new HashSet<string> { Context.ConnectionId },
                        (key, existingConnections) =>
                        {
                            existingConnections.Add(Context.ConnectionId);
                            return existingConnections;
                        });

                    // Associate the connection with the user ID
                    await Groups.AddToGroupAsync(Context.ConnectionId, userId);

                    await Clients.All.SendAsync("UserStatusChanged", userId.ToString(), true);

                }
            }
            

            await base.OnConnectedAsync();
        }

        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }



        //Called when a client disconnects
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var httpContext = Context.GetHttpContext();
            var userId = httpContext?.Request.Query["userId"];
            if (!string.IsNullOrEmpty(userId) && _userConnections.ContainsKey(userId))
            {
                // Remove the connection ID from the user's connection list
                _userConnections[userId].Remove(Context.ConnectionId);

                // If no connections remain for the user, remove the user from the dictionary
                if (_userConnections[userId].Count == 0)
                {
                    _userConnections.TryRemove(userId, out _);

                    // Update the last connection time for the user
                    _userLastConnectionTime[userId] = DateTime.UtcNow.AddHours(1);
                }

                await Clients.All.SendAsync("UserStatusChanged", userId.ToString(), false);
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);
            }
            await base.OnDisconnectedAsync(exception);
        }
        public DateTime? GetLastConnectionTime(string userId)
        {
            return _userLastConnectionTime.TryGetValue(userId, out var lastConnectionTime)
                ? lastConnectionTime
                : null;
        }

        // Check if a user is connected

        public Dictionary<string, IEnumerable<string>> GetAllConnectedUsers()
        {
            return _userConnections.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.AsEnumerable()
            );
        }
        public async Task SendMessage(string senderId, string message, string receiverId)
        {
            await Clients.All.SendAsync("ReceiveMessage", senderId, message, receiverId);
        }
        public async Task MarkAsSeen(string currentUserId, string senderId)
        {
            await Clients.All.SendAsync("MessageStatusUpdate", currentUserId,senderId);
        }
        public async Task ToogleStatus(string currentUserId)
        {
            await Clients.All.SendAsync("ToglleStatusConnection", currentUserId);
        }
    }


}
