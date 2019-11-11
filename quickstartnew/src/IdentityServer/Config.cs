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
             /*new IdentityResource[]
             { 
                 new IdentityResources.OpenId()
             };*/
             new List<IdentityResource>
             {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
             };


        public static IEnumerable<ApiResource> Apis =>
            /*new ApiResource[] 
            { };*/  //the original code
            new List<ApiResource>
            {
                new ApiResource("api1", "My API")
            };


        public static IEnumerable<Client> Clients =>
            /*new Client[] 
            { };*/  //the original code
            new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    // secret for authentication
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    // scopes that client has access to   //the scope here is changable
                    AllowedScopes = { "api1" }
                },
                new Client
                {
                    ClientId = "mvc",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,
                    RequireConsent = false,
                    RequirePkce = true,

                    // where to redirect to after login
                    RedirectUris = { "http://localhost:5002/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

                    AllowedScopes = new List<string>   //in theory, i can add the resource here to allow access the resource
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    },
                    AllowOfflineAccess = true
                }
            };

    }
}