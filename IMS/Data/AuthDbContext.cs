using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IMS.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seed roles (User, SuperAdmin)
            var adminRoleId = "a1d9a7ec-ac90-487b-8076-0b2b115c21d5";
            var userRoleId = "3d0a3bf5-4a69-4151-96f3-72a3ab4d3d1a";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "SuperAdmin",
                    NormalizedName = "Admin",
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "User",
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            // Seed SuperAdminUser
            var superAdminId = "7c57748f-533f-4e00-845f-e8d782b5cac4";

            var superAdminUser = new IdentityUser
            {
                UserName = "SuperAdmin",
                Email = "superadmin@ims.com",
                NormalizedEmail = "superadmin@ims.com".ToUpper(),
                NormalizedUserName = "SuperAdmin".ToUpper(),
                Id = superAdminId

            };
            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
                .HashPassword(superAdminUser, "SuperAdmin@123");


            builder.Entity<IdentityUser>().HasData(superAdminUser);

            // Add all roles to SuperAdminUser
            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId = superAdminId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles); 
        }
    }
}
