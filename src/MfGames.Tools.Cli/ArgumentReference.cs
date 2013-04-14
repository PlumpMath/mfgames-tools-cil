namespace MfGames.Tools.Cli
{
	/// <summary>
	/// Contains information about an argument that was referenced from the arguments
	/// given. This contains information about how many times it was seen, which values
	/// were given.
	/// </summary>
	public class ArgumentReference
	{
		public ArgumentReference(Argument argument)
		{
			Argument = argument;
		}

		public Argument Argument { get; private set; }
		public int ReferenceCount { get; set; }
	}
}