using System;
using System.Collections.Generic;

namespace MfGames.Tools.Cli
{
	/// <summary>
	/// A tool factory that creates tools by parsing command-line arguments.
	/// </summary>
	public class CliToolFactory
	{
		public TTool Create<TTool>(IEnumerable<string> arguments)
		{
			Type toolType = typeof(TTool);
			object constructedTool = Create(toolType, arguments);
			return (TTool)constructedTool;
		}

		/// <summary>
		/// Creates a tool by first using the given type to find the appropriate
		/// constructor and then populate the properties if possible. Validation is
		/// perform to determine if there are
		/// </summary>
		/// <param name="toolType">The tool type to construct.</param>
		/// <param name="arguments">The arguments to parse.</param>
		/// <returns></returns>
		public object Create(
			Type toolType, 
			IEnumerable<string> arguments)
		{
			// Convert the arguments into an ordered list of required arguments
			// and a dictionary of optional arguments.
			return null;
		}
	}
}
