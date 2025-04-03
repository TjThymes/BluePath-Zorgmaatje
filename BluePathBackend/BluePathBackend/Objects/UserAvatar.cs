using BluePathBackend.Objects;
using System;

namespace BluePath_Backend.Objects
{
    public class UserAvatar
    {
        public Guid Id { get; set; }

        public int Head { get; set; }
        public int Body { get; set; }
        public int Legs { get; set; }

        public string UserId { get; set; }
    }
}
