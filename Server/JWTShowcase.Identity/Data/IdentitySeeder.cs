namespace JWTShowcase.Identity.Data;

using JWTShowcase.Services;
using Microsoft.AspNetCore.Identity;
using Models;

public class IdentitySeeder : IDataSeeder
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public IdentitySeeder(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public void SeedData()
    {
        if (_roleManager.Roles.Any())
            return;

        Task
            .Run(async () =>
            {
                var adminRole = new IdentityRole(Constants.AdministratorRoleName);

                await _roleManager.CreateAsync(adminRole);

                var adminUser = new User
                {
                    UserName = "adminTester@jwtshowcase.com",
                    Email = "adminTester@jwtshowcase.com",
                    SecurityStamp = "RandomSecurityStamp"
                };

                await _userManager.CreateAsync(adminUser, "adminTester123");

                await _userManager.AddToRoleAsync(adminUser, Constants.AdministratorRoleName);
            })
            .GetAwaiter()
            .GetResult();
    }
}