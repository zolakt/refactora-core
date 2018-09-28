using System.Threading.Tasks;

namespace Refactora.Auth.Provider
{
	public interface IAuthProvider
	{
		Task<bool> IsAuthenticatedAsync();
	}

	public interface IAuthProvider<TEntityType> : IAuthProvider
	{
		Task<TEntityType> GetCurrentUserAsync();
	}

	public interface IAuthProvider<TEntityType, TPermissionType> : IAuthProvider<TEntityType>
	{
		Task<bool> HasPermissionAsync(TPermissionType permission);
	}
}
