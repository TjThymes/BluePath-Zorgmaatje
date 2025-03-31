using BluePathBackend.Objects;
using System;

namespace BluePath_Backend.Objects
{
    public class DiaryEntry
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
