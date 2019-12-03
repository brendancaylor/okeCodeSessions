using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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

            var identityId = context.User.Claims.FirstOrDefault(o => o.Type.ToLower() == "sub")?.Value;

            if(identityId == null)
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


            var claims = GetClaims(dbContext, identityId);
            foreach (var claim in claims)
            {
                claimIdentity.AddClaim(claim);
            }
            await _next.Invoke(context);
        }

        List<Claim> GetClaims(CollegeContext dbContext, string identityId)
        {
            var result = new List<Claim>();
            var user = dbContext
                .AppUsers
                .AsNoTracking()
                    .Include(o => o.Role.RoleClaims)
                        .ThenInclude(rc => rc.Claim)
                .SingleOrDefault(o => o.IdentityId == identityId);

            var claims = user?.Role.RoleClaims.Select(o => o.Claim.ClaimName).ToList();

            foreach (var claim in claims)
            {
                result.Add(new Claim("role", claim));
            }
            result.Add(new Claim("appUserId", user?.Id.ToString()));
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
