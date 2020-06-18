using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace GraphQLTryOuts.Identity.Data
{
    public class Config
    {
        private const string UsersApi = "GraphQL.Users";
        private const string MeetingsApi = "GraphQL.Meetings";
        private const string MessagingApi = "GraphQL.Messaging";

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource(UsersApi, "Users API", new List<string>() { JwtClaimTypes.Name }),
                new ApiResource(MeetingsApi, "Meetings API"),
                new ApiResource(MessagingApi, "Messaging API")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                // OpenID Connect implicit flow client (MVC)
                new Client
                {
                    ClientId = "GraphQLTryOuts.WebClient",
                    ClientName = "GraphQL Web Client",
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,

                    // where to redirect to after login
                    RedirectUris = { "http://localhost:5004/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "http://localhost:5004/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        UsersApi,
                        MeetingsApi
                    },
                    AllowOfflineAccess = true,
                    RequireConsent = false,
                    RequireClientSecret = false,

                    ClientSecrets =
                    {
                        // TODO: Create proper secret
                        new Secret("secret".Sha256())
                    }
                },

                new Client
                {
                    ClientId = "GraphQL.CrossPlatform",
                    ClientName = "GraphQL Mobile client",
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,

                    ClientSecrets =
                    {
                        // TODO: Create proper secret
                        new Secret("secret".Sha256())
                    },

                    RedirectUris = { "graphql://callback" },

                    PostLogoutRedirectUris = { "graphql://callback" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        UsersApi,
                        MeetingsApi,
                        MessagingApi
                    },

                    RequireConsent = false,
                    AllowOfflineAccess = true,

                    RequirePkce = true,

                    RefreshTokenUsage = TokenUsage.ReUse
                }
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "818727",
                    Username = "alice",
                    Password = "alice",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Alice Smith"),
                        new Claim(JwtClaimTypes.GivenName, "Alice"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                    }
                },
                new TestUser
                {
                    SubjectId = "88421113",
                    Username = "bob",
                    Password = "bob",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Bob Smith"),
                        new Claim(JwtClaimTypes.GivenName, "Bob"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.Email, "BobSmith@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json),
                        new Claim("location", "somewhere"),
                    }
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }
    }
}
