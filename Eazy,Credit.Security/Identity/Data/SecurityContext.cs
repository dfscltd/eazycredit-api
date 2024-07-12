using Eazy.Credit.Security.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace Eazy.Credit.Security.Identity.Data
{
    public class SecurityContext : IdentityDbContext<AppUsers, AppRoles, string>
    {
        public SecurityContext(DbContextOptions<SecurityContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUsers>(entity =>
            {
                entity.ToTable(name: "Users", "sec")
                //.HasKey(c => c.Id);
                .Property(p => p.Id).HasColumnName("LoginID");
                entity.HasKey(p => p.Id);

                //// Each User can have many UserClaims
                //entity.HasMany(e => e.Claims)
                //    .WithOne()
                //    .HasForeignKey(uc => uc.UserId)
                //    .IsRequired();

                //// Each User can have many UserLogins
                //entity.HasMany(e => e.Logins)
                //    .WithOne()
                //    .HasForeignKey(ul => ul.UserId)
                //    .IsRequired();

                //// Each User can have many UserTokens
                //entity.HasMany(e => e.Tokens)
                //    .WithOne()
                //    .HasForeignKey(ut => ut.UserId)
                //    .IsRequired();

                //// Each User can have many entries in the UserRole join table
                //entity.HasMany(e => e.UserRoles)
                //    .WithOne()
                //    .HasForeignKey(ur => ur.UserId)
                //    .IsRequired();
            });

            builder.Entity<AppRoles>(entity =>
            {
                entity.ToTable(name: "UserRoles", "sec")
                .Property(p => p.Id).HasColumnName("UserRoleID");

                entity.HasKey(c => new { c.Id });

                // entity.HasMany(e => e.UserRoles)
                //.WithOne(e => e.Role)
                //.HasForeignKey(ur => ur.RoleId)
                //.IsRequired();

                // // Each Role can have many associated RoleClaims
                // entity.HasMany(e => e.RoleClaims)
                //     .WithOne(e => e.Role)
                //     .HasForeignKey(rc => rc.RoleId)
                //     .IsRequired();

                // Each Role can have many associated Menu Permission
                //entity.HasMany(e => e.AppRoleMenuPermission)
                //    .WithOne(e => e.Role)
                //    .HasForeignKey(rc => rc.RoleID)
                //    .IsRequired();
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable(name: "AppUserClaim", "sec")
                .Property(p => p.Id).HasColumnName("UserClaimID");
                entity.Property(p => p.UserId).HasColumnName("LoginID");
                entity.HasKey(c => new { c.Id });
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable(name: "AppUserLogin", "sec")
                .HasKey(c => new { c.LoginProvider, c.ProviderKey });
            });

            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable(name: "AppRoleClaim", "sec")
                .Property(p => p.Id).HasColumnName("RoleClaimID");
                entity.HasKey(c => c.Id);
            });

            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable(name: "UserRoleUsers", "sec")
                .Property(p => p.RoleId).HasColumnName("UserRoleID");
                entity.Property(p => p.UserId).HasColumnName("LoginID");
                entity.HasKey(c => new { c.UserId, c.RoleId });
            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable(name: "AppUserToken", "sec")
                .HasKey(c => new { c.UserId, c.LoginProvider, c.Name });
                entity.Property(p => p.UserId).HasColumnName("LoginID");
            });

            //builder.Entity<IdentityUserClaim<string>>(entity =>
            //{
            //    entity.ToTable(name: "AppUserClaim", "sec")
            //    .HasKey(c => c.Id);
            //    entity.Property(p => p.UserId).HasColumnName("LoginID");
            //});

            //builder.Entity<IdentityUserToken<string>>(entity =>
            //{
            //    entity.ToTable(name: "AppUserToken", "sec")
            //    .HasKey(c => c.LoginProvider);
            //    entity.Property(p => p.UserId).HasColumnName("LoginID");
            //});

            //builder.Entity<AppNavigationMenu>(entity =>
            //{
            //    entity.ToTable(name: "AppNavigationMenu")
            //    //.HasKey(c => new { c.UserID, c.LoginProvider, c.Name }); 
            //    .HasKey(c => c.MenuID);

            //    entity.HasMany(e => e.AppRoleMenuPermission)
            //    .WithOne(e => e.ParentMenu)
            //    .HasForeignKey(rc => rc.MenuID)
            //    .IsRequired();

            //});

            //builder.Entity<AppRoleMenuPermission>(entity =>
            //{
            //    entity.ToTable(name: "AppRoleMenuPermission")
            //    //.HasKey(c => new { c.UserID, c.LoginProvider, c.Name });
            //    .HasKey(c => c.ID);
            //});


            //builder.Entity<RefreshToken>(entity =>
            //{
            //    entity.ToTable(name: "RefreshToken")
            //    //.HasKey(c => new { c.UserID, c.LoginProvider, c.Name });
            //    .HasKey(c => c.RecordID);
            //});


        }

    }


    public class AppDbContextFactory : IDesignTimeDbContextFactory<SecurityContext>
    {
        public SecurityContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SecurityContext>();
            optionsBuilder.UseSqlServer("Server=DLINKS_RD\\SQL2019;Database=EazyCreditWorkFlow;Trusted_Connection=false;MultipleActiveResultSets=true;TrustServerCertificate=True;Encrypt=True;User ID=dlinks;Password=abc@123");

            return new SecurityContext(optionsBuilder.Options);
        }
    }

}
