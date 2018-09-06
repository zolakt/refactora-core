namespace Refactora.Auth.Provider
{
	public interface IAuthProvider
	{
		bool IsAuthenticated { get; }
	}

	public interface IAuthProvider<TEntityType> : IAuthProvider
	{
		TEntityType CurrentUser { get; }
	}

	public interface IAuthProvider<TEntityType, TPermissionType> : IAuthProvider<TEntityType>
	{
		bool HasPermission(TPermissionType permission);
	}
}
