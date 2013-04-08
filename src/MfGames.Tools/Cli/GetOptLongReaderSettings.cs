namespace MfGames.Tools.Cli
{
	/// <summary>
	/// A convinence settings object that includes the default settings for a GNU
	/// GetOpt long-style argument parsing. This style uses the following rules.
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