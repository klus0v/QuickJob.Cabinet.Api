namespace QuickJob.Cabinet.DataModel.API.Requests;

public class SetAdditionalInfoRequest : SetInfoRequest
{
    public string? Institute  { get; set; }
    public string? BirthDate  { get; set; }
    public int? Course  { get; set; }
    public string? Citizenship   { get; set; }
    public List<string>? Preferences { get; set; }
}