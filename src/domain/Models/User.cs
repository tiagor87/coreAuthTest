using System;
using System.Collections.Generic;

namespace domain.Models
{
    public class User : Model
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<string> Roles { get; } = new List<string>();

        public override bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(this.Login) && !string.IsNullOrWhiteSpace(this.Email);
        }
    }
}
