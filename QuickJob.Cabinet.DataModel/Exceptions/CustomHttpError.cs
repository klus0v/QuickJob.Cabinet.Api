namespace QuickJob.Cabinet.DataModel.Exceptions;

public sealed record CustomHttpError(string? Code, string? Message = null)
{
    
}
