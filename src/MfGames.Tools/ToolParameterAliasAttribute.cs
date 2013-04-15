// Copyright 2013 Moonfire Games
// 
// Released under the MIT license
// http://mfgames.com/mfgames-tools-cil/license

using System;

namespace MfGames.Tools
{
	public class ToolParameterAliasAttribute: Attribute
	{
		public ToolParameterAliasAttribute(string alias)
			: this(alias,
				AliasScopes.All)
		{
		}

		public ToolParameterAliasAttribute(
			string alias,
			AliasScopes aliasScopes)
		{
		}
	}
}
