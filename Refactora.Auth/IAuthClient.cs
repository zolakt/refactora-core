using System.Threading.Tasks;

namespace Refactora.Auth
{
	public interface IAuthClient
	{
		Task<string> GetToken();

		string Host { get; }
	}
}
