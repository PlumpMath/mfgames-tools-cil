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
