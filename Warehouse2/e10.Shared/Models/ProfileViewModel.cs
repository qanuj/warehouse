using System;

namespace e10.Shared.Models
{
    public class ProfileEditViewModel
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string About { get; set; }
        public string PictureUrl { get; set; }
        public string Address { get; set; }
        public string AlternateNumber { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public string PinCode { get; set; }
    }

    public class ProfileViewModel : ProfileEditViewModel
    {
        public string Hash { get; set; }
        public DateTime Created { get; set; }
        public bool IsGuest { get; set; }
    }
}