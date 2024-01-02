namespace QuickJob.Cabinet.DataModel.Exceptions;

public static class HttpErrors
{
    private const string AWSError = "AWSError";
    private const string UsersApiError = "UsersApiError";
    private const string NotificationsApiError = "NotificationsApiError";
    private const string EmailAlreadySetError = "EmailAlreadySet";
    private const string IncorrectCodeError = "IncorrectCode";
    private const string NotFoundError = "NotFound";
    private const string NoAccessError = "NoAccess";

    public static CustomHttpError EmailAlreadySet => new(EmailAlreadySetError, "Email in request is already set in user");
    public static CustomHttpError IncorrectCode => new(IncorrectCodeError, "Provided code is not correct");
    public static CustomHttpError AWS(object error) => new(AWSError, $"AWSError: {error}");
    public static CustomHttpError Users(object error) => new(UsersApiError, $"Users api error: {error}");
    public static CustomHttpError Notifications(object error) => new(NotificationsApiError, $"Notifications api error: {error}");
    public static CustomHttpError NotFound(object itemKey) => new(NotFoundError, $"Not found item with key: '{itemKey}'");
    public static CustomHttpError NoAccess(object itemKey) => new(NoAccessError, $"No access to item with key: '{itemKey}'");
}
