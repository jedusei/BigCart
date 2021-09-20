using System;

namespace BigCart.Models
{
    public record Review
    {
        public User User { get; init; }
        public float Rating { get; init; }
        public string Comment { get; init; }
        public DateTime DateCreated { get; init; } = DateTime.Now;
    }
}
