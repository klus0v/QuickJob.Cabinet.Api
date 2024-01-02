using System.ComponentModel.DataAnnotations;

namespace QuickJob.Cabinet.DataModel.API.Requests.Email;

public record ConfirmEmailRequest
{
    [StringLength(maximumLength: 4, MinimumLength = 4), Required]
    public string Code { get; set; }
}