using System;

namespace domain.Models
{
    public class Account : Disposable
    {
        public long Id { get; set; }

        public User Owner { get; set; }
    }
}