using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class CollegeContextSeed
    {
        public static async Task SeedAsync(CollegeContext collegeContext,
            ILoggerFactory loggerFactory, int? retry = 0)
        {
            int retryForAvailability = retry.Value;
            try
            {
                collegeContext.Database.Migrate();

                if (!collegeContext.Claims.Any())
                {
                    collegeContext.Claims.AddRange(
                        GetPreconfiguredClaims());
                    await collegeContext.SaveChangesAsync();
                }

                if (!collegeContext.Roles.Any())
                {
                    collegeContext.Roles.AddRange(
                        GetPreconfiguredRoles());
                    await collegeContext.SaveChangesAsync();
                }

                if (!collegeContext.RoleClaims.Any())
                {
                    collegeContext.RoleClaims.AddRange(
                        GetPreconfiguredRoleClaims(collegeContext));
                    await collegeContext.SaveChangesAsync();
                }

                if (!collegeContext.AppUsers.Any())
                {
                    collegeContext.AppUsers.AddRange(
                        GetPreconfiguredAppUsers(collegeContext));
                    await collegeContext.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<CollegeContextSeed>();
                    log.LogError(ex.Message);
                    await SeedAsync(collegeContext, loggerFactory, retryForAvailability);
                }
            }
        }

        static IEnumerable<Claim> GetPreconfiguredClaims()
        {
            return new List<Claim>()
            {
                new Claim() { ClaimName = Constaints.ClaimAdminisiterClaims, DisplayName = "Adminisiter Claims"},
                new Claim() { ClaimName = Constaints.ClaimAdminisiterRoles, DisplayName = "Adminisiter Roles"},
                new Claim() { ClaimName = Constaints.ClaimAdminisiterAllUsers, DisplayName = "Adminisiter All Users"},
                new Claim() { ClaimName = Constaints.ClaimAdminisiterColleges, DisplayName = "Adminisiter Colleges"},
                new Claim() { ClaimName = Constaints.ClaimAdminisiterCollegeUsers, DisplayName = "Adminisiter College Users"},
                new Claim() { ClaimName = Constaints.ClaimAdminisiterHomework, DisplayName = "Adminisiter Homework"}
            };
        }

        static IEnumerable<Role> GetPreconfiguredRoles()
        {
            return new List<Role>()
            {
                new Role() { RoleName = Constaints.RoleFullAdmin, DisplayName = "Full Administration"},
                new Role() { RoleName = Constaints.RoleCollegeAdmin, DisplayName = "College Administration"},
                new Role() { RoleName = Constaints.RoleTeacher, DisplayName = "Teacher"}
            };
        }

        static IEnumerable<RoleClaim> GetPreconfiguredRoleClaims(CollegeContext collegeContext)
        {
            var result = new List<RoleClaim>();

            var roles = collegeContext.Roles.ToList();
            var claims = collegeContext.Claims.ToList();

            var listCliamsForRoleFullAdmin = new List<string>
            {
                Constaints.ClaimAdminisiterAllUsers,
                Constaints.ClaimAdminisiterClaims,
                Constaints.ClaimAdminisiterColleges,
                Constaints.ClaimAdminisiterCollegeUsers,
                Constaints.ClaimAdminisiterHomework,
                Constaints.ClaimAdminisiterRoles
            };

            var listCliamsForRoleCollegeAdmin = new List<string>
            {
                Constaints.ClaimAdminisiterCollegeUsers,
                Constaints.ClaimAdminisiterHomework
            };

            var listCliamsForRoleTeacher = new List<string>
            {
                Constaints.ClaimAdminisiterHomework
            };

            foreach (var role in roles)
            {
                if (role.RoleName == Constaints.RoleFullAdmin)
                {
                    foreach (var claimName in listCliamsForRoleFullAdmin)
                    {
                        var roleClaim = new RoleClaim
                        {
                            Claim = claims.Single(o => o.ClaimName == claimName),
                            Role = role
                        };
                        result.Add(roleClaim);
                    }
                }

                if (role.RoleName == Constaints.RoleCollegeAdmin)
                {
                    foreach (var claimName in listCliamsForRoleCollegeAdmin)
                    {
                        var roleClaim = new RoleClaim
                        {
                            Claim = claims.Single(o => o.ClaimName == claimName),
                            Role = role
                        };
                        result.Add(roleClaim);
                    }
                }

                if (role.RoleName == Constaints.RoleTeacher)
                {
                    foreach (var claimName in listCliamsForRoleTeacher)
                    {
                        var roleClaim = new RoleClaim
                        {
                            Claim = claims.Single(o => o.ClaimName == claimName),
                            Role = role
                        };
                        result.Add(roleClaim);
                    }
                }
            }
            return result;
        }

        static IEnumerable<AppUser> GetPreconfiguredAppUsers(CollegeContext collegeContext)
        {
            var roleFullAdmin = collegeContext.Roles.Single(o => o.RoleName == Constaints.RoleFullAdmin);
            return new List<AppUser>()
            {
                new AppUser() { Email = "brendan.caylor@gmail.com", FirstName = "Brendan", IdentityId = "54EDA8D1-521E-4DA1-8ADD-ECA5D1E07D22".ToLower(), LastName = "Caylor", Role = roleFullAdmin}
            };
        }
    }
}
