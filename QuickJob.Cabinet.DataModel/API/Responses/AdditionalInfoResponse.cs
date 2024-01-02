namespace QuickJob.Cabinet.DataModel.API.Responses;

public record AdditionalInfoResponse
{
    public Guid Id { get; set; }
    public string? Institute  { get; set; }
    public string? BirthDate  { get; set; }
    public int? Course  { get; set; }
    public string? Citizenship   { get; set; }
    public List<string>? Preferences   { get; set; }
    
}