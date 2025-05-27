public class Incident
{
    public string IncidentName { get; set; } = Guid.NewGuid().ToString(); // PK
    public string Description { get; set; } = null!;
    public int AccountId { get; set; }
    public Account Account { get; set; } = null!;
}