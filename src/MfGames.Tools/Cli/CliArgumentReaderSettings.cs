namespace MfGames.Tools.Cli
{
	/// <summary>
	/// Contains the various controls to describe how CLI arguments are parsed. This is
	/// used to identify prefix strings used to identify the difference between a
	/// parameter and an optional argument.
	/// 
	/// Hints are provided to determine if an argument has values of their own, such
	/// as "--key=value" or "-k value".
	/// </summary>
	public class CliArgumentReaderSettings
	{
		/// <summary>
		/// Gets or sets the stop processing argument. This is the value of a single
		/// argument that indicates that all arguments after this point are automatically
		/// a parameter and that no additional processing should take place.
		/// 
		/// When this argument is removed the first time it is encountered in a string.
		/// </summary>
		public string StopProcessingArgument { get; set; }
	}
}