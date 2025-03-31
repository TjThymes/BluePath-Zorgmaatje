using System;

namespace BluePath_Backend.Objects
{
    public class RouteStep
    {
        public Guid Id { get; set; }
        public string RouteType { get; set; } // "A" of "B"
        public int StepOrder { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string IconName { get; set; } // bv. "appointment", "hospital", "recovery"
    }
}
