using System.Threading.Tasks;

namespace Refactora.Auth.Management
{
	public interface IAuthClient
	{
		Task<string> GetToken();

		string Host { get; }
	}
}
