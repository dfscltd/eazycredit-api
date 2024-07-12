
using Eazy.Credit.Security.Entities;
using Eazy.Credit.Security.Identity.Data;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;


namespace Eazy.Credit.Security.Identity.Services
{
    public class CustomPasswordResetTokenProvider<TUser> : IUserTwoFactorTokenProvider<TUser>
       where TUser : AppUsers
    {
        private readonly SecurityContext _db;
        public DataProtectionTokenProviderOptions Options { get; }
        public IDataProtector Protector { get; }
        public string Name => Options.Name;

        public CustomPasswordResetTokenProvider(
            IDataProtectionProvider dataProtectionProvider,
            IOptions<CustomPasswordResetTokenProviderOptions> options,
            SecurityContext db)
        {
            if (dataProtectionProvider == null)
                throw new ArgumentNullException(nameof(dataProtectionProvider));

            Options = options?.Value ?? new DataProtectionTokenProviderOptions();
            Protector = dataProtectionProvider.CreateProtector(Name ?? "DataProtectorTokenProvider");
            _db = db;
        }

        public virtual async Task<string> GenerateAsync(string purpose, UserManager<TUser> userManager, TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var ms = new MemoryStream();
            var userId = await userManager.GetUserIdAsync(user);
            //using (var writer = new  ms.CreateWriter())
            //{
            //    writer.Write(DateTimeOffset.UtcNow);
            //    writer.Write(userId);
            //    writer.Write(purpose ?? "");
            //    string stamp = null;
            //    if (userManager.SupportsUserSecurityStamp)
            //    {
            //        stamp = await userManager.GetSecurityStampAsync(user);
            //    }
            //    writer.Write(stamp ?? "");
            //}
            var tokenProperties = new TokenProperties
            {
                DateTimeOffset = DateTimeOffset.UtcNow,
                UserId = userId,
                Purpose = string.IsNullOrEmpty(purpose) ? "" : purpose,
                Stamps = userManager.SupportsUserSecurityStamp ? await userManager.GetSecurityStampAsync(user) : null,
            };
            using (var writer = new BinaryWriter(ms))
            {
                writer.Write(tokenProperties.DateTimeOffset.ToString("o")); //convert date to a full time string with milliseconds
                writer.Write(tokenProperties.UserId);
                writer.Write(tokenProperties.Purpose);
                writer.Write(tokenProperties.Stamps);
            }

            var protectedBytes = Protector.Protect(ms.ToArray());
            return Convert.ToBase64String(protectedBytes);
        }

        public virtual async Task<bool> ValidateAsync(string purpose, string encodedToken, UserManager<TUser> userManager, TUser user)
        {
            try
            {
                
                var unprotectedData = Protector.Unprotect(Convert.FromBase64String(encodedToken));
                var ms = new MemoryStream(unprotectedData);
                using (var reader = new BinaryReader(ms))
                {
                    var tokenProperties = new TokenProperties
                    {
                        DateTimeOffset = DateTimeOffset.Parse(reader.ReadString()),
                        UserId = reader.ReadString(),
                        Purpose = reader.ReadString(),
                        Stamps = reader.ReadString()
                    };

                    var creationTime = tokenProperties.DateTimeOffset;
                    var expirationTime = creationTime + Options.TokenLifespan;
                    if (expirationTime < DateTimeOffset.UtcNow)
                    {
                        return false;
                    }

                    var userId = tokenProperties.UserId;
                    var actualUserId = await userManager.GetUserIdAsync(user);
                    if (userId != actualUserId)
                    {
                        return false;
                    }
                    var purp = tokenProperties.Purpose;
                    if (!string.Equals(purp, purpose))
                    {
                        return false;
                    }
                    var stamp = tokenProperties.Stamps;
                    if (reader.PeekChar() != -1)
                    {
                        return false;
                    }

                    if (userManager.SupportsUserSecurityStamp)
                    {
                        return stamp == await userManager.GetSecurityStampAsync(user);
                    }
                    return stamp == "";
                }

                //using (var reader = ms.CreateReader())
                //{
                //    var creationTime = reader.ReadDateTimeOffset();
                //    var expirationTime = creationTime + Options.TokenLifespan;
                //    if (expirationTime < DateTimeOffset.UtcNow)
                //    {
                //        return false;
                //    }

                //    var userId = reader.ReadString();
                //    var actualUserId = await userManager.GetUserIdAsync(user);
                //    if (userId != actualUserId)
                //    {
                //        return false;
                //    }
                //    var purp = reader.ReadString();
                //    if (!string.Equals(purp, purpose))
                //    {
                //        return false;
                //    }
                //    var stamp = reader.ReadString();
                //    if (reader.PeekChar() != -1)
                //    {
                //        return false;
                //    }

                //    if (userManager.SupportsUserSecurityStamp)
                //    {
                //        return stamp == await userManager.GetSecurityStampAsync(user);
                //    }
                //    return stamp == "";
                //}
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch
            {
                // Do not leak exception
            }
            return false;
        }

        public virtual async Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<TUser> manager, TUser user)
        {
            var key = await manager.GetAuthenticatorKeyAsync(user);
            return !string.IsNullOrWhiteSpace(key);
        }
    }

    public class TokenProperties
    {
        public DateTimeOffset DateTimeOffset { get; set; }
        public string UserId { get; set; }= string.Empty;
        public string? Purpose { get; set; } = string.Empty;
        public string? Stamps {  get; set; } = string.Empty;
    }
    public class CustomPasswordResetTokenProviderOptions : DataProtectionTokenProviderOptions
    {
        // Add any additional options specific to your custom provider
    }

}
