using ooohCar.Application.Models.Identity;
using System;

namespace ooohCar.Application.Models.Chat
{
    public partial class ChatHistory
    {
        public long Id { get; set; }
        public string FromUserId { get; set; }
        public string ToUserId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual CarUser FromUser { get; set; }
        public virtual CarUser ToUser { get; set; }
    }
}