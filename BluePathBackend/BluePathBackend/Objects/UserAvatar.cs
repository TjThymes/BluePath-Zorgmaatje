using BluePathBackend.Objects;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
        [BindNever]
        public string UserId { get; set; }
    }
}
