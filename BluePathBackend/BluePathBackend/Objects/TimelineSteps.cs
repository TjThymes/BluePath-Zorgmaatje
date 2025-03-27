namespace BluePath_Backend.Objects
{
    public class TimelineSteps
    {
        public int ID { get; set; }
        public string TypeOfProcess { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int TimelineID { get; set; }

    }
}
