using System;

namespace MfGames.Tools.Cli
{
	/// <summary>
	/// Indicates hints for processing a specific argument. This enumeration is used
	/// to indicate there are optional or required values associated with the argument,
	/// if there is reference counting, or other processing options.
	/// </summary>
	[Flags]
	public enum CliArgumentHints: byte
	{
		/// <summary>
		/// Indicates no special processing is required for the argument.
		/// </summary>
		None = 0,

		/// <summary>
		/// Indicates that the argument has a value associated with it. If the 
		/// OptionalValue is not included in the enumeration, then the value is
		/// required.
		/// </summary>
		HasValue = 1,

		/// <summary>
		/// Indicates that the value, if it has one, is optional.
		/// </summary>
		OptionalValue = 2,

		/// <summary>
		/// Indicates that there is a value and it is optional. This automatically
		/// includes HasValue as part of its numerical value.
		/// </summary>
		HasOptionalValue = HasValue | OptionalValue,

		/// <summary>
		/// Indicates that the number of times the value is included in the arguments
		/// should be counted. For example, "-v -v" would store 2. If the parameter is
		/// not present, the results will have it stored as 0.
		/// 
		/// If the field also has HasOptionalValue, then "-v 2" and "-v2" will also
		/// store two. In cases where both are used (e.g., "-v 2 -v"), then the values
		/// will be summed. Likewise "-v 2 -v 2" will store 4 as the value.
		/// </summary>
		ReferenceCounted = 4,

		/// <summary>
		/// If this flag is present, then the resulting otuput will be a list of string
		/// values instead of a single value.
		/// </summary>
		AllowsMultipleValues = 8,
	}
}
