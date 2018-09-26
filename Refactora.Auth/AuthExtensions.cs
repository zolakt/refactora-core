using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Refactora.Auth.Common;
using Refactora.Auth.Provider;

namespace Refactora.Auth
{
	public static class AuthExtensions
	{
		public static IMvcCoreBuilder AddDefaultAuth(this IMvcCoreBuilder builder,
			string host,
			string clientHost,
			string clientAudience,
			string scheme = JwtBearerDefaults.AuthenticationScheme,
			bool autmapDefaults = true)
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
				.AddTransient<IContactDetails, ContactDetails>();

			if (autmapDefaults) { 
				builder.Services.AddScoped<IAuthProvider, IdentityAuthProvider>();
			}

			return builder;
		}

		public static IMvcCoreBuilder AddDefaultAuth<TEntityType>(this IMvcCoreBuilder builder,
			string host,
			string clientHost,
			string clientAudience,
			string scheme = JwtBearerDefaults.AuthenticationScheme)
		{
			AddDefaultAuth(builder, host, clientHost, clientAudience, scheme);

			builder.Services.AddScoped<IAuthProvider<TEntityType>, IdentityAuthProvider<TEntityType>>();

			return builder;
		}

		public static IMvcCoreBuilder AddDefaultAuth<TEntityType, TPermissionType>(this IMvcCoreBuilder builder,
			string host,
			string clientHost,
			string clientAudience,
			string scheme = JwtBearerDefaults.AuthenticationScheme)
		{
			AddDefaultAuth<TEntityType>(builder, host, clientHost, clientAudience,scheme);

			builder.Services.AddScoped<IAuthProvider<TEntityType, TPermissionType>, IdentityAuthProvider<TEntityType, TPermissionType>>();

			return builder;
		}


		public static IMvcCoreBuilder AddCustomAuth<TAuthProviderImplementation>(this IMvcCoreBuilder builder,
			string host,
			string clientHost,
			string clientAudience,
			string scheme = JwtBearerDefaults.AuthenticationScheme)
			where TAuthProviderImplementation : IAuthProvider
		{
			AddDefaultAuth(builder, host, clientHost, clientAudience, scheme, false);

			builder.Services.AddScoped(typeof(IAuthProvider), typeof(TAuthProviderImplementation));

			return builder;
		}

		public static IMvcCoreBuilder AddCustomAuth<TAuthProviderImplementation, IAuthProviderInterface>(this IMvcCoreBuilder builder,
			string host,
			string clientHost,
			string clientAudience,
			string scheme = JwtBearerDefaults.AuthenticationScheme)
			where TAuthProviderImplementation : IAuthProvider
		{
			AddCustomAuth<TAuthProviderImplementation>(builder, host, clientHost, clientAudience, scheme);

			builder.Services.AddScoped(typeof(IAuthProviderInterface), typeof(TAuthProviderImplementation));

			return builder;
		}
	}
}
