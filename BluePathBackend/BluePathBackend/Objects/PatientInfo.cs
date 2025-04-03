using System;

namespace BluePathBackend.Objects
{
    public class PatientInfo
    {
        public Guid Id { get; set; }
        public string ChildName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Route { get; set; } // "A" of "B"
        public string DoctorName { get; set; }
        public DateTime? FirstAppointmentDate { get; set; }

        // optioneel: koppel aan gebruiker
        public string UserId { get; set; }
    }
}
