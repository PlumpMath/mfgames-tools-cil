// MfGames Tools CIL
// 
// Copyright 2013 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-tools-cil/license

using System;

namespace MfGames.Tools
{
	/// <summary>
	/// Defines the scope for the various attributes. This allows for
	/// </summary>
	[Flags]
	public enum AliasScopes: byte
	{
		/// <summary>
		/// Indicates the alias applies to all applications and types of tools.
		/// </summary>
		All = Cli | MsBuild | Nant,

		/// <summary>
		/// Includes the frameworks that use task-based names (NANt, MSBuild) that correlate
		/// to the class names.
		/// </summary>
		Tasks = MsBuild | Nant,

		/// <summary>
		/// Indicates the alias applies to the command-line interfaces (CLI). Typically these
		/// aliases will be lowercase and dash-separated (e.g., "build-directory").
		/// </summary>
		Cli = 1,

		/// <summary>
		/// Indicates the alias is the name for the MSBuild tasks.
		/// </summary>
		MsBuild = 2,

		/// <summary>
		/// Indicates the alias is the name for NAnt tasks.
		/// </summary>
		Nant = 4,
	}
}
