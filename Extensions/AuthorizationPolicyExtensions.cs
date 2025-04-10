namespace api.Extensions;

public static class AuthorizationPolicyExtensions
{

    private const string PermissionClaimType = "permission";

    public static IServiceCollection AddCustomAuthorizationPolicies(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            
            options.AddPolicy("RequireReadUsers", policy =>
                policy.RequireClaim(PermissionClaimType, "ReadUsers"));

            options.AddPolicy("RequireCreateUsers", policy =>
                policy.RequireClaim(PermissionClaimType, "CreateUsers"));

            options.AddPolicy("CanManageOwnProfile", policy =>
                policy.RequireClaim(PermissionClaimType, "ReadOwnProfile", "EditOwnProfile"));

            options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));

            options.AddPolicy("AdminOrCanDelete", policy => policy.RequireAssertion(context =>
                context.User.IsInRole("Admin") ||
                context.User.HasClaim(claim => claim is { Type: PermissionClaimType, Value: "DeleteUsers" })
            ));
            
        });
        return services;
    }
}