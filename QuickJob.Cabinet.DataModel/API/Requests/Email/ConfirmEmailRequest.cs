using System.ComponentModel.DataAnnotations;

namespace QuickJob.Cabinet.DataModel.API.Requests.Email;

public record ConfirmEmailRequest
{
    [EmailAddress]
    public string Email { get; set; }
    
    [StringLength(maximumLength: 4, MinimumLength = 4), Required]
    public string Code { get; set; }
}