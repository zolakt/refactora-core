using Microsoft.Extensions.DependencyInjection;
using Refactora.Auth.Management;

namespace Refactora.Auth.Vendor.Auth0
{
	public static class Auth0Extensions
	{
		public static IMvcCoreBuilder AddAuth0Module(this IMvcCoreBuilder builder,
			string host,
			string clientId,
			string clientSecret,
			string clientAudience)
		{
			builder.Services
				.AddScoped<IAuthClient>(x => new Auth0Client(host, clientId, clientSecret, clientAudience))
				.AddScoped<IExternalUserManager, Auth0UserManager>();

			return builder;
		}
	}
}
