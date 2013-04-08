using System;

namespace MfGames.Tools.Cli
{
	/// <summary>
	/// Contains hints about how the options are processed.
	/// </summary>
	[Flags]
	public enum OptionHints
	{
		/// <summary>
		/// Indicates that the parameter has no special processing.
		/// </summary>
		None = 0,

		/// <summary>
		/// Indicates that the option has an requirement parameter.
		/// </summary>
		HasRequiredParameter = 1,
	}
}