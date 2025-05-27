using System.ComponentModel.DataAnnotations;

public class IncidentRequestDto
{
    [Required]
    public string AccountName { get; set; } = null!;

    [Required]
    public string ContactFirstName { get; set; } = null!;

    [Required]
    public string ContactLastName { get; set; } = null!;

    [Required, EmailAddress]
    public string ContactEmail { get; set; } = null!;

    [Required]
    public string IncidentDescription { get; set; } = null!;
}