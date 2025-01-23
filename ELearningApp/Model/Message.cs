using ELearningApp.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearningApp.Model
{
    public class Message
    {
        public int Id { get; set; }
        [ForeignKey("ApplicationUser")]
        public string SenderId { get; set; } = string.Empty;
        public ApplicationUser? Sender { get; set; }
        [ForeignKey("ApplicationUser")]
        public string ReceiverId { get; set; } = string.Empty;
        public ApplicationUser? Receiver { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
        public bool Status { get; set; } = false;

    }
}
