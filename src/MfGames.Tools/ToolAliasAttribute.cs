// MfGames Tools CIL
// 
// Copyright 2013 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-tools-cil/license

using System;

namespace MfGames.Tools
{
	/// <summary>
	/// Describes the various names that a tool may have in the various systems that implement tools.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true
		)]
	public class ToolAliasAttribute: Attribute
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolAliasAttribute"/> class.
		/// </summary>
		/// <param name="alias">The alias.</param>
		/// <param name="scopes">The scopes.</param>
		public ToolAliasAttribute(
			string alias,
			AliasScopes scopes)
		{
		}

		#endregion
	}
}
