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
		/// Prevents a default instance of the <see cref="CliArgumentReaderSettings"/> class from being created.
		/// </summary>
		public CliArgumentReaderSettings()
		{
			Arguments = new CliArgumentReaderArgumentCollection();
		}

		/// <summary>
		/// Gets or sets the stop processing argument. This is the value of a single
		/// argument that indicates that all arguments after this point are automatically
		/// a parameter and that no additional processing should take place.
		/// 
		/// When this argument is removed the first time it is encountered in a string.
		/// </summary>
		public string StopProcessingArgument { get; set; }

		public CliArgumentReaderArgumentCollection Arguments { get; private set; }

		/// <summary>
		/// Gets a value indicating whether these settings allow for long options.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance has long options; otherwise, <c>false</c>.
		/// </value>
		public bool HasLongOptions
		{
			get { return !string.IsNullOrEmpty(LongOptionPrefix); }
		}

		/// <summary>
		/// Gets or sets the prefix string which indicates a long option.
		/// </summary>
		protected string LongOptionPrefix { get; set; }

		/// <summary>
		/// Parses the argument as a long option. If this is not a valid string, this
		/// returns null. Otherwise, it returns the long option without any option prefix
		/// but including assignment (e.g., "assets=bob").
		/// </summary>
		/// <param name="argument">The argument to test.</param>
		/// <returns>Either the parsed option or null to indicate it isn't parsable.</returns>
		public string GetLongOption(string argument)
		{
			// If we don't have long options, then we don't do anything.
			if (!HasLongOptions)
			{
				return null;
			}

			// We have a long option, but if the argument doesn't match start with it,
			// then we skip it.
			if (!argument.StartsWith(LongOptionPrefix))
			{
				return null;
			}

			// Pull out the prefix string.
			string value = argument.Substring(LongOptionPrefix.Length);
			return value;
		}

		/// <summary>
		/// Determines whether the given argument indicates that the system
		/// should stop processing elements.
		/// </summary>
		/// <param name="argument">The argument.</param>
		/// <returns>
		///   <c>true</c> if [is stop processing] [the specified argument]; otherwise, <c>false</c>.
		/// </returns>
		/// <exception cref="System.NotImplementedException"></exception>
		public bool IsStopProcessing(string argument)
		{
			if (!string.IsNullOrEmpty(StopProcessingArgument)
				&& argument == StopProcessingArgument)
			{
				return true;
			}

			return false;
		}
	}
}
