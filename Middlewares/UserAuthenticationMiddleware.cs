using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todo_api.Data;
using todo_api.Dtos.Todo;
using todo_api.Interfaces;
using todo_api.Mappers;
using todo_api.Models;

public class UserAuthenticationMiddleware

{
    private readonly RequestDelegate _next;
    private readonly IServiceProvider _serviceProvider;

    public UserAuthenticationMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
    {
        _next = next;
        _serviceProvider = serviceProvider;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var endpoint = context.GetEndpoint();
        var requiresAuth = endpoint?.Metadata.GetMetadata<RequiresUserAuthenticationAttribute>() != null;

        if (requiresAuth && context.User.Identity.IsAuthenticated)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var username = context.User.GetUsername();
                var user = await userManager.FindByNameAsync(username);

                if (user != null)
                {
                    context.Items["CurrentUser"] = user;
                }
            }
        }

        await _next(context);
    }
}
