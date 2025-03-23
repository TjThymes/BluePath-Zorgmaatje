namespace BluePath_Backend.Objects
{
    public class Timeline
    {

        public int ID { get; set; 
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        List<TimelineSteps> TimelineSteps { get; set; }

    }
}
