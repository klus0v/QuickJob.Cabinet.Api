using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using QuickJob.Cabinet.BusinessLogic.Extensions;
using QuickJob.Cabinet.DataModel.Context;

namespace QuickJob.Cabinet.Api.Middlewares;

internal sealed class UserAuthMiddleware :  IMiddleware
{ 
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        CommonContext.Initialize();
        RequestContext.Initialize();

        var userId = AuthenticateUser(context);
        if (userId == null)
        {
            RequestContext.ClientInfo.IsUserAuthenticated = false;
            await next.Invoke(context);
            return;
        }
        
        RequestContext.ClientInfo.IsUserAuthenticated = true;
        RequestContext.ClientInfo.UserId = userId.Value;
        await next.Invoke(context);
    }
    
    private Guid? AuthenticateUser(HttpContext context) => 
        Guid.TryParse(context.User.GetId(), out var id) ? id : null;
}

