using BluePathBackend.Objects;
using System;
using System.Text.Json.Serialization;

namespace BluePath_Backend.Objects
{
    public class UserAvatar
    {
        public Guid Id { get; set; }

        public int Head { get; set; }
        public int Body { get; set; }
        public int Legs { get; set; }

        [JsonIgnore]
        public string UserId { get; set; }
    }
}
