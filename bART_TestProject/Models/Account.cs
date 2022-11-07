namespace bART_TestProject.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string? IncidentName { get; set; }
        public Incident? Incident { get; set; }
        public ICollection<Contact> Contacts { get; set; }
    }
}
