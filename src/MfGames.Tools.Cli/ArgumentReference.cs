namespace MfGames.Tools.Cli
{
	/// <summary>
	/// Contains information about an argument that was referenced from the arguments
	/// given. This contains information about how many times it was seen, which values
	/// were given.
	/// </summary>
	public class ArgumentReference
	{
		public ArgumentReference(string key)
		{
			Key = key;
		}

		public string Key { get; private set; }
		public int ReferenceCount { get; set; }
	}
}