using Refactora.Auth.Provider;

namespace Test.Refactora.Auth.Fakes
{
	public interface IFakeAuthProvider : IAuthProvider<FakeUser, object>
	{
	}
}
