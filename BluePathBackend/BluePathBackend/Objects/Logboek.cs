namespace BluePath_Backend.Objects
{
    public class Logboek
    {
        public Guid ID { get; set; }

        public string UserID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } 
        public DateTime Date { get; set; }
        public float Bloedsuikerspiegel { get; set; }
    }
}
