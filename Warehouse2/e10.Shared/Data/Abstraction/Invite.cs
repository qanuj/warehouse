using System;

namespace e10.Shared.Data.Abstraction
{
    public partial class Invite : Entity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? Completed { get; set; }
        public Member Inviter { get; set; }
        public int InviterId { get; set; }
    }
}