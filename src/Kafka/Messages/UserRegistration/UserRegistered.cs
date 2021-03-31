namespace Kafka.Messages.UserRegistration
{
	/// <summary>
	///  Represents the message object in UserRegistered Topic
	/// </summary>
	public class UserRegistered
	{
		/// <summary>
		/// Indicates UserName to be used for login
		/// </summary>
		public string UserName { get; set; }
		/// <summary>
		/// Indicates firstName
		/// </summary>
		public string FirstName { get; set; }
		/// <summary>
		/// Indicates lastname
		/// </summary>
		public string LastName { get; set; }
		/// <summary>
		/// Indicates EmailId
		/// </summary>
		public string EmailId { get; set; }
	}
}
