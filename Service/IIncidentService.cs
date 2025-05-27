public interface IIncidentService
{
    Task<string> CreateIncidentAsync(IncidentRequestDto request);
}
