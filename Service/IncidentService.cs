using Microsoft.EntityFrameworkCore;

public class IncidentService : IIncidentService
{
    private readonly AppDbContext _context;

    public IncidentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<string> CreateIncidentAsync(IncidentRequestDto request)
    {
        var account = await _context.Accounts.Include(a => a.Contacts)
            .FirstOrDefaultAsync(a => a.Name == request.AccountName);

        if (account == null)
            throw new KeyNotFoundException("Account not found");

        var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.Email == request.ContactEmail);

        if (contact != null)
        {
            contact.FirstName = request.ContactFirstName;
            contact.LastName = request.ContactLastName;
            if (contact.AccountId != account.Id)
                contact.AccountId = account.Id;
        }
        else
        {
            contact = new Contact
            {
                FirstName = request.ContactFirstName,
                LastName = request.ContactLastName,
                Email = request.ContactEmail,
                AccountId = account.Id
            };
            _context.Contacts.Add(contact);
        }

        var incident = new Incident
        {
            Description = request.IncidentDescription,
            Account = account
        };

        _context.Incidents.Add(incident);
        await _context.SaveChangesAsync();

        return incident.IncidentName;
    }
}

