namespace QuickJob.Cabinet.DataModel.Exceptions;

public static class HttpErrors
{
    private const string AWSError = "AWSError";
    private const string UsersApiError = "UsersApiError";
    private const string NotFoundError = "NotFound";
    private const string NoAccessError = "NoAccess";

    public static CustomHttpError AWS(object error) => new(AWSError, $"AWSError: {error}");
    public static CustomHttpError Users(object error) => new(UsersApiError, $"Users api error: {error}");
    public static CustomHttpError NotFound(object itemKey) => new(NotFoundError, $"Not found item with key: '{itemKey}'");
    public static CustomHttpError NoAccess(object itemKey) => new(NoAccessError, $"No access to item with key: '{itemKey}'");
}
