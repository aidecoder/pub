// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace ids4e
{
    public static class Config
    {
        public static string ServerName = "localhost";
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[]
            {
                new ApiResource("api1", "My API #1", 
                    // эти claims войдут в scope api1
                    new[] {"name", "role" })
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                // client credentials flow client
                new Client
                {
                    ClientId = "client",
                    ClientName = "Client Credentials Client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                    AllowedScopes = { "api1" }
                },

                // MVC client using hybrid flow
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",

                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                    RedirectUris = { $"http://{ServerName}:5001/signin-oidc" },
                    FrontChannelLogoutUri = $"http://{ServerName}:5001/signout-oidc",
                    PostLogoutRedirectUris = { $"http://{ServerName}:5001/signout-callback-oidc" },

                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "api1" }
                },

                // SPA client using implicit flow
                new Client
                {
                    ClientId = "spa",
                    ClientName = "SPA Client",
                    ClientUri = "http://identityserver.io",

                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris =
                    {
                        $"http://{ServerName}:5002/index.html",
                        $"http://{ServerName}:5002/callback.html",
                        $"http://{ServerName}:5002/silent.html",
                        $"http://{ServerName}:5002/popup.html",
                    },

                    PostLogoutRedirectUris = { $"http://{ServerName}:5002/index.html" },
                    AllowedCorsOrigins = { $"http://{ServerName}:5002" },

                    AllowedScopes = { "openid", "profile", "api1" }
                },

                new Client
                {
                    ClientId = "js",
                    ClientName = "JS Client",
                    ClientUri = $"http://{ServerName}:5593",

                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris =
                    {
                        $"http://{ServerName}:5593/index.html",
                        $"http://{ServerName}:5593/callback.html",

//                        $"http://{ServerName}:5593/silent.html",
                        $"http://{ServerName}:5593/callback-silent.html",

                        $"http://{ServerName}:5593/popup.html",
                    },

                    PostLogoutRedirectUris = { $"http://{ServerName}:5593/index.html" },
                    AllowedCorsOrigins = { $"http://{ServerName}:5593" },

                    AllowedScopes = { "openid", "profile", "api1" }
                },

                new Client
                {
                    ClientId = "js2",
                    ClientName = "JS Client 2",
                    ClientUri = $"http://{ServerName}:5594",

                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris =
                    {
                        $"http://{ServerName}:5594/index.html",
                        $"http://{ServerName}:5594/callback.html",

//                        $"http://{ServerName}:5594/silent.html",
                        $"http://{ServerName}:5594/callback-silent.html",

                        $"http://{ServerName}:5594/popup.html",
                    },

                    PostLogoutRedirectUris = { $"http://{ServerName}:5594/index.html" },
                    AllowedCorsOrigins = { $"http://{ServerName}:5594" },

                    AllowedScopes = { "openid", "profile", "api1" }
                }
            };
        }
    }
}