using BluePathBackend.Objects;
using System;
using System.Text.Json.Serialization;

namespace BluePath_Backend.Objects
{
    public class DiaryEntry
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }
        public int gevoel { get; set; } // 1-3

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [JsonIgnore]

        public string ?UserId { get; set; }
    }
}
