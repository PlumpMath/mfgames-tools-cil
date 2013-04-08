namespace MfGames.Tools.Cli
{
	/// <summary>
	/// Enumeration that describes the current type of argument in the reader.
	/// </summary>
	public enum CliArgumentType
	{
		/// <summary>
		/// Indicates an unknown state or end of argument processing.
		/// </summary>
		None,

		/// <summary>
		/// Indicates a required parameter.
		/// </summary>
		Parameter,

		/// <summary>
		/// Indicates an optional argument.
		/// </summary>
		Optional,
	}
}