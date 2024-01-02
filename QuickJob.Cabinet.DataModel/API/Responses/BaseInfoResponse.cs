using QuickJob.Cabinet.DataModel.ServerDataModel;

namespace QuickJob.Cabinet.DataModel.API.Responses;

public record BaseInfoResponse
{
    public Guid Id { get; set; }
    public string? Fio { get; set; }
    public string? Phone { get; set; }
    public string Email { get; set; }
    public Telegram Telegram { get; set; }
    public string? Address { get; set; }
    
}