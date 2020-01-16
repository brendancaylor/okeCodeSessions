// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };


        public static IEnumerable<ApiResource> Apis =>
            new List<ApiResource>
            {
                new ApiResource("spelling-api", "Spelling API")
            };

        public static IEnumerable<Client> GetClients(string spaSpellingClientBaseUrl)
        {
            var liveWwwUrl = "https://www.spell-it.co.uk";

            var clients = new List<Client>
            {
                new Client
                {
                    ClientId = "spa-spelling-client",
                    ClientName = "Spelling SPA",
                    RequireClientSecret = false,
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,

                    RedirectUris = {
                        $"{spaSpellingClientBaseUrl}/signin-callback",
                        $"{spaSpellingClientBaseUrl}/assets/silent-callback.html",
                        $"{liveWwwUrl}/signin-callback",
                        $"{liveWwwUrl}/assets/silent-callback.html"
                    },
                    
                    PostLogoutRedirectUris = {
                        $"{spaSpellingClientBaseUrl}/signout-callback",
                        $"{liveWwwUrl}/signout-callback"
                    },

                    AllowedCorsOrigins = {
                        spaSpellingClientBaseUrl,
                        liveWwwUrl
                    },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "spelling-api"
                    },

                    AccessTokenLifetime = 600

                }
            };

            return clients;
        }
    }
}