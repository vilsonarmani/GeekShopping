using Duende.IdentityServer.Models;

namespace GeekShopping.IdentityServer.Configuration;

public static class IdentityConfiguration
{
    public const string Admin = "admin";
    public const string Customer = "Customer";

    //add Identity Resources
    //resources protected by the server

    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile()
        };

    //define API SCOPE (Resources client access)
    // IdentityScope
    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>()
        {
            new ApiScope("geek_Shopping", "GeekShopping Server."),
            new ApiScope("read", "Read data."),
            new ApiScope("write", "Write data."),
            new ApiScope("delete", "Delete data."),

        };

    /// Must Create a client
    /// is a software component that requests a token from an identity server 
    /// So it can identify the user, allow or deny access to a resource
    /// an example of a client is an application. In this case GeekShoppingAPI
    /// 
    public static IEnumerable<Client> Clients =>
        new List<Client>()
        {
            new Client{
                ClientId = "client",
                ///secret can be called in the AppSettings. Use a complex secret for increase the security
                ClientSecrets = { new Secret("my_Super_Secret".Sha256()) },
                ///credentials
                AllowedGrantTypes = GrantTypes.ClientCredentials, //need usercredentials to access
                AllowedScopes = { "read", "write", "profile"} /// Can read, write and access own profile
            }
        };
}

