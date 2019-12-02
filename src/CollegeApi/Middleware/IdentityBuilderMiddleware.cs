using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace College.Api.Middleware
{
    public class IdentityBuilderMiddleware
    {
        readonly RequestDelegate _next;

        public IdentityBuilderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, CollegeContext dbContext)
        {
            if (context.User is null || !context.User.Identity.IsAuthenticated)
            {
                await _next.Invoke(context);
                return;
            }

            //var userLockOutDate = await dbContext.
            //    .AsNoTracking()
            //    .Where(u => u.Id == userContext.UserId)
            //    .Select(u => u.LockOutDate)
            //    .SingleAsync();

            //var userIsLockedOut = userLockOutDate is DateTimeOffset lockOutDate &&
            //                      lockOutDate < DateTimeOffset.UtcNow;

            //if (userIsLockedOut)
            //{
            //    context.Response.StatusCode = StatusCodes.Status403Forbidden;
            //    return;
            //}


            var claimIdentity = context.User.Identity as ClaimsIdentity;

            if (claimIdentity.Claims.Any(o => o.Type == ClaimTypes.Role))
            {
                // roles have already been set so we can ignore below
                await _next.Invoke(context);
                return;
            }


            var claims = GetClaims(dbContext);
            foreach (var claim in claims)
            {
                claimIdentity.AddClaim(claim);
            }
            await _next.Invoke(context);
        }

        List<Claim> GetClaims(CollegeContext dbContext)
        {
            //var claims = dbContext.AppUsers
            //return dbContext.RoleClaims
            //    .AsNoTracking()
            //    .Where(roleClaim => roleClaim.Role.Id == roleId)
            //    .Select(roleClaim => GetClaim(roleClaim.Claim))
            //    .ToListAsync();

            var result = new List<Claim>
            {
                new Claim("role", "Admin")
            };
            return result;
        }
    }

    public static class IdentityBuilderMiddlewareExtensions
    {
        public static IApplicationBuilder UseIdentityBuilder(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<IdentityBuilderMiddleware>();
        }
    }
}
