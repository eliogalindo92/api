namespace api.Extensions;

public static class AuthorizationPolicyExtensions
{

    private const string PermissionClaimType = "permission";

    public static IServiceCollection AddCustomAuthorizationPolicies(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            
            options.AddPolicy("RequireReadUsers", policy =>
                policy.RequireClaim(PermissionClaimType, "users.read"));

            options.AddPolicy("RequireWriteUsers", policy =>
                policy.RequireClaim(PermissionClaimType, "users.write"));

            options.AddPolicy("RequireDeleteUsers", policy =>
                            policy.RequireClaim(PermissionClaimType, "users.delete"));
            
            options.AddPolicy("RequireReadRoles", policy =>
                            policy.RequireClaim(PermissionClaimType, "roles.read"));
            
            options.AddPolicy("RequireWriteRoles", policy =>
                            policy.RequireClaim(PermissionClaimType, "roles.write"));
            
            options.AddPolicy("RequireDeleteRoles", policy =>
                            policy.RequireClaim(PermissionClaimType, "roles.delete"));

            options.AddPolicy("CanManageOwnProfile", policy =>
                policy.RequireClaim(PermissionClaimType, "ReadOwnProfile", "EditOwnProfile"));

            options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));

            options.AddPolicy("AdminOrCanDelete", policy => policy.RequireAssertion(context =>
                context.User.IsInRole("Admin") ||
                context.User.HasClaim(claim => claim is { Type: PermissionClaimType, Value: "users.delete" })
            ));
            
        });
        return services;
    }
}