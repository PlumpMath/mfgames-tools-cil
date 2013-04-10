using System.Collections.Generic;

namespace MfGames.Tools.Cli
{
	/// <summary>
	/// Defines the common interfaces for arguments parsed by the CliArgumentReader.
	/// </summary>
	public interface ICliArgumentReaderArgument
	{
		/// <summary>
		/// Gets the number of values the argument requires.
		/// </summary>
		int ValueCount { get; }

		/// <summary>
		/// Gets a value indicating whether values are required. This is an invalid
		/// state if ValueCount greater than 1 while this value is true.
		/// </summary>
		bool HasOptionalValues { get; }

		/// <summary>
		/// Gets the long option names associated with this argument. If this is null
		/// then there are no long options.
		/// </summary>
		ICollection<string> LongOptionNames { get; }

		/// <summary>
		/// Gets the short option names associated with this argument. If this is null
		/// then there are no short options.
		/// </summary>
		ICollection<string> ShortOptionNames { get; }
	}
}
