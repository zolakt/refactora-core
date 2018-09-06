using Refactora.Auth.Common;
using System.Threading.Tasks;

namespace Refactora.Auth.Management
{
	public interface IExternalUserManager
	{
		Task<bool> CreateUser(string email, IContactDetails info = null, string password = null, bool verifyEmail = true);

		Task<bool> UpdateUser(string email, IContactDetails info = null, string password = null);

		Task<bool> DeleteUser(string email);

		Task<string> GetEmailVerifyUrl(string email, string redirectUrl = null);

		Task<string> GetChangePasswordUrl(string email, string redirectUrl = null);
	}
}
