using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Refactora.Auth.Common;
using Refactora.Auth.Provider;

namespace Refactora.Auth
{
	public static class AuthExtensions
	{
		public static IMvcCoreBuilder AddAuthModule(this IMvcCoreBuilder builder,
			string host,
			string clientHost,
			string clientAudience,
			string scheme = JwtBearerDefaults.AuthenticationScheme)
		{
			builder.Services
				   .AddAuthorization()
				   .AddAuthentication(options =>
				   {
					   options.DefaultAuthenticateScheme = scheme;
					   options.DefaultChallengeScheme = scheme;
				   })
				   .AddJwtBearer(options =>
				   {
					   options.Authority = $"https://{host}/";
					   options.Audience = clientAudience;
				   });

			builder.Services
				.AddCors(options =>
					options.AddPolicy("default", policy =>
						policy.WithOrigins(clientHost)
							.AllowAnyHeader()
							.AllowAnyMethod()
				));

			builder.Services
				.AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
				.AddTransient<IContactDetails, ContactDetails>()
				.AddScoped<IAuthProvider, IdentityAuthProvider>();

			return builder;
		}

		public static IMvcCoreBuilder AddAuthModule<TAuthProviderInterface, TAuthProviderImplementation>(this IMvcCoreBuilder builder,
			string host,
			string clientHost,
			string clientAudience,
			string scheme = JwtBearerDefaults.AuthenticationScheme)
		{
			AddAuthModule(builder, host, clientHost, clientAudience, scheme);

			builder.Services.AddScoped(typeof(TAuthProviderInterface), typeof(TAuthProviderImplementation));

			return builder;
		}
	}
}
