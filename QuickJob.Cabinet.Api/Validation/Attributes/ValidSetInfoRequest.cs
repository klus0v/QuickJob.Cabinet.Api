using System.ComponentModel.DataAnnotations;
using QuickJob.Cabinet.DataModel.API.Requests.Info;
using QuickJob.Cabinet.DataModel.ServerDataModel.Constants;

namespace QuickJob.Cabinet.Api.Validation.Attributes;

public class ValidSetInfoRequest : ValidationAttribute
{
    private readonly HashSet<string> NotAllowedDeletes = new()
    {
        KeycloackConstants.TgId, KeycloackConstants.TgUsername, KeycloackConstants.Phone, KeycloackConstants.Avatar
    };
    
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is SetInfoRequest setInfoRequest)
        {
            if (setInfoRequest.Deletes is not null && setInfoRequest.Deletes.Count > 0)
            {
                if (setInfoRequest.Deletes.Any(x => NotAllowedDeletes.Contains(x)))
                    return new ValidationResult("Deletes contain not allowed values", setInfoRequest.Deletes.Where(x => NotAllowedDeletes.Contains(x)));
            }
            return ValidationResult.Success;
        }

        return new ValidationResult("Incorrect request");
    }
}