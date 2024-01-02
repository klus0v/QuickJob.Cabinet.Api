using System.Security.Claims;

namespace QuickJob.Cabinet.BusinessLogic.Extensions;

public static class ClaimsPrincipalExtensions
{
    private const string SubClaim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"; 
    
    public static string? GetId(this ClaimsPrincipal claimsPrincipal) 
        => claimsPrincipal.Claims.FirstOrDefault(x => x.Type == SubClaim)?.Value ?? null;
    
    public static string GetName(this ClaimsPrincipal claimsPrincipal) 
        => claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value ?? string.Empty;
}