using System;

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

		/// <summary>
		/// Gets the specific argument settings.
		/// </summary>
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
		/// Gets a value indicating whether these settings allow for long option
		/// assignment.
		/// </summary>
		public bool HasLongOptionAssignment
		{
			get { return !string.IsNullOrEmpty(LongOptionAssignment); }
		}

		/// <summary>
		/// Gets a value indicating whether these settings allow for short options.
		/// </summary>
		public bool HasShortOptions
		{
			get { return !string.IsNullOrEmpty(ShortOptionPrefix); }
		}

		/// <summary>
		/// Gets or sets a value indicating whether short options can be bundled together.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if short options can be bundled; otherwise, <c>false</c>.
		/// </value>
		public bool AllowShortOptionBundling { get; set; }

		/// <summary>
		/// Gets or sets the prefix string which indicates a long option.
		/// </summary>
		public string LongOptionPrefix { get; set; }

		/// <summary>
		/// Gets or sets the long option assignment string, this is what identifies an
		/// assignment operator.
		/// </summary>
		public string LongOptionAssignment { get; set; }

		/// <summary>
		/// Gets or sets the prefix string which indicates a short option.
		/// </summary>
		protected string ShortOptionPrefix { get; set; }

		/// <summary>
		/// Parses the argument as a long option. If this is not a valid string, this
		/// returns null. Otherwise, it returns the long option without any option prefix
		/// but including assignment (e.g., "assets=bob").
		/// </summary>
		/// <param name="argument">The argument to test.</param>
		/// <param name="option">The long option parsed from the argument.</param>
		/// <param name="value">The value parsed from the argument or null.</param>
		public bool GetLongOption(
			string argument,
			out string option,
			out string value)
		{
			// If we don't have long options, then we don't do anything.
			if (!HasLongOptions)
			{
				option = null;
				value = null;
				return false;
			}

			// We have a long option, but if the argument doesn't match start with it,
			// then we skip it.
			if (!argument.StartsWith(LongOptionPrefix))
			{
				option = null;
				value = null;
				return false;
			}

			// Pull out the prefix string from the value.
			option = argument.Substring(LongOptionPrefix.Length);
			value = null;

			// Check to see if the long option assignment operator.
			if (HasLongOptionAssignment)
			{
				int operatorIndex = option.IndexOf(
					LongOptionAssignment,
					StringComparison.InvariantCulture);

				if (operatorIndex > 0)
				{
					value = option.Substring(operatorIndex + 1);
					option = option.Substring(
						0,
						operatorIndex);
				}
			}

			// Return that we were successful in finding a long value.
			return true;
		}

		/// <summary>
		/// Parses the argument as a short option bundle. If this is not a valid string, this
		/// returns null. Otherwise, it returns the short option bundle without any option prefix
		/// but including assignment (e.g., "avfilename").
		/// </summary>
		/// <param name="argument">The argument to test.</param>
		/// <returns>Either the parsed option or null to indicate it isn't parsable.</returns>
		public string GetShortOptions(string argument)
		{
			// If we don't have short options, then we don't do anything.
			if (!HasShortOptions)
			{
				return null;
			}

			// We have a long option, but if the argument doesn't match start with it,
			// then we skip it.
			if (!argument.StartsWith(ShortOptionPrefix))
			{
				return null;
			}

			// Pull out the prefix string.
			string value = argument.Substring(ShortOptionPrefix.Length);
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
