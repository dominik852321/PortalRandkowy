using System;

namespace PortalRandkowy.API.Model
{
    public class Message
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public User Sender { get; set; }
        
        public int RecipientId { get; set; }
        public User Recipient { get; set; }

        public string Content { get; set; }
        public bool IsRead { get; set; }

        public DateTime? DateRead { get; set; }
        public DateTime? DateSend { get; set; }

        public bool SenderDelete { get; set; }
        public bool RecipienDelete { get; set; }

    }
}