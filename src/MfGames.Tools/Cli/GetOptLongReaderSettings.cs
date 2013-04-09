namespace MfGames.Tools.Cli
{
	/// <summary>
	/// A convinence settings object that includes the default settings for a GNU
	/// GetOpt long-style argument parsing. This style uses the following rules:
	/// 
	/// <list type="bullet">
	/// <item>Single character optional arguments are prefixed with a dash (i.e., "-a").</item>
	/// <item>Single character optional arguments can be combined together (e.g., "-a -b" is the same as "-ab").</item>
	/// <item>Single character optional arguments can be followed by a value with an optional whitespace (e.g., "-a 24" and "-a24").</item>
	/// <item>Single character optional arguments can be counted for values (e.g., "-vv", "-v 2", "-v2" are the same).</item>
	/// <item>Longer optional arguments are prefixed with double-dashes (i.e., "--arg").</item>
	/// <item>Longer optional arguments cannot be bundled.</item>
	/// <item>Longer optional arguments can have a value followed by whitespace or an equal (e.g., "--key=value" or "--key value").</item>
	/// <item>Longer optional arguments can have more than one value (e.g., "--coords 24.2 12.2").</item>
	/// <item>To stop processing, a double-dash as a single argument is used. For example, "--verbose -- --value" results in "--value" as a parameter.</item>
	/// </list>
	/// 
	/// To handle all of these conditions is nearly impossible to do without additional information.
	/// Because of this, additional argument settings are required to indicate if a specific
	/// argument allows for singular or multiple values, if it is considered a short or long argument,
	/// and if it is counted.
	/// </summary>
	public class GetOptLongReaderSettings : CliArgumentReaderSettings
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="GetOptLongReaderSettings"/> class.
		/// </summary>
		public GetOptLongReaderSettings()
		{
			StopProcessingArgument = "--";
		}
	}
}