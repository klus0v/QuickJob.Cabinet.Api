using Microsoft.AspNetCore.Http;
using QuickJob.Cabinet.BusinessLogic.Extensions;
using QuickJob.Cabinet.BusinessLogic.Mappers;
using QuickJob.Cabinet.BusinessLogic.Services.S3;
using QuickJob.Cabinet.BusinessLogic.Services.Users;
using QuickJob.Cabinet.DataModel.Context;

namespace QuickJob.Cabinet.BusinessLogic.Managers.Avatars;

public sealed class AvatarsManager : IAvatarsManager
{
    private readonly IS3Storage s3Storage;
    private readonly IUsersService usersService;

    public AvatarsManager(IS3Storage s3Storage, IUsersService usersService)
    {
        this.s3Storage = s3Storage;
        this.usersService = usersService;
    }

    public async Task<string> SetAvatar(IFormFile content)
    {
        var userId = RequestContext.ClientInfo.UserId;
        var user = await usersService.GetById(userId);
        var oldAvatarUrl = user.GetAvatar();

        var newAvatarUrl = await s3Storage.UploadFile(content);
        user.AddOrUpdateAvatar(newAvatarUrl);
        await usersService.PatchById(userId, user);

        if (oldAvatarUrl is not null)
            await s3Storage.DeleteFile(oldAvatarUrl);
        return newAvatarUrl;
    }

    public async Task DeleteAvatar()
    {
        var userId = RequestContext.ClientInfo.UserId;
        var user = await usersService.GetById(userId);
        var avatarUrl = user.GetAvatar();

        user.DeleteAvatar();
        await usersService.PatchById(userId, user);

        if (avatarUrl is not null)
            await s3Storage.DeleteFile(avatarUrl);
    }
}