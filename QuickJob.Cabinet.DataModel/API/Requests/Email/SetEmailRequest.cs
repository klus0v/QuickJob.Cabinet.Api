using System.ComponentModel.DataAnnotations;

namespace QuickJob.Cabinet.DataModel.API.Requests.Email;

public record SetEmailRequest
{
    [EmailAddress]
    public string Email { get; set; }
}