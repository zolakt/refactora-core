namespace Refactora.Auth.Common
{
	public interface IContactDetails
	{
		string FirstName { get; }

		string LastName { get; }

		string Address { get; }

		string Phone { get; }

		string FullName { get; }
	}
}
