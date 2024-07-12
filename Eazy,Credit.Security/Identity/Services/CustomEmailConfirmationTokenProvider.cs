using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;


namespace Eazy.Credit.Security.Identity.Services
{
    public class CustomEmailConfirmationTokenProvider<TUser> : DataProtectorTokenProvider<TUser> where TUser : class
    {
        public CustomEmailConfirmationTokenProvider(IDataProtectionProvider dataProtectionProvider,
            IOptions<EmailConfirmationTokenProviderOptions> options,
            ILogger<DataProtectorTokenProvider<TUser>> logger)
            : base(dataProtectionProvider, options, logger)
        {
        }
    }

    public class EmailConfirmationTokenProviderOptions : DataProtectionTokenProviderOptions
    {
        public EmailConfirmationTokenProviderOptions()
        {
            Name = "EmailConfirmationTokenProvider";
            TokenLifespan = TimeSpan.FromHours(4);
        }
    }

    public static class CustomIdentityBuilderExtensions
    {
        public static IdentityBuilder AddEmailConfirmationTokenProvider(this IdentityBuilder builder)
        {
            var userType = builder.UserType;
            var provider = typeof(CustomEmailConfirmationTokenProvider<>).MakeGenericType(userType);
            return builder.AddTokenProvider("EmailConfirmationTokenProvider", provider);
        }
    }

}
