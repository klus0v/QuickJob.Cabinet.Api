namespace QuickJob.Cabinet.DataModel.API.Requests;

public class SetBaseInfoRequest : SetInfoRequest
{
    public string? Fio { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
}