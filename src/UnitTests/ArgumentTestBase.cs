using System.Collections.Generic;
using MfGames.Tools.Cli;

namespace UnitTests
{
	public class ArgumentTestBase
	{
		/// <summary>
		/// Creates the common settings with various arguments populated.
		/// </summary>
		/// <returns>A populated argument settings class.</returns>
		protected ArgumentSettings CreateCommonSettings()
		{
			// Create the initial settings using the Unix-style parameters.
			var settings = new GetOptArgumentSettings();

			settings.Arguments.Add(
				new Argument
				{
					Key = "Option 1",
					LongOptionNames = new List<string>
					{
						"opt1"
					},
					ShortOptionNames = new List<string>
					{
						"a"
					}
				});
			settings.Arguments.Add(
				new Argument
				{
					Key = "Array",
					LongOptionNames = new List<string>
					{
						"array"
					},
					ShortOptionNames = new List<string>
					{
						"t"
					},
					ValueCount = 1
				});
			settings.Arguments.Add(
				new Argument
				{
					Key = "Dual",
					LongOptionNames = new List<string>
					{
						"dual"
					},
					ShortOptionNames = new List<string>
					{
						"d"
					},
					ValueCount = 2
				});

			// Return the resulting settings.
			return settings;
		}
	}
}
