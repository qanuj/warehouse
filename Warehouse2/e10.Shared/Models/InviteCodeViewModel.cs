namespace e10.Shared.Models
{
    public class InviteCodeViewModel : InviteViewModel
    {
        public InviteCodeViewModel() { }
        public InviteCodeViewModel(InviteViewModel invite)
        {
            this.Name = invite.Name;
            this.Email = invite.Email;
        }
        public string Code { get; set; }
        public int InviterId { get; set; }
    }
}