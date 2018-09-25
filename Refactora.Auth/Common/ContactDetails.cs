namespace Refactora.Auth.Common
{
	public class ContactDetails : IContactDetails
    {
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Address { get; set; }

		public string Phone { get; set; }

		public string FullName
		{
			get { return string.Join(' ', new[] { FirstName, LastName }).Trim(); }
		}
	}
}
