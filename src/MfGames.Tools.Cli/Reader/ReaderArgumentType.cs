// MfGames Tools CIL
// 
// Copyright 2013 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-tools-cil/license

namespace MfGames.Tools.Cli.Reader
{
	/// <summary>
	/// Enumeration that describes the current type of argument in the reader.
	/// </summary>
	public enum ReaderArgumentType
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
		/// Indicates a description optional argument.
		/// </summary>
		LongOption,

		/// <summary>
		/// Indicates a single-character optional argument that can be bundled.
		/// </summary>
		ShortOption,
	}
}
