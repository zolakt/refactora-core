using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Refactora.Auth.Common;
using Refactora.Auth.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Refactora.Auth.Vendor.Auth0
{
	public class Auth0UserManager : IExternalUserManager
	{
		private readonly IManagementApiClient _client;

		public Auth0UserManager(IAuthClient auth)
		{
			if (auth == null)
			{
				throw new ArgumentNullException("client");
			}

			_client = new ManagementApiClient(
				auth.GetToken().Result,
				new Uri(string.Format("https://{0}/api/v2/", auth.Host))
			);
		}


		public async Task<bool> CreateUser(string email, IContactDetails info = null, string password = null, bool verifyEmail = true)
		{
			var existing = await GetUsersByEmail(email);

			if (existing.Any())
			{
				return false;
			}

			var phone = (!string.IsNullOrEmpty(info?.Phone) && Regex.Match(info.Phone, @"^(\+[0-9]{1,15})$").Success) ? info.Phone : null;

			var user = await _client.Users.CreateAsync(new UserCreateRequest
			{
				Email = email,
				Password = password ?? email,
				FirstName = info?.FirstName,
				LastName = info?.LastName,
				PhoneNumber = phone,
				VerifyEmail = verifyEmail,
				Connection = "Username-Password-Authentication"
			});

			return (user != null);
		}

		public async Task<bool> UpdateUser(string email, IContactDetails info = null, string password = null)
		{
			var existing = await GetUsersByEmail(email);

			if (!existing.Any())
			{
				return false;
			}

			foreach (var user in existing)
			{
				await _client.Users.UpdateAsync(user.UserId, new UserUpdateRequest
				{
					Email = email,
					Password = password
				});
			}

			return true;
		}

		public async Task<bool> DeleteUser(string email)
		{
			var existing = await GetUsersByEmail(email);

			if (!existing.Any())
			{
				return false;
			}

			foreach (var user in existing)
			{
				await _client.Users.DeleteAsync(user.UserId);
			}

			return true;
		}


		public async Task<string> GetChangePasswordUrl(string email, string redirectUrl = null)
		{
			var users = await GetUsersByEmail(email);
			var user = users.FirstOrDefault();

			if (user == null)
			{
				return null;
			}

			var identity = user.Identities.FirstOrDefault();
			if (identity == null || (identity.IsSocial == true))
			{
				return null;
			}

			var result = await _client.Tickets.CreatePasswordChangeTicketAsync(new PasswordChangeTicketRequest
			{
				UserId = user.UserId,
				ResultUrl = redirectUrl
			});

			return result.Value;
		}

		public async Task<string> GetEmailVerifyUrl(string email, string redirectUrl = null)
		{
			var users = await GetUsersByEmail(email);
			var user = users.FirstOrDefault();

			if (user == null)
			{
				return null;
			}

			var identity = user.Identities.FirstOrDefault();
			if (identity == null || (identity.IsSocial == true))
			{
				return null;
			}

			var result = await _client.Tickets.CreateEmailVerificationTicketAsync(new EmailVerificationTicketRequest
			{
				UserId = user.UserId,
				ResultUrl = redirectUrl
			});

			return result.Value;
		}


		private async Task<IEnumerable<User>> GetUsersByEmail(string email)
		{
			var result = await _client.Users.GetAllAsync(new GetUsersRequest
			{
				Query = email
			});

			return result;
		}
	}
}
